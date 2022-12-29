using BattleShip_StateTracker.Controllers;
using BattleShip_StateTracker.Requests;
using NUnit.Framework;

namespace BattleField_StateTracker_Tests
{
    [TestFixture]
    public class BoardController_UnitTests
    {
        private BoardRequest _boardRequest;
        private BoardController controller;

        [SetUp]
        public void Setup()
        {
            _boardRequest = new BoardRequest();
        }

        [Test]
        public void CreateBoard_ThrowsException_IfRequestIsNull()
        {
            controller = new BoardController();
            Assert.That(() => controller.CreateBoard(null), Throws.ArgumentNullException);
        }

        [Test]
        public void CreateBoard_ThrowsException_IncorrectBoardSize()
        {
            _boardRequest = GetBoardRequest(name: "my board", playerId: 100, size: 4);
            controller = new BoardController();
            Assert.That(() => controller.CreateBoard(_boardRequest), Throws.Exception);
        }

        [Test]
        public void CreateBoard_ThrowsException_PlayerCannotHaveMoreThanOneBoard()
        {
            _boardRequest = GetBoardRequest(name: "my board", playerId: 100, size: 6);
            
            // one board created
            controller = new BoardController();
            _ = controller.CreateBoard(_boardRequest);

            // Exception when more than one board is trying to create
            _boardRequest = GetBoardRequest(name: "my second board", playerId: 100, size: 10);

            Assert.That(() => controller.CreateBoard(_boardRequest), Throws.Exception);
        }

        [Test]
        public void CreateBoard_SetupBoard_Success()
        {
            _boardRequest = GetBoardRequest(name: "my board", playerId: 100, size: 7);
            controller = new BoardController();
            var response = controller.CreateBoard(_boardRequest);

            Assert.AreEqual(response.Name, _boardRequest.Name);
            Assert.AreEqual(response.PlayerId, _boardRequest.PlayerId);
            Assert.AreEqual(response.Size, _boardRequest.Size);
        }

        [Test]
        public void GetAllBoard_Success()
        {
            controller = new BoardController();

            _boardRequest = GetBoardRequest(name: "board-1", playerId: 100, size: 7);
            _=controller.CreateBoard(_boardRequest);

            _boardRequest = GetBoardRequest(name: "board-2", playerId: 101, size: 7);
            _ = controller.CreateBoard(_boardRequest);

            var result = controller.GetAllBoards();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 2);
        }

        private BoardRequest GetBoardRequest(string name = default, int playerId = 0, int size = 0) =>
            _ = new BoardRequest
            {
                Name = name,
                PlayerId = playerId,
                Size = size,
            };
    }
}