using SimpleInventoryAPI.Data;

namespace SimpleInventoryAPI.Models
{
    public class InventoryMove : BaseModel
    {
        public required People People { get; set; }
        public IEnumerable<InventoryMoveItem>? Items { get; set; }

        public required InventoryMoveType MoveType { get; set; }
    }
}
