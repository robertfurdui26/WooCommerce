using WooCommerce.Migrations;
using WooCommerce.Models;

namespace WooCommerce.Repository.IRepository
{
    public interface IProductRepository :IRepository<Product>
    {
        void Update(Product obj);

    }
}
