namespace SimpleInventoryAPI.Models
{
    public class People : BaseModel
    {
        public required string Name { get; set; }
        public string? Document { get; set; }
    }
}
