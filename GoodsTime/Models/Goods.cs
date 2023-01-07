namespace GoodsTime.Models
{
    public class Goods
    {
        public int? Id { get; set; } = null;

        public string Number { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string RegisterDate { get; set; } = DateTime.MinValue.ToString();

        public string UpdateDate { get; set; } = DateTime.MinValue.ToString();

        public string UpdateId { get; set; } = Guid.NewGuid().ToString();

        public void Refresh()
        {
            UpdateDate = DateTime.Now.ToString();
            UpdateId = Guid.NewGuid().ToString();
        }
    }
}
