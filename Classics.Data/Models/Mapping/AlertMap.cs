using Classics.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Mapping
{
    public class AlertMap : EntityTypeConfiguration<Alert>
    {
        public AlertMap()
        {
            // Primary Key
            HasKey(t => t.AlertId);

            Property(t => t.Subject).IsRequired().HasMaxLength(100);
            Property(t => t.Message).IsRequired().HasMaxLength(500);

            // Table & Column Mappings
            ToTable("Alert");
            Property(t => t.AlertId).HasColumnName("AlertId");
            Property(t => t.Subject).HasColumnName("Subject");
            Property(t => t.Message).HasColumnName("Message");
            Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.Receiver).HasColumnName("Receiver");

            HasRequired(t => t.Creator)
                .WithMany(t => t.Alerts)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
        }
    }
}
