using System.Linq.Expressions;
using WooCommerce.Data;
using WooCommerce.Models;
using WooCommerce.Repository.IRepository;

namespace WooCommerce.Repository
{
    public class ShoopingCartRepository : Repository<ShoopingCart>,IShoopingCartRepository
    {
        private ApplicationDbContext _db;

        public ShoopingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


       

        void IShoopingCartRepository.Update(ShoopingCart obj)
        {
            _db.ShoopingCarts.Update(obj);
        }
    }
}
