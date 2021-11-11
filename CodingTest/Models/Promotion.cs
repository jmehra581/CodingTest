using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public int PromotionTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
