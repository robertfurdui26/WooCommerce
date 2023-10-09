using System.Linq.Expressions;
using WooCommerce.Data;
using WooCommerce.Models;
using WooCommerce.Repository.IRepository;

namespace WooCommerce.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


       

        void IOrderHeaderRepository.Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }
    }
}
