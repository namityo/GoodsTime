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
        private readonly StocktakingEventStore _store;

        public IndexModel(StocktakingEventStore store)
            => _store = store;

        [BindProperty]
        public IEnumerable<Models.StocktakingEvent> Items { get; set; } = new List<Models.StocktakingEvent>();

        public async void OnGet()
        {
            Items = await _store.SelectAsync();
        }
    }
}
