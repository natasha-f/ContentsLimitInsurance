using System.Collections.Generic;
using Insurance.Data.Data;
using Insurance.Data.Models;
using Insurance.Logic.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Logic
{
    public class ContentsRepo : IContentsRepo
    {
        private readonly InsuranceContext _context;

        public ContentsRepo(InsuranceContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _context.Items.Include(i => i.Category).ToList();
        }

        public void AddItem(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
        }

        public void DeleteItem(long id)
        {
            var item = _context.Items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
