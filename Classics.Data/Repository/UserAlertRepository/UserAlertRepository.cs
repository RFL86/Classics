using Classics.Data.Models;
using Classics.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Repository
{
    public class UserAlertRepository : RepositoryBase<UserAlert>, IUserAlertRepository
    {
        private readonly ClassicsContext _context;

        public UserAlertRepository(ClassicsContext context) : base(context)
        {
            _context = context;
        }

    }
}