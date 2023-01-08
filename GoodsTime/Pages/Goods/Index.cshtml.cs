using GoodsTime.Context;
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

        public async Task OnGet(int? release)
        {
            Items = await new GoodsStore().SelectAsync(release.HasValue ? release.Value : 0);
		}
    }
}
