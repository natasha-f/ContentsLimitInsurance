using System.Collections.Generic;
using Insurance.Data.Models;

namespace Insurance.Logic.Interfaces
{
    public interface IContentsRepo
    {
        IEnumerable<Category> GetAllCategories();
        IEnumerable<Item> GetAllItems();
        void AddItem(Item item);
        void DeleteItem(long id);
    }
}
