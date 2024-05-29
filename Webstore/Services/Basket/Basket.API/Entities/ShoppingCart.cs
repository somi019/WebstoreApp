namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string Username { get; set; }
        public List<ShoppingCartItem> items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart() { 
            
        }
        public ShoppingCart(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach(var item in items)
                {
                    totalPrice += item.Price;
                }
                return totalPrice;
            }
        }
    }
}
