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
                var r = await new StocktakingEventStore().SelectAtAsync(id.Value);
                if (r != null)
                {
                    StocktakingEvent = r;

                    // �I���Ώۂ̃A�C�e�����擾
                    Items = await new GoodsStore().SelectAsync();

                    // �I���󋵂��擾
                    var events = await new StocktakingGoodsEventStore().SelectAtStocktakingEventAsync(id.Value);
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
        /// ajax�ŌĂ΂�鏈��
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostTaking([FromBody]Models.StocktakingGoodsEvent dto)
        {
            await new StocktakingGoodsEventStore().Add(dto);

            return new JsonResult(new { success = "true" });
        }
    }
}
