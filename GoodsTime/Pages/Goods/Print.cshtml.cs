using GoodsTime.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;

namespace GoodsTime.Pages.Goods
{
    public class PrintModel : PageModel
    {
		[BindProperty]
		public IEnumerable<Models.Goods> Items { get; set; } = new List<Models.Goods>();

        [BindProperty]
        public IEnumerable<int> Targets { get; set; } = new List<int>();

		public IActionResult OnGet()
        {
            return RedirectToPage("./Index");
        }

        public async Task OnPostPrint()
        {
            if (Targets.Any())
            {
                var result = await SelectItems();

                // BindProperty‚É“o˜^
                Items = result.ToList();

				// S3‚É“o˜^‚·‚é
				var uploader = new S3Uploader<Models.Goods>();
				foreach (var item in Items)
				{
					await uploader.UploadGoodsAsync(item);
				}
			}
        }

        private async Task<IEnumerable<Models.Goods>> SelectItems()
        {
            var cs = $"Data Source=db.sqlite;Version=3;";
            using (var connection = new SQLiteConnection(cs))
            using (var db = new QueryFactory(connection, new SqliteCompiler()))
            {
                db.Logger = compiled => {
                    Console.WriteLine(compiled.ToString());
                };

                return await db.Query("Goods").WhereIn("Id", Targets).GetAsync<Models.Goods>();
            }
        }
    }
}
