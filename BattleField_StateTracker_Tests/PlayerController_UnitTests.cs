using BattleShip_StateTracker.Controllers;
using BattleShip_StateTracker.Requests;
using NUnit.Framework;
using System.Linq;

namespace BattleField_StateTracker_Tests
{
    [TestFixture]
    public class PlayerController_UnitTests
    {
        private PlayerRequest _playerRequest;
        private PlayerController controller;

        [SetUp]
        public void Setup()
        {
            _playerRequest = new PlayerRequest();
        }

        [Test]
        public void AddPlayer_ThrowsException_IfRequestIsNull()
        {
            controller = new PlayerController();
            Assert.That(() => controller.AddPlayer(null), Throws.ArgumentNullException);
        }

        [Test]
        public void AddPlayer_ThrowsException_IncorrectBoardSize()
        {
            _playerRequest = GetPlayerRequest(name: "mickey mouse");
            controller = new PlayerController();
            var response = controller.AddPlayer(_playerRequest);

            Assert.AreEqual(response.Name, _playerRequest.Name);
            Assert.IsTrue(response.Id > 0);
        }

        [Test]
        public void GetAllPlayers_Success()
        {
            controller = new PlayerController();

            _playerRequest = GetPlayerRequest(name: "mickey mouse");
            _ = controller.AddPlayer(_playerRequest);

            _playerRequest = GetPlayerRequest(name: "doremon");
            _ = controller.AddPlayer(_playerRequest);

            var result = controller.GetAllPlayers();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 2);
        }


        private PlayerRequest GetPlayerRequest(string name = default) =>
            _ = new PlayerRequest
            {
                Name = name
            };
    }
}