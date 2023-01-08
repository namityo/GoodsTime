using GoodsTime.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;
using System.Linq;

namespace GoodsTime.Pages.StocktakingEvent
{
    [IgnoreAntiforgeryToken]
    public class TakingModel : PageModel
    {
        private readonly GoodsStore _goodsStore;
        private readonly StocktakingEventStore _stocktakingEventStore;
        private readonly StocktakingGoodsEventStore _stocktakingGoodsEventStore;

        public TakingModel(
            GoodsStore goodsStore,
            StocktakingEventStore stocktakingEventStore,
            StocktakingGoodsEventStore stocktakingGoodsEventStore)
            => (_goodsStore, _stocktakingEventStore, _stocktakingGoodsEventStore)
                = (goodsStore, stocktakingEventStore, stocktakingGoodsEventStore);

        [BindProperty]
        public IEnumerable<Models.Goods> Items { get; set; } = new List<Models.Goods>();

        [BindProperty]
        public Models.StocktakingEvent StocktakingEvent { get; set; } = new Models.StocktakingEvent();

        [BindProperty]
        public HashSet<int> TakingEvents { get; set; } = new HashSet<int>();


        public async Task<IActionResult> OnGet(int? id)
        {
            if (id.HasValue)
            {
                var r = await _stocktakingEventStore.SelectAtAsync(id.Value);
                if (r != null)
                {
                    StocktakingEvent = r;

                    // íIâµëŒè€ÇÃÉAÉCÉeÉÄÇéÊìæ
                    Items = await _goodsStore.SelectAsync();

                    // íIâµèÛãµÇéÊìæ
                    var events = await _stocktakingGoodsEventStore.SelectAtStocktakingEventAsync(id.Value);
                    if (events != null && events.Any())
                    {
                        TakingEvents = events.Select((e) => e.GoodsId).ToHashSet();
                    }

                    return Page();
                }
            }

            return NotFound();
        }

        /// <summary>
        /// ajaxÇ≈åƒÇŒÇÍÇÈèàóù
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostTaking([FromBody]Models.StocktakingGoodsEvent dto)
        {
            await _stocktakingGoodsEventStore.Add(dto);

            return new JsonResult(new { success = "true" });
        }
    }
}
