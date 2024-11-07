using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SimpleInventoryAPI.Models
{
    public class InventoryMoveItem : BaseModel
    {
        [ForeignKey(nameof(Models.InventoryMove))]
        public int InventoryMoveRefId { get; set; }

        [JsonIgnore]
        public InventoryMove? InventoryMove { get; set; }

        public required Product Product { get; set; }

        public float Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
