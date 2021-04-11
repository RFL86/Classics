using Classics.Data.Models;
using Classics.Data.Repository.Base;


namespace Classics.Data.Repository
{
    public class BlobFileRepository : RepositoryBase<BlobFile>, IBlobFileRepository
    {
        private readonly ClassicsContext _context;

        public BlobFileRepository(ClassicsContext context) : base(context)
        {
            _context = context;
        }

    }
}