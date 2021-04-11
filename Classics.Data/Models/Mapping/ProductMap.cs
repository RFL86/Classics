

using Classics.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace Classics.Data.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {

            // Primary Key
            HasKey(t => t.ProductId);


            Property(t => t.Title).IsRequired().HasMaxLength(100);
            Property(t => t.Description).IsRequired().HasMaxLength(500);

            // Table & Column Mappings
            ToTable("Product");
            Property(t => t.ProductId).HasColumnName("ProductId");
            Property(t => t.Title).HasColumnName("Title");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.Price).HasColumnName("Price");
            Property(t => t.OwnerId).HasColumnName("OwnerId");
            Property(t => t.CarModelId).HasColumnName("CarModelId");

            HasRequired(t => t.Owner)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.OwnerId).WillCascadeOnDelete(false);

            HasOptional(t => t.CarModel)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.CarModelId).WillCascadeOnDelete(false);
        }
    }
}
