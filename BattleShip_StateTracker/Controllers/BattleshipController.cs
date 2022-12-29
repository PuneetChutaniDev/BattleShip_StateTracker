using BattleShip_StateTracker.Common;
using BattleShip_StateTracker.Common.Enums;
using BattleShip_StateTracker.Models;
using BattleShip_StateTracker.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShip_StateTracker.Controllers
{
    public class BattleshipController
    {
        static int _battleShipId = 0;
        private List<BattleshipModel> _battleShips = new List<BattleshipModel>();
        private readonly int _battleShipSize; // Size is 1 by N
        private readonly int _boardSize;

        public BattleshipController(int battleShipSize, int boardSize)
        {
            _battleShipId++;
            _battleShipSize = battleShipSize;
            _boardSize = boardSize;
        }

        public BattleshipModel AddBattleShip(BattleshipRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("battleship model cannot be null", nameof(BattleshipModel));
            }

            if (_battleShipSize <= 0 || _battleShipSize >= _boardSize)
            {
                throw new Exception("Incorrect battleship size. Its either  less than 0 or greater than the board size");
            }
            //if (true)
            //{
            //    throw new Exception("battleship already exists on the position.");
            //}
            var battleShip = new BattleshipModel
            {
                Id = _battleShipId,
                Name = request.Name,
                PlayerId = request.PlayerId,                 
                BoardId = request.BoardId,
                BattleShipLength = _battleShipSize,
                startPos = request.startPos,
                BattleFieldShape = request.BattleFieldShape,
                Placements = GeneratePlacements(request) ?? throw new Exception("Conlflict exception! battleship already exists on the position.")
            };

            _battleShips.Add(battleShip);

            return battleShip;
        }

        public void DisplayBattleshipDetails()
        {
            if (_battleShips != null)
            {
                Console.WriteLine("********* Details are ***********");
                foreach (var battleship in _battleShips)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Battleship id: {battleship.Id} & Battleship name: {battleship.Name} & Player Id: {battleship.PlayerId} " +
                        $"& Battleship length: {battleship.BattleShipLength} & Battleship shape: { battleship.BattleFieldShape }");
                    foreach (var item in battleship.Placements) //Display the placements 
                    {
                        string result = BattleFieldState.Miss.ToString();
                        if (item.Value == BattleFieldState.Hit)
                        {
                            result = BattleFieldState.Hit.ToString();
                        }
                        Console.WriteLine($"Battleship Location: {item.Location} & Battleship result: { result }");
                    }
                }
            }
        }

        /// <summary>
        /// position format eg. (A-1)
        /// </summary>
        /// <param name="position"></param>
        public BattleFieldState Attack(string position, int boardId)
        {
            var result = BattleFieldState.Miss;
            
            // Update list of battleships
            var list = _battleShips.Where(x => x.BoardId == boardId).ToList();
            foreach (var item in list)
            {
                var placement = item.Placements.FirstOrDefault(l => l.Location.Equals(position));
                if (placement != null)
                {
                    result = BattleFieldState.Hit;
                    placement.Value = BattleFieldState.Hit;
                    break;
                }
            }

            return result;
        }

        public bool HasAllBattleshipSunk(BattleshipRequest request)
        {
            var list = _battleShips.Where(x => x.BoardId == request.BoardId && x.PlayerId == request.PlayerId).ToList();
            int count = 0;
            foreach (var item in list)
            {
                count = item.Placements.Count;
                if (count != item.Placements.Count(x => x.Value.Equals(BattleFieldState.Hit)))
                {
                    return false;                
                }
            }

            return true;
        }

        private List<BattleshipPlacementModel> GeneratePlacements(BattleshipRequest request)
        {
            var arr = request.startPos.Split('-');
            var list = new List<BattleshipPlacementModel>();

            switch (request.BattleFieldShape)
            {
                case BattleFieldShape.Horizontal:
                    int col = Convert.ToInt32(arr[1]);
                    for (int i = 0; i < _battleShipSize; i++)
                    {
                        var loc = arr[0] + "-" + col++;
                        if (Overlap(request.BoardId, loc))
                        {
                            Console.WriteLine($"The {loc} already exists");
                            list = null;
                            break;
                        }

                        var battleshipPlacementModel = new BattleshipPlacementModel { Location = loc, Value = BattleFieldState.Miss };
                        list.Add(battleshipPlacementModel);
                    }
                    break;
                case BattleFieldShape.Vertical:
                    char row = Char.Parse(arr[0]);
                    for (int i = 0; i < _battleShipSize; i++)
                    {
                        var loc = row++ + "-" + Convert.ToInt32(arr[1]);
                        if (Overlap(request.BoardId, loc))
                        {
                            Console.WriteLine($"The {loc} already exists");
                            list = null;
                            break;
                        }

                        var battleshipPlacementModel = new BattleshipPlacementModel { Location = loc, Value = BattleFieldState.Miss };
                        list.Add(battleshipPlacementModel);
                    }
                    break;
                default:
                    break;
            }

            return list;
        }

        private bool Overlap(int boardId, string position)
        {
            var list = _battleShips.Where(x => x.BoardId == boardId).ToList();
            foreach (var item in list)
            {
                var placement = item.Placements.FirstOrDefault(l => l.Location.Equals(position));
                if (placement != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
