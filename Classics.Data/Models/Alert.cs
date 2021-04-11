using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Models
{
    public class Alert
    {
        public Alert()
        {
            UserAlerts = new List<UserAlert>();
        }

        public Guid AlertId { get; set; }
        public Guid CreatedBy { get; set; }
        public Enums.Alert.Receiver Receiver { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public Enums.Alert.AlertStatus Status { get; set; }
        public virtual User Creator { get; set; }
        public virtual ICollection<UserAlert> UserAlerts { get; set; }

    }
}
