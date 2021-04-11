using Classics.Data.UnitOfWork;
using Classics.Data.Models;
using ClassicsApp.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;

namespace ClassicsApp.Services
{
    public class BlobFileService : IBlobFileService
    {
        private string _blobAccount => "DefaultEndpointsProtocol=https;AccountName=classics;AccountKey=aAh4x4MJpEDybKGMCyEnN/FmPyxrkER6nuUg/D4lqorKdEtCdih2XWRi1YrUNv8L4gpy8TWyXBLPCD+dT+n6BQ==;EndpointSuffix=core.windows.net";

        private readonly IBaseUnitOfWork _unitOfWork;


        public BlobFileService(IBaseUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public string UploadToBlob(BlobFile blobFile, byte[] fileBytes)
        {
            var azureAccount = CloudStorageAccount.Parse(_blobAccount);
            var blobClient = new CloudBlobClient(azureAccount.BlobStorageUri.PrimaryUri,
                new StorageCredentials("classics", 
                "aAh4x4MJpEDybKGMCyEnN/FmPyxrkER6nuUg/D4lqorKdEtCdih2XWRi1YrUNv8L4gpy8TWyXBLPCD+dT+n6BQ=="));

            var containerReference = blobClient.GetContainerReference(blobFile.Container);
            var blockBlob = containerReference.GetBlockBlobReference(blobFile.Name);
            blockBlob.Properties.ContentType = blobFile.MIME;           
            blockBlob.UploadFromByteArray(fileBytes, 0, fileBytes.Length);
            return blockBlob.Uri.AbsoluteUri;
        }

        public void AddBlobFile(BlobFile file)
        {
            _unitOfWork.BlobFileRepository.Add(file);
            _unitOfWork.Commit();
        }

    }
}
