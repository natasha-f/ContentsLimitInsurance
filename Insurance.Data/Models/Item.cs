using System.ComponentModel.DataAnnotations;

namespace Insurance.Data.Models
{
    public class Item
    {
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
