using BattleShip_StateTracker.Common;
using BattleShip_StateTracker.Common.Enums;
using System.Collections;
using System.Collections.Generic;

namespace BattleShip_StateTracker.Models
{
    public class BattleshipModel
    {
        public int Id { get; set; }
        
        public int BoardId { get; set; }

        public int PlayerId { get; set; }

        public string Name { get; set; }

        public int BattleShipLength { get; set; }

        public string startPos { get; set; }

        public BattleFieldShape BattleFieldShape { get; set; }

        public List<BattleshipPlacementModel> Placements { get; set; }

        public IDictionary<string, BattleFieldState> val { get; set; }
    }
}
