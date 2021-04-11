using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums
{
    public class Alert
    {
        public enum AlertStatus
        {
            Removed = 0,
            Available = 1,
        }

        public enum Receiver
        {
            Client = 0,
            All = 1,
            Custom = 2
        }

    }
}
