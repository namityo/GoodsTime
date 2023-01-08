using GoodsTime.Context;
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

        public async void OnGet()
        {
            Items = await new StocktakingEventStore().SelectAsync();
        }
    }
}
