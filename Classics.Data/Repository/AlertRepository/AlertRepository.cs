using Classics.Data.Models;
using Classics.Data.Repository.Base;


namespace Classics.Data.Repository
{
    public class AlertRepository : RepositoryBase<Alert>, IAlertRepository
    {
        private readonly ClassicsContext _context;

        public AlertRepository(ClassicsContext context) : base(context)
        {
            _context = context;
        }

    }
}