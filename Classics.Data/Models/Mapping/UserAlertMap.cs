using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Models.Mapping
{
    public class UserAlertMap : EntityTypeConfiguration<UserAlert>
    {
        public UserAlertMap()
        {

            // Primary Key
            HasKey(t => t.UserAlertId);


            // Table & Column Mappings
            //ToTable("Brand", "classics");
            ToTable("UserAlert");
            Property(t => t.UserAlertId).HasColumnName("UserAlertId");
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.AlertId).HasColumnName("AlertId");
            Property(t => t.ReadingStatus).HasColumnName("ReadingStatus");

            HasRequired(t => t.User)
                .WithMany(t => t.UserAlerts)
                .HasForeignKey(d => d.UserId).WillCascadeOnDelete(false);

            HasRequired(t => t.Alert)
             .WithMany(t => t.UserAlerts)
             .HasForeignKey(d => d.AlertId).WillCascadeOnDelete(false);
        }
    }
}

