namespace BattleShip_StateTracker.Requests
{
    public class BoardRequest
    {
        public int BoardId { get; set; }

        public int PlayerId { get; set; }

        public string Name { get; set; }
        
        public int Size { get; set; }
    }
}
