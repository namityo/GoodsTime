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

        public PrintModel(GoodsStore goodsStore)
            => _goodsStore = goodsStore;

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
				var uploader = new S3Uploader<Models.Goods>();
				foreach (var item in Items)
				{
					await uploader.UploadGoodsAsync(item);
				}
			}
        }
    }
}
