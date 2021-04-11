using Classics.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Mapping
{
    public class ProfileMap : EntityTypeConfiguration<Profile>
    {
        public ProfileMap()
        {

            // Primary Key
            HasKey(t => t.ProfileId);

            // Properties
            Property(t => t.Name).IsRequired().HasMaxLength(100);

            // Table & Column Mappings
            ToTable("Profile");
            Property(t => t.ProfileId).HasColumnName("ProfileId");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Type).HasColumnName("Type"); 
            
        }
    }
}

