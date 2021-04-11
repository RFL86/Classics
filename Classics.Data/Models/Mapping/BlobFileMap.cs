
using Classics.Data.Models;
using System.Data.Entity.ModelConfiguration;


namespace Classics.Data.Mapping
{
    public class BlobFileMap : EntityTypeConfiguration<BlobFile>
    {
        public BlobFileMap()
        {

            // Primary Key
            HasKey(t => t.BlobFileId);


            Property(t => t.Name).IsRequired().HasMaxLength(100);
            Property(t => t.Container).IsRequired().HasMaxLength(50);
            Property(t => t.Action).IsRequired().HasMaxLength(50);
            Property(t => t.MIME).IsRequired().HasMaxLength(20);


            // Table & Column Mappings
            ToTable("BlobFile");
            Property(t => t.BlobFileId).HasColumnName("BlobFileId");
            Property(t => t.ReferId).HasColumnName("ReferId");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Container).HasColumnName("Container");
            Property(t => t.Action).HasColumnName("Action");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            Property(t => t.MIME).HasColumnName("MIME");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.Url).HasColumnName("Url");

            HasRequired(t => t.User)
                .WithMany(t => t.BlobFiles)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);

            HasOptional(t => t.Product)
             .WithMany(t => t.BlobFiles)
             .HasForeignKey(d => d.ReferId).WillCascadeOnDelete(false);
        }
    }
}
