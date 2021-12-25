namespace WebApp.Models
{
    public class ProductResponse
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ProductId { get; set; } = null!;

        /// <summary>
        /// 説明
        /// </summary>
        public string Desc { get; set; } = null!;

        /// <summary>
        /// 商品名
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 価格
        /// </summary>
        public decimal Price { get; set; }
    }
}
