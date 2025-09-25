using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Helper
{
    public  class ImageUploadHelper
    {
        public static string _bucketName = "probook-notebooks";
        public static async Task<string> UploadFileAsync(IFormFile file,StorageClient storageClient)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var objectName = $"notebooks/{Guid.NewGuid()}_{file.FileName}";
            await storageClient.UploadObjectAsync(_bucketName, objectName, file.ContentType, memoryStream);

            return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
        }
    }
}
