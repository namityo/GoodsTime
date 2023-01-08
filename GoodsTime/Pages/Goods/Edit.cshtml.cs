using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;

namespace GoodsTime.Pages.Goods
{
    public class EditModel : PageModel
    {
		[BindProperty]
		public Models.Goods Goods { get; set; } = new Models.Goods();

		public async Task<IActionResult> OnGetAsync(int? id)
        {
			if (id.HasValue)
			{
				var r = await SelectGoods(id.Value);
				if(r != null)
				{
					Goods = r;
					return Page();
				}
			}

			return NotFound();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id.HasValue)
			{
				var r = await SelectGoods(id.Value);
				if (r != null)
				{
					// ‰æ–Ê•\Ž¦Žž‚ÌUpdateId‚ðŽæ“¾(ŠyŠÏ”r‘¼)
					var oldUpdateId = Goods.UpdateId;

					// XV‚·‚é“à—e‚ð‘‚«Š·‚¦‚é
					r.Number = Goods.Number;
					r.Description = Goods.Description;
					r.GetDate = Goods.GetDate;
					r.ReleaseDate = Goods.ReleaseDate;
					r.ReleaseFlag = Goods.ReleaseFlag;
					r.ReleaseDescription = Goods.ReleaseDescription;
					r.Refresh();

					// “o˜^ˆ—
					var cs = $"Data Source=db.sqlite;Version=3;";
					using (var connection = new SQLiteConnection(cs))
					using (var db = new QueryFactory(connection, new SqliteCompiler()))
					{
						db.Logger = compiled => {
							Console.WriteLine(compiled.ToString());
						};

						var result = await db.Query("Goods").Where("Id", r.Id).Where("UpdateId", oldUpdateId).UpdateAsync(r);

						return RedirectToPage("./Index");
					}
				}
			}

			return NotFound();
		}

		private async Task<Models.Goods?> SelectGoods(int id)
		{
			var cs = $"Data Source=db.sqlite;Version=3;";
			using (var connection = new SQLiteConnection(cs))
			using (var db = new QueryFactory(connection, new SqliteCompiler()))
			{
				db.Logger = compiled => {
					Console.WriteLine(compiled.ToString());
				};

				var result = await db.Query("Goods").Where("Id", id).GetAsync<Models.Goods>();
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
	}
}
