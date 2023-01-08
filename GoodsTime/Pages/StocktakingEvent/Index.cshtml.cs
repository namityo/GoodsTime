using GoodsTime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;

namespace GoodsTime.Pages.StocktakingEvent
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IEnumerable<Models.StocktakingEvent> Items { get; set; } = new List<Models.StocktakingEvent>();

        public void OnGet()
        {
            var cs = $"Data Source=db.sqlite;Version=3;";
            using (var connection = new SQLiteConnection(cs))
            using (var db = new QueryFactory(connection, new SqliteCompiler()))
            {
                db.Logger = compiled => {
                    Console.WriteLine(compiled.ToString());
                };

                Items = db.Query(nameof(Models.StocktakingEvent)).OrderByDesc("CreatedAt").Get<Models.StocktakingEvent>().ToList();
            }
        }
    }
}
