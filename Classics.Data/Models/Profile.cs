using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Models
{
    public class Profile
    {
        public Profile()
        {
            Users = new List<User>();
        }

        public Guid ProfileId { get; set; }
        public string Name { get; set; }
        public Enums.Profile.ProfileType Type { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
