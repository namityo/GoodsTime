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
                var r = await SelectStocktakingEvent(id.Value);
                if (r != null)
                {
                    StocktakingEvent = r;

                    // íIâµëŒè€ÇÃÉAÉCÉeÉÄÇéÊìæ
                    Items = await SelectGoods();

                    // íIâµèÛãµÇéÊìæ
                    var events = await SelectStocktakingGoodsEvent(id.Value);
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
            // íIâµèÓïÒÇìoò^Ç∑ÇÈ
            var cs = $"Data Source=db.sqlite;Version=3;";
            using (var connection = new SQLiteConnection(cs))
            using (var db = new QueryFactory(connection, new SqliteCompiler()))
            {
                db.Logger = compiled => {
                    Console.WriteLine(compiled.ToString());
                };

                var result = await db.Query(nameof(Models.StocktakingGoodsEvent)).InsertAsync(dto);
            }

            return new JsonResult(new { success = "true" });
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

        private async ValueTask<IEnumerable<Models.StocktakingGoodsEvent>> SelectStocktakingGoodsEvent(int id)
        {
            var cs = $"Data Source=db.sqlite;Version=3;";
            using (var connection = new SQLiteConnection(cs))
            using (var db = new QueryFactory(connection, new SqliteCompiler()))
            {
                db.Logger = compiled => {
                    Console.WriteLine(compiled.ToString());
                };

                var result = await db.Query(nameof(Models.StocktakingGoodsEvent)).Where("StocktakingId", id).GetAsync<Models.StocktakingGoodsEvent>();
                return result.ToList();
            }
        }

        private async ValueTask<IEnumerable<Models.Goods>> SelectGoods()
        {
            var cs = $"Data Source=db.sqlite;Version=3;";
            using (var connection = new SQLiteConnection(cs))
            using (var db = new QueryFactory(connection, new SqliteCompiler()))
            {
                db.Logger = compiled => {
                    Console.WriteLine(compiled.ToString());
                };

                var result = await db.Query("Goods").OrderByDesc("UpdateDate").Where("ReleaseFlag", 0).GetAsync<Models.Goods>();
                return result.ToList();
            }
        }
    }
}
