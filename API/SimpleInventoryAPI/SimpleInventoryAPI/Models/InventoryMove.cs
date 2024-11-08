using SimpleInventoryAPI.Data;
using System.Text.Json.Serialization;

namespace SimpleInventoryAPI.Models
{
    public class InventoryMove : BaseModel
    {
        public required People People { get; set; }
        public IEnumerable<InventoryMoveItem>? Items { get; set; }
        [JsonIgnore]
        public InventoryMoveType MoveType { get; set; }
    }
}
