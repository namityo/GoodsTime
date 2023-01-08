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
                var r = await SelectStocktakingEvent(id.Value);
                if (r != null)
                {
                    StocktakingEvent = r;

                    // ’I‰µƒAƒCƒeƒ€‚ðŽæ“¾
                    Items = await SelectGoods(id.Value);

                    return Page();
                }
            }

            return NotFound();
        }

        private async ValueTask<Models.StocktakingEvent?> SelectStocktakingEvent(int id)
        {
            var cs = $"Data Source=db.sqlite;Version=3;";
            using (var connection = new SQLiteConnection(cs))
            using (var db = new QueryFactory(connection, new SqliteCompiler()))
            {
                db.Logger = compiled => {
                    Console.WriteLine(compiled.ToString());
                };

                var result = await db.Query(nameof(Models.StocktakingEvent)).Where("Id", id).GetAsync<Models.StocktakingEvent>();
                if (result.Any())
                {
                    return result.First();
                }
                else
                {
                    return null;
                }
            }
        }

        private async ValueTask<IEnumerable<Models.Goods>> SelectGoods(int stocktakingId)
        {
            var cs = $"Data Source=db.sqlite;Version=3;";
            using (var connection = new SQLiteConnection(cs))
            using (var db = new QueryFactory(connection, new SqliteCompiler()))
            {
                db.Logger = compiled => {
                    Console.WriteLine(compiled.ToString());
                };

                var result = await db.Query("Goods")
                    .Join("StocktakingGoodsEvent", "Goods.Id", "StocktakingGoodsEvent.GoodsId")
                    .Where("StocktakingGoodsEvent.StocktakingId", stocktakingId)
                    .GetAsync<Models.Goods>();
                return result.ToList();
            }
        }
    }
}
