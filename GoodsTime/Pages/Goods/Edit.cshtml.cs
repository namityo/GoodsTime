using GoodsTime.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;

namespace GoodsTime.Pages.Goods
{
    public class EditModel : PageModel
    {
		[BindProperty]
		public Models.Goods Goods { get; set; } = new Models.Goods();

		[BindProperty]
		public IEnumerable<Models.StocktakingEvent> StocktakingEvent { get; set; } = new List<Models.StocktakingEvent>();


		public async Task<IActionResult> OnGetAsync(int? id)
        {
			if (id.HasValue)
			{
				var r = await new GoodsStore().SelectAtAsync(id.Value);
				if(r != null)
				{
					Goods = r;

					StocktakingEvent = await new StocktakingEventStore().SelectAtGoodsAsync(id.Value);

                    return Page();
				}
			}

			return NotFound();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id.HasValue)
			{
				var updateModel = await new GoodsStore().SelectAtAsync(id.Value);
                if (updateModel != null)
				{
					// 画面表示時のUpdateIdを取得(楽観排他)
					var oldUpdateId = Goods.UpdateId;

					// 登録処理
					var result = new GoodsStore().UpdateAtAsync(id.Value, oldUpdateId, Goods.AsUpdateModel());

                    return RedirectToPage("/Goods/Index");
                }
			}

			return NotFound();
		}
	}
}
