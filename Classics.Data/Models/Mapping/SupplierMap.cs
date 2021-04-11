using Classics.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Mapping
{
    public class SupplierMap : EntityTypeConfiguration<Supplier>
    {
        public SupplierMap()
        {
            // Primary Key
            HasKey(t => t.SupplierId);

            Property(t => t.Title).IsRequired().HasMaxLength(100);
            Property(t => t.Description).IsRequired().HasMaxLength(1000);
            Property(t => t.Email).IsRequired().HasMaxLength(50);
            Property(t => t.PhoneNumber).IsRequired().HasMaxLength(20);
            Property(t => t.Cnpj).IsRequired().HasMaxLength(20);

            // Table & Column Mappings
            ToTable("Supplier");
            Property(t => t.SupplierId).HasColumnName("SupplierId");
            Property(t => t.Title).HasColumnName("Title");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.Cnpj).HasColumnName("Cnpj");

            HasRequired(t => t.User)
                .WithMany(t => t.Suppliers)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
        }
    }
}
