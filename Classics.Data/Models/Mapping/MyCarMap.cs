

using Classics.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace Classics.Data.Mapping
{
    public class MyCarMap : EntityTypeConfiguration<MyCar>
    {
        public MyCarMap()
        {

            // Primary Key
            HasKey(t => t.MyCarId);
            Property(t => t.NickName).IsRequired().HasMaxLength(30);

            // Table & Column Mappings
            ToTable("MyCar");
            Property(t => t.MyCarId).HasColumnName("MyCarId");
            Property(t => t.NickName).HasColumnName("NickName"); 
            Property(t => t.OwnerId).HasColumnName("OwnerId"); 
            Property(t => t.SerieId).HasColumnName("SerieId");
            Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            Property(t => t.Status).HasColumnName("Status");

            HasRequired(t => t.User)
                .WithMany(t => t.MyCars)
                .HasForeignKey(d => d.OwnerId).WillCascadeOnDelete(false);

            HasRequired(t => t.Serie)
            .WithMany(t => t.MyCars)
            .HasForeignKey(d => d.SerieId).WillCascadeOnDelete(false);
        }
    }
}
