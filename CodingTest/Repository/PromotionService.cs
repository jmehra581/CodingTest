using CodingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Repository
{
    public class PromotionService : iPromotionService
    {
        private readonly List<Promotion> _listPromotion;
        private readonly List<PromotionRule> _rules;
        private readonly List<PromotionType> _types;
        private readonly List<CartItems> _items;

        public PromotionService(List<Promotion> listPromotion, List<PromotionRule> rules, List<PromotionType> types, List<CartItems> items)
        {
            _listPromotion = listPromotion;
            _rules = rules;
            _types = types;
            _items = items;
        }

        public string AddPromotion(Promotion promotion)
        {
            _listPromotion.Add(promotion);
            return "Added";
        }

        public decimal ApplyPromotion()
        {
            decimal total = 0;

            var count = _items.Count();

            var ruleA = _rules.Where(r => r.SKU_Id == "A").FirstOrDefault();
            var ruleB = _rules.Where(r => r.SKU_Id == "B").FirstOrDefault();
            var ruleC = _rules.Where(r => r.SKU_Id == "C").FirstOrDefault();
            var ruleD = _rules.Where(r => r.SKU_Id == "D").FirstOrDefault();

            var itemA = _items.Where(s => s.SKU_Id.Contains("A")).ToList();
            var itemB = _items.Where(s => s.SKU_Id.Contains("B")).ToList();
            var itemC = _items.Where(s => s.SKU_Id.Contains("C")).ToList();
            var itemD = _items.Where(s => s.SKU_Id.Contains("D")).ToList();

            decimal valueA = 0;
            decimal valueB = 0;
            decimal valueC = 0;
            decimal valueD = 0;

            //Logic for A
            if (itemA != null)
            {

                if (itemA.Count() < ruleA.Quantity)
                {
                    valueA = itemA.Count() * ruleA.Value;
                }
                else if (itemA.Count() > ruleA.Quantity)
                {
                    valueA = (Math.Floor((decimal)itemA.Count() / (decimal)ruleA.Quantity) * ruleA.Quantity) + ((itemA.Count() % ruleA.Quantity) * ruleA.Value);
                }
            }

            //Logic for B

            if (itemB != null)
            {

                if (itemB.Count() < ruleB.Quantity)
                {
                    valueB = itemB.Count() * ruleB.Value;
                }
                else if (itemB.Count() > ruleB.Quantity)
                {
                    valueB = (Math.Floor((decimal)itemB.Count() / (decimal)ruleB.Quantity) * ruleB.Quantity) + ((itemB.Count() % ruleB.Quantity) * ruleB.Value);
                }
            }


            //Logic for C & D
            if (itemC != null && itemD == null)
            {
                valueC = itemC.Count() * ruleC.Value;
            }

            else if (itemC == null && itemD != null)
            {
                valueD = itemD.Count() * ruleD.Value;
            }

            else if (itemC != null && itemD != null)
            {
                if (itemC.Count() == itemD.Count())
                {
                    valueC = 0;
                    valueD = itemD.Count() * ruleD.Value;
                }
            }




            total = valueA + valueB + valueC + valueD;

            return total;
        }

        public List<Promotion> GetActivePromotions()
        {
            return _listPromotion.Where(p => p.IsActive == true).ToList();

        }

        public List<PromotionRule> InitializePromotionRules()
        {
            PromotionRule[] rules = {
                new PromotionRule(){ PromotionRuleId=1,PromotionTypeId=1,Quantity=3,SKU_Id="A",Value=130},
                new PromotionRule(){ PromotionRuleId=2,PromotionTypeId=1,Quantity=2,SKU_Id="B",Value=45},
                new PromotionRule(){ PromotionRuleId=3,PromotionTypeId=2,Quantity=1,SKU_Id="C",Value=0},
                new PromotionRule(){ PromotionRuleId=4,PromotionTypeId=2,Quantity=1,SKU_Id="D",Value=30}

            };

            _rules.AddRange(rules);

            return _rules;
        }

        public List<Promotion> InitializePromotions()
        {
            Promotion[] promotions = {
                new Promotion(){ PromotionId=1,PromotionTypeId=1,IsActive=true},
                new Promotion(){ PromotionId=2,PromotionTypeId=1,IsActive=true},
                new Promotion(){ PromotionId=3,PromotionTypeId=2,IsActive=true}
            };

            _listPromotion.AddRange(promotions);
            return _listPromotion;
        }

        public List<PromotionType> InitializePromotionTypes()
        {
            PromotionType[] types = {
                new PromotionType(){ PromotionTypeId=1,PromotionTypeName="NTimesStockPromotion"},
                new PromotionType(){ PromotionTypeId=2,PromotionTypeName="MultipleStockFixedPromotion"}
            };

            _types.AddRange(types);

            return _types;
        }
    }
}
