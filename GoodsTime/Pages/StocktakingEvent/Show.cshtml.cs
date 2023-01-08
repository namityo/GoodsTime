using GoodsTime.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;

namespace GoodsTime.Pages.StocktakingEvent
{
    public class ShowModel : PageModel
    {
        [BindProperty]
        public IEnumerable<Models.Goods> Items { get; set; } = new List<Models.Goods>();

        [BindProperty]
        public Models.StocktakingEvent StocktakingEvent { get; set; } = new Models.StocktakingEvent();

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id.HasValue)
            {
                var r = await new StocktakingEventStore().SelectAtAsync(id.Value);
                if (r != null)
                {
                    StocktakingEvent = r;

                    // ’I‰µƒAƒCƒeƒ€‚ðŽæ“¾
                    Items = await new GoodsStore().SelectAtStocktakingAsync(id.Value);

                    return Page();
                }
            }

            return NotFound();
        }
    }
}
