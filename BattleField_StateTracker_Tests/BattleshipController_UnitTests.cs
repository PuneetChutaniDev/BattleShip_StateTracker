using BattleShip_StateTracker.Common;
using BattleShip_StateTracker.Common.Enums;
using BattleShip_StateTracker.Controllers;
using BattleShip_StateTracker.Models;
using BattleShip_StateTracker.Requests;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BattleField_StateTracker_Tests
{
    [TestFixture]
    public class BattleshipController_UnitTests
    {
        private List<BattleshipModel> _battleShips;
        private BattleshipRequest _battleShipRequest;
        private BattleshipController controller;
        private const int boardSize = 7;
        private const int battleshipSize = 3;

        [SetUp]
        public void Setup()
        {
            _battleShips = new List<BattleshipModel>();
            _battleShipRequest = new BattleshipRequest();
        }

        [Test]
        public void AddBattleShip_ThrowsException_IfRequestIsNull()
        {
            controller = new BattleshipController(battleshipSize, boardSize);
            Assert.That(() => controller.AddBattleShip(null), Throws.ArgumentNullException);
        }

        [TestCase(-1, 4)]
        [TestCase(5, 4)]
        public void AddBattleShip_ThrowsException_IncorrectBattleShipSize(int battleshipSize, int boardSize)
        {
            controller = new BattleshipController(battleshipSize, boardSize);
            Assert.That(() => controller.AddBattleShip(_battleShipRequest), Throws.Exception);
        }

        [TestCase(3, 7)]
        public void AddBattleShip_SetupBattleship_Success(int battleshipSize, int boardSize)
        {
            controller = new BattleshipController(battleshipSize, boardSize);
            _battleShipRequest = GetBattleshipRequests(name: "battleship-1", playerId: 100, boardId: 1);
            var response = controller.AddBattleShip(_battleShipRequest);

            Assert.AreEqual(response.Name, _battleShipRequest.Name);
            Assert.AreEqual(response.PlayerId, _battleShipRequest.PlayerId);
            Assert.AreEqual(response.BoardId, _battleShipRequest.BoardId);
            Assert.AreEqual(response.BattleShipLength, battleshipSize);
            Assert.AreEqual(response.startPos, _battleShipRequest.startPos);
            Assert.AreEqual(response.BattleFieldShape, _battleShipRequest.BattleFieldShape);
            Assert.AreEqual(response.Placements.Count, battleshipSize);
            Assert.AreEqual(response.Placements.Count(x=>x.Value.Equals(BattleFieldState.Miss)), battleshipSize);
        }

        [Test]
        public void AddBattleShip_SetupBattleships_ResultsInOverlap()
        {
            SetupBattleships();
            _battleShipRequest = GetBattleshipRequests(name: "battleship-2", playerId: 100, boardId: 1, startPos: "A-2", battleFieldShape: BattleFieldShape.Vertical);

            Assert.That(() => controller.AddBattleShip(_battleShipRequest), Throws.Exception);
        }

        [Test]
        public void AddBattleShip_SunkBattleshipsTrue()
        {
            SetupBattleships();

            // Attack all battleships
            _ = controller.Attack("A-2", 1);
            _ = controller.Attack("A-3", 1);
            _ = controller.Attack("A-4", 1);
            _ = controller.Attack("C-2", 1);
            _ = controller.Attack("D-2", 1);
            _ = controller.Attack("E-2", 1);

            Assert.IsTrue(controller.HasAllBattleshipSunk(new BattleshipRequest { PlayerId = 100, BoardId = 1 }));
        }

        [TestCase("A-2", 1, BattleFieldState.Hit)]
        public void AddBattleShip_AttackBattleship_ReturnsHit(string position, int boardId, BattleFieldState battleFieldState)
        {
            SetupBattleships();
            var result = controller.Attack(position,boardId);

            Assert.AreEqual(result, battleFieldState);
        }

        [TestCase("G-3", 1, BattleFieldState.Miss)]
        public void AddBattleShip_AttackBattleship_ReturnsMiss(string position, int boardId, BattleFieldState battleFieldState)
        {
            SetupBattleships();
            var result = controller.Attack(position, boardId);

            Assert.AreEqual(result, battleFieldState);
        }

        private void SetupBattleships()
        {
            controller = new BattleshipController(battleshipSize, boardSize);
            // BattleShip - 1
            _battleShipRequest = GetBattleshipRequests(name: "battleship-1", playerId: 100, boardId: 1, startPos: "A-2", battleFieldShape: BattleFieldShape.Horizontal);
            controller.AddBattleShip(_battleShipRequest);

            // BattleShip - 2
            _battleShipRequest = GetBattleshipRequests(name: "battleship-2", playerId: 100, boardId: 1, startPos: "C-2", battleFieldShape: BattleFieldShape.Vertical);
            controller.AddBattleShip(_battleShipRequest);
        }

        private BattleshipRequest GetBattleshipRequests(string name = default, int playerId = 0, 
            int boardId = 0, string startPos = "A-1", BattleFieldShape battleFieldShape = BattleFieldShape.Horizontal) =>
            _= new BattleshipRequest 
            {
                Name = name,
                PlayerId = playerId,
                BoardId = boardId,
                startPos = startPos,
                BattleFieldShape = battleFieldShape
            };
    }
}