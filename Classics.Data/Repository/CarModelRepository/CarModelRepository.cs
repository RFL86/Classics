using Classics.Data.Models;
using Classics.Data.Repository.Base;


namespace Classics.Data.Repository
{
    public class CarModelRepository : RepositoryBase<CarModel>, ICarModelRepository
    {
        private readonly ClassicsContext _context;

        public CarModelRepository(ClassicsContext context) : base(context)
        {
            _context = context;
        }

    }
}