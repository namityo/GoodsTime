using System.Text.Json;
using System.Text;
using System.Security.Cryptography;

namespace GoodsTime.Models.Extensions
{
    public static class GoodsExtensions
    {
        public static byte[] ConvertJsonData<T>(this T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            var data = Encoding.UTF8.GetBytes(json);
            return data;
        }

        public static string CreateHash(this byte[] data)
        {
            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(data);
            return string.Concat(hash.Select(b => $"{b:x2}"));
        }

        public static string CreateUrl<T>(this T obj)
        {
            var hash = obj.ConvertJsonData().CreateHash();
            return CreateUrl(hash);
		}

        public static string CreateUrl(string hash)
		{
			var s3url = "http://consisthackathon2023app.s3-website-ap-northeast-1.amazonaws.com/?id=";
			return string.Concat(s3url, hash);
		}
    }
}
