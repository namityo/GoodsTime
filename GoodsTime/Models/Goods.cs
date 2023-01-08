using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GoodsTime.Models
{
    public class Goods
    {
        public int? Id { get; set; } = null;

        [DisplayName("設備番号")]
        public string Number { get; set; } = string.Empty;

        [DisplayName("設備説明")]
        public string Description { get; set; } = string.Empty;

        [DisplayName("取得日")]
        public string GetDate { get; set; } = string.Empty;

        [DisplayName("破棄予定日")]
        public string ReleaseDate { get; set; } = string.Empty;

        [DisplayName("破棄")]
        public bool ReleaseFlag { get; set; } = false;

        [DisplayName("破棄日")]
        public string ReleasedDate { get; set; } = string.Empty;

        [DisplayName("破棄理由")]
        public string ReleaseDescription { get; set; } = string.Empty;

		public string RegisterDate { get; set; } = DateTime.MinValue.ToString("yyyy/MM/dd HH:mm:ss");

        public string UpdateDate { get; set; } = DateTime.MinValue.ToString("yyyy/MM/dd HH:mm:ss");

        public string UpdateId { get; set; } = Guid.NewGuid().ToString();

        public void Refresh()
        {
            UpdateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            UpdateId = Guid.NewGuid().ToString();
        }
    }
}
