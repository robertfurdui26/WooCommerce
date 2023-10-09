using WooCommerce.Migrations;
using WooCommerce.Models;
using WooCommerce.Repository.IRepository;
using Category = WooCommerce.Models.Category;

namespace WooCommerce.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
    }
}
