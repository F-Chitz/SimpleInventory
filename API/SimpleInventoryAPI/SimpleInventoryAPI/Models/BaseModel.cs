using System.ComponentModel.DataAnnotations;

namespace SimpleInventoryAPI.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            IsActive = true;
            CreateAt = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }
    }
}
