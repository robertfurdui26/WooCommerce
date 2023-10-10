using System.Linq.Expressions;
using WooCommerce.Data;
using WooCommerce.Models;
using WooCommerce.Repository.IRepository;

namespace WooCommerce.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


       

        void ICompanyRepository.Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
