using Classics.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace Classics.Data.Mapping
{
    class SerieMap : EntityTypeConfiguration<Serie>
    {
        public SerieMap()
        {

            // Primary Key
            HasKey(t => t.SerieId);
            Property(t => t.Name).IsRequired().HasMaxLength(30);

            // Table & Column Mappings
            //ToTable("Serie", "classics");
            ToTable("Serie");
            Property(t => t.SerieId).HasColumnName("SerieId");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.CarModelId).HasColumnName("CarModelId");

            HasRequired(t => t.User)
                .WithMany(t => t.Series)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);

            HasRequired(t => t.CarModel)
                .WithMany(t => t.Series)
                .HasForeignKey(d => d.CarModelId).WillCascadeOnDelete(false);
        }
    }
}

