using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.Service
{
    public class UploadFile
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public UploadFile(IConfiguration config)
        {
            _connectionString = config.GetSection("AzureStorage")["ConnectionString"];
            _containerName = config.GetSection("AzureStorage")["ContainerName"];
        }

        public async Task<string> UploadPdfAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null");

            // إنشاء client للحاوية
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // التأكد من وجود الحاوية
            await containerClient.CreateIfNotExistsAsync();

            // تعيين اسم الملف
            var fileName= Guid.NewGuid().ToString() + file.FileName;
            var blobClient = containerClient.GetBlobClient(fileName);


            // تحديد نوع المحتوى
            var blobHttpHeaders = new BlobHttpHeaders
            {
                ContentType = file.ContentType // مثال: "application/pdf"
            };


            // رفع الملف
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobUploadOptions
                {
                    HttpHeaders = blobHttpHeaders
                });
            }

            // رابط الملف بعد الرفع
            return blobClient.Uri.ToString();
        }
        public async Task<bool> DeleteFileAsync(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
                throw new ArgumentException("File URL is empty or null");

            // إنشاء client للحاوية
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // استخراج اسم الملف من الرابط الكامل
            var uri = new Uri(fileUrl);
            var fileName = Path.GetFileName(uri.LocalPath);

            // الحصول على الـ BlobClient
            var blobClient = containerClient.GetBlobClient(fileName);

            // حذف الملف إن وجد
            var result = await blobClient.DeleteIfExistsAsync();

            return result.Value; // يرجع true إذا تم الحذف بنجاح
        }

    }
}
