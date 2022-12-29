using System;
using BattleShip_StateTracker.Common.Enums;
using BattleShip_StateTracker.Controllers;
using BattleShip_StateTracker.Models;
using BattleShip_StateTracker.Requests;

namespace BattleShip_StateTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Player
            PlayerController player = new PlayerController();
            Console.WriteLine("Enter the player name: ");
            var playerName = Console.ReadLine();
            var playerDetail = player.AddPlayer(new PlayerRequest { Name = playerName });

            // Display the player data
            player.ShowPlayerDetail(playerDetail.Id);
            Console.ReadKey();

            //Create a board game
            BoardController board = new BoardController();
            Console.WriteLine("Enter the Board name: ");
            var boardName = Console.ReadLine();

            int boardSize = 0;
            var boardDetail = new BoardModel();
            Console.WriteLine("Enter the Board size (It will be square grid, for eg Size = 5 means 5X5): ");
            if (int.TryParse(Console.ReadLine(), out boardSize))
            {
                boardDetail = board.CreateBoard(new BoardRequest { PlayerId = playerDetail.Id, Name = boardName, Size = boardSize });

                // Display the Board Grid data
                board.ShowBoardDetail(boardDetail.Id);

                // Display battleship details
                int battleShipSize = 3;
                BattleshipController battleship = new BattleshipController(battleShipSize, boardDetail.Size);
                // Adding 2 battleships
                battleship.AddBattleShip(new BattleshipRequest
                {
                    BoardId = boardDetail.Id,
                    Name = "Battleship one",
                    PlayerId = playerDetail.Id,
                    BattleFieldShape = BattleFieldShape.Horizontal,
                    startPos = "A-1"
                });

                battleship.AddBattleShip(new BattleshipRequest
                {
                    BoardId = boardDetail.Id,
                    Name = "Battleship two",
                    PlayerId = playerDetail.Id,
                    BattleFieldShape = BattleFieldShape.Vertical,
                    startPos = "C-1"
                });

                battleship.DisplayBattleshipDetails();

                Console.WriteLine("Attack position - 1: ");
                var attackPosition1 = Console.ReadLine();

                Console.WriteLine("It is a: " + battleship.Attack(attackPosition1, boardDetail.Id).ToString());
                battleship.DisplayBattleshipDetails();

                Console.WriteLine("Attack position - 2: ");
                var attackPosition2 = Console.ReadLine();

                Console.WriteLine("It is a: " + battleship.Attack(attackPosition2, boardDetail.Id).ToString());
                battleship.DisplayBattleshipDetails();

                Console.ReadKey();
                //System.Environment.Exit(1);
            }
        }
    }
}
