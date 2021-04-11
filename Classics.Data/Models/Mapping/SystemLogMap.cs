//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Classics.Data.Mapping
//{
//    public class SystemLogMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Models.SystemLog>
//    {
//        public SystemLogMap()
//        {
//            // Primary Key
//            HasKey(t => t.SystemLogId);

//            // Properties
//            Property(t => t.TableName)
//                .IsRequired()
//                .HasMaxLength(100);

//            Property(t => t.FieldName)
//                .IsRequired()
//                .HasMaxLength(100);

//            // Table & Column Mappings
//            //ToTable("SystemLogs", "classics");
//            ToTable("SystemLogs");
//            Property(t => t.SystemLogId).HasColumnName("SystemLogId");
//            Property(t => t.CreatedOn).HasColumnName("CreatedOn");
//            Property(t => t.TableName).HasColumnName("TableName");
//            Property(t => t.ReferId).HasColumnName("ReferId");
//            Property(t => t.FieldName).HasColumnName("FieldName");
//            Property(t => t.OriginalValue).HasColumnName("OriginalValue");
//            Property(t => t.CurrentValue).HasColumnName("CurrentValue");
//            Property(t => t.UserId).HasColumnName("UserId");
//            Property(t => t.Created).HasColumnName("Created");
//            Property(t => t.Changed).HasColumnName("Changed");
//            Property(t => t.Deleted).HasColumnName("Deleted");

//            // Relationships
//            HasRequired(t => t.User)
//                .WithMany(t => t.SystemLogs)
//                .HasForeignKey(d => d.UserId).WillCascadeOnDelete(false);

//        }
//    }
//}
