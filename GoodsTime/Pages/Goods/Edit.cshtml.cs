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
				var r = await new GoodsStore().SelectAtAsync(id.Value);
                if (r != null)
				{
					// ��ʕ\������UpdateId���擾(�y�ϔr��)
					var oldUpdateId = Goods.UpdateId;

					// �X�V������e������������
					r.Number = Goods.Number;
					r.Description = Goods.Description;
					r.GetDate = Goods.GetDate;
					r.ReleaseDate = Goods.ReleaseDate;
					r.ReleaseFlag = Goods.ReleaseFlag;
					r.ReleaseDescription = Goods.ReleaseDescription;
					r.Refresh();

					// �o�^����
					var result = new GoodsStore().UpdateAtAsync(r, oldUpdateId);

                    return RedirectToPage("/Goods/Index");
                }
			}

			return NotFound();
		}
	}
}
