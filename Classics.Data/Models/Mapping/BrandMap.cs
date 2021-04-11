using Classics.Data.Models;
using System.Data.Entity.ModelConfiguration;


namespace Classics.Data.Mapping
{
    public class BrandMap : EntityTypeConfiguration<Brand>
    {
        public BrandMap()
        {

            // Primary Key
            HasKey(t => t.BrandId);


            Property(t => t.Name).IsRequired().HasMaxLength(100);

            // Table & Column Mappings
            //ToTable("Brand", "classics");
            ToTable("Brand");
            Property(t => t.BrandId).HasColumnName("BrandId");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            Property(t => t.Status).HasColumnName("Status");

            HasRequired(t => t.User)
                .WithMany(t => t.Brands)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
        }
    }
}
