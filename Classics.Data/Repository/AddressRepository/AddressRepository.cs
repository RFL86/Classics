using Classics.Data.Models;
using Classics.Data.Repository.Base;


namespace Classics.Data.Repository
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        private readonly ClassicsContext _context;

        public AddressRepository(ClassicsContext context) : base(context)
        {
            _context = context;
        }

    }
}