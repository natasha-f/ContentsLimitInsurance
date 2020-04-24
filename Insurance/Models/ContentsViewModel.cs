using System.Collections.Generic;

namespace Insurance.Web.Models
{
    public class ContentsViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<UsedCategoryViewModel> UsedCategories { get; set; }

        public string Total { get; set; }
    }
}
