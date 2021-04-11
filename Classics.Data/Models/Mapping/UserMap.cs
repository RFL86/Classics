using Classics.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            HasKey(t => t.UserId);

            // Properties
            Property(t => t.Name).IsRequired().HasMaxLength(200);
            Property(t => t.Email).IsRequired().HasMaxLength(200);
            Property(t => t.MobilePhone).IsOptional().HasMaxLength(20);
            Property(t => t.Password1).IsRequired().HasMaxLength(300);
            Property(t => t.Password2).IsOptional().HasMaxLength(300);

            // Table & Column Mappings
            ToTable("User");
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.MobilePhone).HasColumnName("MobilePhone");
            Property(t => t.Password1).HasColumnName("Password1");
            Property(t => t.Password2).HasColumnName("Password2");
            Property(t => t.Status).HasColumnName("Status"); 
            Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            Property(t => t.AddressId).HasColumnName("AddressId");

            // Relationships
            HasRequired(t => t.Profile)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.ProfileId).WillCascadeOnDelete(false);

            HasRequired(t => t.Address)
              .WithMany(t => t.Users)
              .HasForeignKey(d => d.AddressId).WillCascadeOnDelete(false);

        }
    }
}
