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
    }
}
