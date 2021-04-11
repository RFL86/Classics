using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Classics.AzureApi
{
    public class CloudStorage
    {
        public CloudStorageAccount StorageAccount { get; internal set; }
        public CloudBlobContainer BlobContainer { get; internal set; }
        public CloudBlobClient BlobClient { get; internal set; }

        public CloudStorage(string storageConnectionString, string blobContainer, bool createIfNotExists = true)
        {
            if (string.IsNullOrWhiteSpace(blobContainer))
                throw new Exception("O parâmetro 'blobContainer' não pode ser nulo, vazio ou em branco.");

            if (string.IsNullOrWhiteSpace(storageConnectionString))
                throw new Exception("O parâmetro 'storageConnectionString' não pode ser nulo, vazio ou em branco.");

            if (blobContainer.Any(char.IsUpper))
                throw new Exception("O parâmetro 'storageConnectionString' não pode conter caracteres em caixa alta.");

            StorageAccount = CloudStorageAccount.Parse(storageConnectionString);
            BlobClient = StorageAccount.CreateCloudBlobClient();
            BlobContainer = BlobClient.GetContainerReference(blobContainer);

            if (createIfNotExists)
                BlobContainer.CreateIfNotExists();
        }

        public BlobEntry DownloadToBlobEntry(string blobReference)
        {
            if (string.IsNullOrWhiteSpace(blobReference))
                throw new Exception("O parâmetro 'blobReference' não pode ser nulo, vazio ou em branco.");

            var memoryStream = new MemoryStream();
            var cloudBlockContainer = BlobContainer.GetBlockBlobReference(blobReference);
            cloudBlockContainer.DownloadToStream(memoryStream);
            return new BlobEntry(memoryStream, blobReference);
        }

        public async Task<BlobEntry> DownloadToBlobEntryAsyncBlobEntry(string blobReference)
        {
            if (string.IsNullOrWhiteSpace(blobReference))
                throw new Exception("O parâmetro 'blobReference' não pode ser nulo, vazio ou em branco.");

            var memoryStream = new MemoryStream();
            var cloudBlockContainer = BlobContainer.GetBlockBlobReference(blobReference);
            await cloudBlockContainer.DownloadToStreamAsync(memoryStream);
            return new BlobEntry(memoryStream, blobReference);
        }

        public MemoryStream DownloadToStream(string blobReference)
        {
            if (string.IsNullOrWhiteSpace(blobReference))
                throw new Exception("O parâmetro 'blobReference' não pode ser nulo, vazio ou em branco.");

            var memoryStream = new MemoryStream();
            var cloudBlockContainer = BlobContainer.GetBlockBlobReference(blobReference);
            cloudBlockContainer.DownloadToStream(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task<MemoryStream> DownloadToStreamAsync(string blobReference)
        {
            if (string.IsNullOrWhiteSpace(blobReference))
                throw new Exception("O parâmetro 'blobReference' não pode ser nulo, vazio ou em branco.");

            var memoryStream = new MemoryStream();
            var cloudBlockContainer = BlobContainer.GetBlockBlobReference(blobReference);
            await cloudBlockContainer.DownloadToStreamAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        public void UploadFromByteArray(byte[] file, string name, string contentType)
        {
            var blockBlob = BlobContainer.GetBlockBlobReference(name);
            blockBlob.Properties.ContentType = contentType;
            blockBlob.UploadFromByteArray(file, 0, file.Length);
        }

        public void UploadFromByteArray(byte[] file, string name)
        {
            UploadFromByteArray(file, name, GetContentType(name));
        }

        public async void UploadFromByteArrayAsync(byte[] file, string name, string contentType)
        {
            var blockBlob = BlobContainer.GetBlockBlobReference(name);
            blockBlob.Properties.ContentType = contentType;
            await blockBlob.UploadFromByteArrayAsync(file, 0, file.Length);
        }

        public async void UploadFromByteArrayAsync(byte[] file, string name)
        {
            UploadFromByteArrayAsync(file, name, GetContentType(name));
        }

        public async void UploadFromStreamAsync(Stream stream, string name, string contentType)
        {
            var blockBlob = BlobContainer.GetBlockBlobReference(name);
            blockBlob.Properties.ContentType = contentType;
            await blockBlob.UploadFromStreamAsync(stream);
        }

        public void UploadFromStream(Stream stream, string name, string contentType)
        {
            var blockBlob = BlobContainer.GetBlockBlobReference(name);
            blockBlob.Properties.ContentType = contentType;
            blockBlob.UploadFromStream(stream);
        }

        public void UploadFromStream(Stream stream, string name)
        {
            UploadFromStream(stream, name, GetContentType(name));
        }

        private string GetContentType(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return "";

            var extension = fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal));
            return MIMEAssistant.GetMIMEType(extension);
        }
    }
}
