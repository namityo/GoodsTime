using GoodsTime.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;

namespace GoodsTime.Pages.Goods
{
    public class PrintModel : PageModel
    {
        private readonly GoodsStore _goodsStore;

        private readonly S3Uploader<Models.Goods> _s3Uploader;

        public PrintModel(GoodsStore goodsStore, S3Uploader<Models.Goods> s3Uploader)
            => (_goodsStore, _s3Uploader) = (goodsStore, s3Uploader);

        [BindProperty]
		public IEnumerable<Models.Goods> Items { get; set; } = new List<Models.Goods>();

        [BindProperty]
        public IEnumerable<int> Targets { get; set; } = new List<int>();

		public IActionResult OnGet()
        {
            return RedirectToPage("/Goods/Index");
        }

        public async Task OnPostPrint()
        {
            if (Targets.Any())
            {
                Items = await _goodsStore.SelectAtAsync(Targets);

				// S3‚É“o˜^‚·‚é
				foreach (var item in Items)
				{
					await _s3Uploader.UploadGoodsAsync(item);
				}
			}
        }
    }
}
