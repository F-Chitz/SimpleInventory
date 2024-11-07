using SimpleInventoryAPI.Data;

namespace SimpleInventoryAPI.Models
{
    public class Product : BaseModel
    {
        public required string Name { get; set; }

        public CostMethod CostMethod { get; set; }
        public decimal Cost { get; set; }
    }
}
