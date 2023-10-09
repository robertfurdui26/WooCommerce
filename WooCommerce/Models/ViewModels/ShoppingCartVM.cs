namespace WooCommerce.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoopingCart> ShoopingCartsList { get; set; }

        public OrderHeader OrderHeader { get; set; }
    }
}
