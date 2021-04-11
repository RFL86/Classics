using Classics.Data.Models;
using Classics.Data.Repository.Base;


namespace Classics.Data.Repository
{
    public class SerieRepository : RepositoryBase<Serie>, ISerieRepository
    {
        private readonly ClassicsContext _context;

        public SerieRepository(ClassicsContext context) : base(context)
        {
            _context = context;
        }

    }
}