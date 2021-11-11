using CodingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Repository
{
    public class BusinessRuleService : IBusinessRuleService
    {
        private readonly List<BusinessRule> _rules;

        public BusinessRuleService(List<BusinessRule> rules)
        {
            _rules = rules;
        }


        public List<BusinessRule> InitializeRules()
        {
            BusinessRule[] rules = {
                new BusinessRule(){ PaymentFor="Physical product",Rule="Generate a packing slip for shipping"},
                new BusinessRule(){ PaymentFor="Book",Rule=" create a duplicate packing slip for the royalty department"},
                new BusinessRule(){ PaymentFor="Physical product",Rule="Generate a packing slip for shipping"},
                new BusinessRule(){ PaymentFor="Membership",Rule="Activate that membership"},
                new BusinessRule(){ PaymentFor="Upgrade to a membership,",Rule="Activate the membership"},
                new BusinessRule(){ PaymentFor="Membership or upgrade",Rule="E-mail the owner and inform them of the activation/upgrade"},
                new BusinessRule(){ PaymentFor="Video - Learning to Ski",Rule="Add a free “First Aid” video to the packing slip (the result of a court decision in 1997)"}
            };

            _rules.AddRange(rules);

            return _rules;
        }

        public string AddRule(BusinessRule rule)
        {
            _rules.Add(rule);
            return "Added";
        }

        public string RuleForPayment(string PaymentFor)
        {
            return _rules.Where(r => r.PaymentFor == PaymentFor).Select(r => r.Rule).FirstOrDefault();
        }
    }
}
