using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;
using static System.Net.Mime.MediaTypeNames;

namespace GoodsTime.Pages.Goods
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Models.Goods Goods { get; set; } = new Models.Goods();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var now = DateTime.Now;

            Goods.RegisterDate = now;
            Goods.UpdateDate = now;

			// “o˜^ˆ—
			var cs = $"Data Source=db.sqlite;Version=3;";
			using (var connection = new SQLiteConnection(cs))
            using (var db = new QueryFactory(connection, new SqliteCompiler()))
            {
                db.Logger = compiled => {
                    Console.WriteLine(compiled.ToString());
                };

                await db.Query("Goods").InsertAsync(Goods);
			}

			return RedirectToPage("./Index");
        }
    }
}
