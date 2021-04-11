using Classics.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Mapping
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {

            // Primary Key
            HasKey(t => t.AddressId);

            // Properties          
            Property(t => t.ZipCode).IsRequired().HasMaxLength(20);
            Property(t => t.City).IsRequired().HasMaxLength(200);
            Property(t => t.StateCode).IsRequired().HasMaxLength(5);


            // Table & Column Mappings
            ToTable("Address");
            Property(t => t.AddressId).HasColumnName("AddressId");
            Property(t => t.ZipCode).HasColumnName("ZipCode");
            Property(t => t.StateCode).HasColumnName("StateCode");
            Property(t => t.City).HasColumnName("City");

        }
    }
}