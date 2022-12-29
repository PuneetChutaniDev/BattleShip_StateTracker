using BattleShip_StateTracker.Common;

namespace BattleShip_StateTracker.Models
{
    public class BattleshipPlacementModel
    {
        public string Location { get; set; }

        public BattleFieldState Value { get; set; }
    }
}