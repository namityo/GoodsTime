using System.ComponentModel;

namespace GoodsTime.Models
{
	public class StocktakingEvent
	{
		public int? Id { get; set; } = null;

		[DisplayName("棚卸名")]
		public string Name { get; set; } = string.Empty;

		public DateTime CreatedAt { get; set; } = DateTime.MinValue;
	}
}
