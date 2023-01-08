using GoodsTime.Context;
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
        private readonly GoodsStore _goodsStore;

        public AddModel(GoodsStore goodsStore)
            => _goodsStore = goodsStore;

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

            await _goodsStore.AddAsync(Goods);

			return RedirectToPage("/Goods/Index");
        }
    }
}
