using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using GoodsTime.Models.Extensions;

namespace GoodsTime.Context
{
    public class S3Uploader<T>
    {
        public string BucketName { get; set; } = string.Empty;

        public RegionEndpoint RegionEndpoint { get; set; } = RegionEndpoint.APNortheast1;

        public string KeyPrefix { get; set; } = string.Empty;

        public string KeySuffix { get; set; } = string.Empty;

        public async Task UploadGoodsAsync(T obj)
        {
            try
            {
                var data = obj.ConvertJsonData();
                var hash = data.CreateHash();

                var s3client = new AmazonS3Client(RegionEndpoint);
                var request = new PutObjectRequest()
                {
                    BucketName = BucketName,
                    Key = string.Concat(KeyPrefix, hash, KeySuffix),
                    InputStream = new MemoryStream(data)
                };

                var response = await s3client.PutObjectAsync(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
