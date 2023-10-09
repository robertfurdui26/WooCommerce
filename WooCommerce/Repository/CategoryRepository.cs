using System.Linq.Expressions;
using WooCommerce.Data;
using WooCommerce.Models;
using WooCommerce.Repository.IRepository;

namespace WooCommerce.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


       

        void ICategoryRepository.Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
