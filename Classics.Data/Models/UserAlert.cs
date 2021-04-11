using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Models
{
    public class UserAlert
    {
        public Guid UserAlertId { get; set; }
        public Guid UserId { get; set; }
        public Guid AlertId { get; set; }
        public Enums.UserAlert.ReadingStatus ReadingStatus { get; set; }
        public virtual User User { get; set; }
        public virtual Alert Alert { get; set; }
    }
}
