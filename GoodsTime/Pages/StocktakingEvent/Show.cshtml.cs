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
        private readonly GoodsStore _goodsStore;
        private readonly StocktakingEventStore _stocktakingEventStore;

        public ShowModel(GoodsStore goodsStore, StocktakingEventStore stocktakingEventStore)
            => (_goodsStore, _stocktakingEventStore) = (goodsStore, stocktakingEventStore);

        [BindProperty]
        public IEnumerable<Models.Goods> Items { get; set; } = new List<Models.Goods>();

        [BindProperty]
        public Models.StocktakingEvent StocktakingEvent { get; set; } = new Models.StocktakingEvent();

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id.HasValue)
            {
                var r = await _stocktakingEventStore.SelectAtAsync(id.Value);
                if (r != null)
                {
                    StocktakingEvent = r;

                    // ’I‰µƒAƒCƒeƒ€‚ðŽæ“¾
                    Items = await _goodsStore.SelectAtStocktakingAsync(id.Value);

                    return Page();
                }
            }

            return NotFound();
        }
    }
}
