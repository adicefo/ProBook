using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProBook.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Helper
{
    public class ImageUploadHelper
    {
        private readonly GoogleCloudSettings _settings;

        public ImageUploadHelper(IOptions<GoogleCloudSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            // Load credentials from config
            var credential = Google.Apis.Auth.OAuth2.GoogleCredential
                .FromFile(_settings.CredentialPath);

            var storageClient = await StorageClient.CreateAsync(credential);

            var objectName = $"_{file.FileName}";
            await storageClient.UploadObjectAsync(
                _settings.BucketName,
                objectName,
                file.ContentType,
                memoryStream
            );

            return $"https://storage.googleapis.com/{_settings.BucketName}/{objectName}";
        }
    }

}
