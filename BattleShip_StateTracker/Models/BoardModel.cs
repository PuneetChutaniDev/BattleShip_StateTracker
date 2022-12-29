using System.Collections.Generic;

namespace BattleShip_StateTracker.Models
{
    public class BoardModel
    {        
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public string Name { get; set; }
        
        public int Size { get; set; }
    }
}
