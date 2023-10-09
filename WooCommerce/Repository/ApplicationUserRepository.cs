using System.Linq.Expressions;
using WooCommerce.Data;
using WooCommerce.Models;
using WooCommerce.Repository.IRepository;

namespace WooCommerce.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


       

        
    }
}
