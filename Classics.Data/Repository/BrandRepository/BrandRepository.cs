using Classics.Data.Models;
using Classics.Data.Repository.Base;


namespace Classics.Data.Repository
{
    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        private readonly ClassicsContext _context;

        public BrandRepository(ClassicsContext context) : base(context)
        {
            _context = context;
        }

    }
}