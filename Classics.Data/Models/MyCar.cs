using System;
using System.Collections.Generic;
using System.Linq;

namespace Classics.Data.Models
{
    public class MyCar
    {
        public MyCar()
        {

        }

        public Guid MyCarId { get; set; }
        public Guid SerieId { get; set; }
        public Guid OwnerId { get; set; }
        public string NickName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Enums.MyCar.MyCarStatus Status { get; set; }
        public virtual User User { get; set; }
        public virtual Serie Serie { get; set; }

    }
}
