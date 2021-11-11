using CodingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Repository
{
    public class CartService : iCartService
    {
        private readonly List<CartItems> _list;

        public CartService(List<CartItems> list)
        {
            _list = list;
        }


        public string AddToCart(CartItems items)
        {
            _list.Add(items);
            return "Added";
        }

        public string RemoveFromCart(CartItems items)
        {
            _list.Remove(items);
            return "Removed";
        }

        public decimal CalculateCartValue()
        {
            throw new NotImplementedException();
        }

        public CartItems GetCartItems(int? cartItemsId)
        {
          return _list.Where(c => c.CartItemsId == cartItemsId).FirstOrDefault();
        }
    }
}
