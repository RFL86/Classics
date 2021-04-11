using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Models
{
    public class Serie
    {
        public Serie()
        {
            MyCars = new List<MyCar>();
        }

        public Guid SerieId { get; set; }
        public Guid CarModelId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Enums.Serie.SerieStatus Status { get; set; }
        public virtual User User { get; set; }
        public virtual CarModel CarModel { get; set; }
        public virtual ICollection<MyCar> MyCars { get; set; }

    }
}
