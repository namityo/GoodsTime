namespace GoodsTime.Models
{
    public class Goods
    {
        public int Id { get; set; } = 0;

        public string Number { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime RegisterDate { get; set; } = DateTime.MinValue;

        public DateTime UpdateDate { get; set; } = DateTime.MinValue;

        public string UpdateId { get; set; } = Guid.NewGuid().ToString();
    }
}
