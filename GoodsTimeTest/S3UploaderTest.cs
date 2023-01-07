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
                RegisterDate = DateTime.Now.ToString(),
                UpdateDate = DateTime.Now.ToString(),
            };
            new S3Uploader<Goods>().UploadGoodsAsync(obj).Wait();
        }
    }
}