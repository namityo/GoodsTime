using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;

namespace GoodsTime.Pages.Goods
{
    public class IndexModel : PageModel
    {
		[BindProperty]
		public IEnumerable<Models.Goods> Items { get; set; } = new List<Models.Goods>();

        public void OnGet(int? release)
        {
			var cs = $"Data Source=db.sqlite;Version=3;";
			using (var connection = new SQLiteConnection(cs))
			using (var db = new QueryFactory(connection, new SqliteCompiler()))
			{
				db.Logger = compiled => {
					Console.WriteLine(compiled.ToString());
				};

				var query = db.Query("Goods").OrderByDesc("UpdateDate");
				query = release.HasValue ? query.Where("ReleaseFlag", release.Value) : query.Where("ReleaseFlag", 0);
				Items = query.Get<Models.Goods>().ToList();
			}
		}
    }
}
