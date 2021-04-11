using Classics.Data.Models;
using Classics.Data.Repository.Base;


namespace Classics.Data.Repository
{
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        private readonly ClassicsContext _context;

        public SupplierRepository(ClassicsContext context) : base(context)
        {
            _context = context;
        }

    }
}