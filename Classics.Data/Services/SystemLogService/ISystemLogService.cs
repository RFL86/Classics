using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Services.SystemLogService
{
    public interface ISystemLogService
    {
        void Save(List<ObjectValue.SystemLog> logs);

    }
}
