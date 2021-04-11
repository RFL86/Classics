using Classics.Data.Models;
using Classics.Data.Repository.Base;


namespace Classics.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly ClassicsContext _context;

        public UserRepository(ClassicsContext context) : base(context)
        {
            _context = context;
        }

    }
}