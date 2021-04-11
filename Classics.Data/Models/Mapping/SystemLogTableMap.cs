//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Classics.Data.Mapping
//{
//    public class SystemLogTableMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Models.SystemLogTable>
//    {
//        public SystemLogTableMap()
//        {
//            // Primary Key
//            HasKey(t => t.SystemLogTableId);

//            // Properties
//            Property(t => t.TableName)
//                .IsRequired()
//                .HasMaxLength(100);

//            // Table & Column Mappings
//            ToTable("SystemLogTables", "classics");
//            Property(t => t.SystemLogTableId).HasColumnName("SystemLogTableId");
//            Property(t => t.TableName).HasColumnName("TableName");
//        }
//    }
//}
