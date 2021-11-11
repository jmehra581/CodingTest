using CodingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Repository
{
    public interface IBusinessRuleService
    {
        public List<BusinessRule> InitializeRules();
        public string RuleForPayment(string PaymentFor);
        public string AddRule(BusinessRule rule);
    }
}
