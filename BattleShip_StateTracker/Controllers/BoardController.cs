using BattleShip_StateTracker.Models;
using BattleShip_StateTracker.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShip_StateTracker.Controllers
{
    public class BoardController
    {
        private static int _boardId = 0;
        private List<BoardModel> _boards = new List<BoardModel>();

        public BoardController()
        {
            _boardId++;
        }

        public BoardModel CreateBoard(BoardRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("board model cannot be null", nameof(BoardRequest));
            }

            if (request.Size < 5)
            {
                throw new Exception("A board size cannot be less than 5");
            }

            if (_boards.Count(b => b.PlayerId == request.PlayerId) >= 1)
            {
                throw new Exception("A player cannot have more than one board");
            }

            var board = new BoardModel { Id = _boardId, PlayerId = request.PlayerId, Name = request.Name, Size = request.Size };
            _boards.Add(board);

            return board;
        }

        public void ShowBoardDetail(int id)
        {
            var board = _boards.FirstOrDefault(b => b.Id == id);
            if (board != null)
            {
                Console.WriteLine($"Board id: {board.Id} & Board name: {board.Name} & Player Id: {board.PlayerId} & Board size: {board.Size}X{board.Size}");
            }
        }

        public List<BoardModel> GetAllBoards() => _boards;
    }
}