using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Models
{
    public class CartItems
    {
        public int CartItemsId { get; set; }
        public string SKU_Id { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
    }
}
