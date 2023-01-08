using GoodsTime.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;

namespace GoodsTime.Pages.StocktakingEvent
{
    public class AddModel : PageModel
	{
		private readonly StocktakingEventStore _store;

		public AddModel(StocktakingEventStore store)
			=> _store = store;

		[BindProperty]
		public Models.StocktakingEvent Event { get; set; } = new Models.StocktakingEvent();

		public void OnGet()
        {
        }

		public async Task<IActionResult> OnPostAsync()
		{
			var now = DateTime.Now;

			Event.CreatedAt = now;

			await _store.AddAsync(Event);

			return RedirectToPage("/StocktakingEvent/Index");
		}
	}
}
