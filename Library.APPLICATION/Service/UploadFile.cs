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
        private readonly Cloudinary _cloudinary;

        public UploadFile(IConfiguration config)
        {
            var acc = new Account(
                config.GetSection("CloudinarySettings")["CloudName"],
                config.GetSection("CloudinarySettings")["ApiKey"],
                config.GetSection("CloudinarySettings")["ApiSecret"]
            );
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<string> UploadPdfAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null");

            using var stream = file.OpenReadStream();
            var uploadParams = new RawUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "pdf_files",          // مجلد مخصص داخل Cloudinary
                UseFilename = true,            // استخدم نفس اسم الملف
                UniqueFilename = false,        // لا تضف رموز عشوائية للاسم
                AccessMode = "public",         // ✅ مهم جدًا لمنع Blocked for delivery
               
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            // التحقق من نجاح الرفع
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                return result.SecureUrl?.ToString();

            throw new Exception($"Upload failed: {result.Error?.Message}");
        }

    }
}
