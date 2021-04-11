using Classics.Data.Models;
using ClassicsApp.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.Services
{
    public interface IBlobFileService
    {
        string UploadToBlob(BlobFile blobFile, byte[] fileBytes);
        void AddBlobFile(BlobFile file);
    }
}
