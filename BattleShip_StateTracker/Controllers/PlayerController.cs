using BattleShip_StateTracker.Models;
using BattleShip_StateTracker.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShip_StateTracker.Controllers
{
    public class PlayerController
    {
        static int _playerId = 0;
        private List<PlayerModel> _players = new List<PlayerModel>();
        public PlayerController()
        {
            _playerId++;
        }

        public PlayerModel AddPlayer(PlayerRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("board model cannot be null", nameof(BoardRequest));
            }

            var player = new PlayerModel { Id = _playerId, Name = request.Name };
            _players.Add(player);

            return player;  
        }

        public void ShowPlayerDetail(int id)
        {
            var player = _players.FirstOrDefault(p => p.Id == id);
            if (player != null)
            {
                Console.WriteLine($"Player id: {player.Id} & Player name: {player.Name}");
            }
        }

        public List<PlayerModel> GetAllPlayers() => _players;
    }
}
