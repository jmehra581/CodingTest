using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Models
{
    public class PromotionRule
    {
        public int PromotionRuleId { get; set; }
        public int PromotionTypeId { get; set; }
        public int Quantity { get; set; }
        public string SKU_Id { get; set; }
        public decimal Value { get; set; }
    }
}
