using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using vimmvc.Common;

namespace vimmvc.Cloudinary
{
    public static class CloudinaryService
    {
        private static readonly IConfiguration _config;
        public static async Task<AssetResponse> UploadImage(string upload, MemoryStream encFile)
        {
            var cloudinary = new CloudinaryDotNet.Cloudinary(Environment.GetEnvironmentVariable(Constants.Cloudinary));
            cloudinary.Api.Secure = true;
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(upload, encFile),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
            };
            var uploadResult = cloudinary.Upload(uploadParams);

            var getResourceParams = new GetResourceParams(upload)
            {
                QualityAnalysis = true
            };
            var getResourseResult = cloudinary.GetResource(getResourceParams);
            var resultJson = getResourseResult.JsonObj;
            var transformation = cloudinary.Api.UrlImgUp.Transform(new Transformation()
                .Width(300).Crop("scale"));

            var imageUrl = transformation.BuildUrl(upload);
            var imageTag = transformation.BuildImageTag(upload);
            return new AssetResponse
            {
                ImageUrl = imageUrl,
                ImageTag = imageTag,
            };
         }

        private static async Task<CustomFile> CreateFile(this IFormFile file)
        {
            CustomFile ms = new CustomFile()
            {
                ContentLength = file.Length,
                ContentType = file.ContentType,
                Name = file.Name,
            };
            await file.CopyToAsync(ms.Content, new CancellationToken());
            return ms;
        }

        public static MemoryStream GetStream(this IFormFile formfile)
        {
            using var mermoryStream = new MemoryStream();
            formfile.CopyToAsync(mermoryStream);
            return mermoryStream;
        }

        public static byte[] GetByte(this IFormFile formfile)
        {
            using var mermoryStream = new MemoryStream();
            formfile.CopyToAsync(mermoryStream);
            return mermoryStream.ToArray();
        }
    }
}
