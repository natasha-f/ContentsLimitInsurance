using System.Collections.Generic;

namespace Insurance.Web.Models
{
    public class UsedCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryTotal { get; set; }
        public IEnumerable<ItemViewModel> Items { get; set; }
    }
}
