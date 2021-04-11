using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.AzureApi
{
    public class BlobEntry
    {

        public string FileName { get; }
        public string Extension => GetFileExtension(FileName);
        public string ContentType => MIMEAssistant.GetMIMEType(FileName);
        public string Base64 => Convert.ToBase64String(ByteArray);
        public byte[] ByteArray => Stream.ToArray();
        public MemoryStream Stream { get; }

        public BlobEntry(MemoryStream memoryStream, string fileName)
        {
            Stream = memoryStream;
            FileName = fileName;
            Stream.Position = 0;
        }

        private string GetFileExtension(string fileName)
        {
            return FileName.Contains('.') == false ? "" : fileName.Substring(fileName.IndexOf('.') + 1);
        }
    }
}
