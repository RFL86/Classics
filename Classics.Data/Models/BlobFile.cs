using System;

namespace Classics.Data.Models
{
    public class BlobFile
    {
        public Guid BlobFileId { get; set; }
        public Guid? ReferId { get; set; }
        public string Name { get; set; }
        public string Container { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string MIME { get; set; }
        public Enums.BlobFile.BlobFileStatus Status { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }


    }
}
