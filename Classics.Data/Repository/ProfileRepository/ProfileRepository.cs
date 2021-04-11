using Classics.Data.Models;
using Classics.Data.Repository.Base;


namespace Classics.Data.Repository
{
    public class ProfileRepository : RepositoryBase<Profile>, IProfileRepository
    {
        private readonly ClassicsContext _context;

        public ProfileRepository(ClassicsContext context) : base(context)
        {
            _context = context;
        }

    }
}