using Classics.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace Classics.Data.Mapping
{
    public class CarModelMap : EntityTypeConfiguration<CarModel>
    {
        public CarModelMap()
        {

            // Primary Key
            HasKey(t => t.CarModelId);


            Property(t => t.Name).IsRequired().HasMaxLength(30);

            // Table & Column Mappings
            ToTable("CarModel");
            Property(t => t.CarModelId).HasColumnName("CarModelId");
            Property(t => t.BrandId).HasColumnName("BrandId");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            Property(t => t.Status).HasColumnName("Status");

            HasRequired(t => t.User)
                .WithMany(t => t.CarModels)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);

            HasRequired(t => t.Brand)
               .WithMany(t => t.CarModels)
               .HasForeignKey(d => d.BrandId).WillCascadeOnDelete(false);
        }
    }
}
