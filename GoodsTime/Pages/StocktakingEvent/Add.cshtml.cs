using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;

namespace GoodsTime.Pages.StocktakingEvent
{
    public class AddModel : PageModel
	{
		[BindProperty]
		public Models.StocktakingEvent Event { get; set; } = new Models.StocktakingEvent();

		public void OnGet()
        {
        }

		public async Task<IActionResult> OnPostAsync()
		{
			var now = DateTime.Now;

			Event.CreatedAt = now;

			// “o˜^ˆ—
			var cs = $"Data Source=db.sqlite;Version=3;";
			using (var connection = new SQLiteConnection(cs))
			using (var db = new QueryFactory(connection, new SqliteCompiler()))
			{
				db.Logger = compiled => {
					Console.WriteLine(compiled.ToString());
				};

				await db.Query(nameof(Models.StocktakingEvent)).InsertAsync(Event);
			}

			return RedirectToPage("/StocktakingEvent/Index");
		}
	}
}
