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

        public void OnGet()
        {
			var cs = $"Data Source=db.sqlite;Version=3;";
			using (var connection = new SQLiteConnection(cs))
			using (var db = new QueryFactory(connection, new SqliteCompiler()))
			{
				Items = db.Query("Goods").Get<Models.Goods>().ToList();
			}
		}
    }
}
