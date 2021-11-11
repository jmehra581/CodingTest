using CodingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Repository
{
    public interface iPromotionService
    {
        public List<Promotion> InitializePromotions();
        public List<PromotionType> InitializePromotionTypes();
        public List<PromotionRule> InitializePromotionRules();
        public List<Promotion> GetActivePromotions();
        public string AddPromotion(Promotion promotion);
        public decimal ApplyPromotion();

    }
}
