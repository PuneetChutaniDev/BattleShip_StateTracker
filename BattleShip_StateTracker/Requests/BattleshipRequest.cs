using BattleShip_StateTracker.Common;
using BattleShip_StateTracker.Common.Enums;

namespace BattleShip_StateTracker.Requests
{
    public class BattleshipRequest
    {
        public int BoardId { get; set; }

        public int PlayerId { get; set; }

        public string Name { get; set; }

        public string startPos { get; set; }

        public BattleFieldShape BattleFieldShape { get; set; }
    }
}
