using Classics.Data.Models;
using Classics.Data.Repository.Base;


namespace Classics.Data.Repository
{
    public class MyCarRepository : RepositoryBase<MyCar>, IMyCarRepository
    {
        private readonly ClassicsContext _context;

        public MyCarRepository(ClassicsContext context) : base(context)
        {
            _context = context;
        }

    }
}