using CodingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Repository
{
    public interface iCartService
    {
        public decimal CalculateCartValue();
        public string AddToCart(CartItems items);
        public string RemoveFromCart(CartItems items);
        public CartItems GetCartItems(int? cartItemsId);

    }
}
