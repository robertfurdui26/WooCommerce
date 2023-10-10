
using WooCommerce.Models;
using WooCommerce.Repository.IRepository;
using Category = WooCommerce.Models.Category;

namespace WooCommerce.Repository.IRepository
{
    public interface IShoopingCartRepository : IRepository<ShoopingCart>
    {
        void Update(ShoopingCart obj);
    }
}
