using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

            Goods.RegisterDate= now;
            Goods.UpdateDate= now;

            // TODO ìoò^èàóù

            return RedirectToPage("./Index");
        }
    }
}
