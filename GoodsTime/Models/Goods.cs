using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public DateTime? GetDate { get; set; } = null;

        [DisplayName("破棄予定日")]
        public DateTime? ReleaseDate { get; set; } = null;

        [DisplayName("破棄")]
        public bool ReleaseFlag { get; set; } = false;

        [DisplayName("破棄日")]
        public DateTime? ReleasedDate { get; set; } = null;

        [DisplayName("破棄理由")]
        public string ReleaseDescription { get; set; } = string.Empty;

        [DisplayName("登録日時")]
		public DateTime RegisterDate { get; set; } = DateTime.MinValue;

		[DisplayName("最終更新日時")]
		public DateTime UpdateDate { get; set; } = DateTime.MinValue;

        public string UpdateId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 更新用のモデル生成
        /// </summary>
        /// <returns></returns>
        public UpdateModel AsUpdateModel() => new UpdateModel()
        {
            Number = Number,
            Description = Description,
            GetDate = GetDate,
            ReleaseDate = ReleaseDate,
            ReleaseFlag = ReleaseFlag,
            ReleasedDate = ReleasedDate,
            ReleaseDescription = ReleaseDescription,
            UpdateDate = DateTime.Now,
            UpdateId = Guid.NewGuid().ToString(),
        };

        public class UpdateModel
        {
            public string Number { get; set; } = string.Empty;

            public string Description { get; set; } = string.Empty;

            public DateTime? GetDate { get; set; }

            public DateTime? ReleaseDate { get; set; }

            public bool ReleaseFlag { get; set; }

            public DateTime? ReleasedDate { get; set; }

            public string ReleaseDescription { get; set; } = string.Empty; 

            public DateTime UpdateDate { get; set; }

            public string UpdateId { get; set; } = string.Empty; 
        }
    }
}
