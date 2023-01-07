using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace GoodsTime.Context
{
    public class S3Uploader<T>
    {
        public string BucketName { get; set; } = "consisthackathon2023app";

        public RegionEndpoint RegionEndpoint { get; set; } = RegionEndpoint.APNortheast1;

        public string RelativePath { get; set; } = "data";

        public async Task UploadGoodsAsync(T obj)
        {
            try
            {
                var data = ConvertJsonData(obj);
                var hash = CreateHash(data);

                var s3client = new AmazonS3Client(RegionEndpoint);
                var request = new PutObjectRequest()
                {
                    BucketName = BucketName,
                    Key = string.Concat("data", hash, ".json"),
                    InputStream = new MemoryStream(data)
                };

                var response = await s3client.PutObjectAsync(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public byte[] ConvertJsonData(T obj)
        {
            var json = JsonSerializer.Serialize<T>(obj);
            var data = Encoding.UTF8.GetBytes(json);
            return data;
        }

        public string CreateHash(byte[] data)
        {
            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(data);
            return string.Concat(hash.Select(b => $"{b:x2}"));
        }
    }
}
