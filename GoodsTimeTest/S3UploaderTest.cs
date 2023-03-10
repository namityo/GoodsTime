using Amazon;
using GoodsTime.Context;
using GoodsTime.Models;

namespace GoodsTimeTest
{
    public class S3UploaderTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var obj = new Goods()
            {
                Id = 1,
                Number = "TESTHOGEHOGE000001-001",
                Description = "テストデータです",
                RegisterDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
            new S3Uploader<Goods>()
            {
                RegionEndpoint = RegionEndpoint.APNortheast1,
                BucketName = "consisthackathon2023app",
                KeyPrefix = "data/",
                KeySuffix = ".json",
            }.UploadGoodsAsync(obj).Wait();
        }
    }
}