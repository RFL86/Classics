using Classics.Data.Models;
using Classics.Data.Repository.Base;


namespace Classics.Data.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly ClassicsContext _context;

        public ProductRepository(ClassicsContext context) : base(context)
        {
            _context = context;
        }

    }
}