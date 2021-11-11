using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingTest.Models;
using CodingTest.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CodingTest.Controllers
{
    public class BusinessRuleController : Controller
    {

        private readonly IBusinessRuleService _ruleService;

        public BusinessRuleController(IBusinessRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BusinessRule>> InitializeRules()
        {
            var rules = _ruleService.InitializeRules();
            return Ok(rules);
        }

        [HttpPost]
        public string AddRule(BusinessRule bRule)
        {

            if (bRule == null)
            {
                return "Business Rule is empty";
            }

            if (!ModelState.IsValid)
            {
                return "Validations failed";
            }

            return _ruleService.AddRule(bRule);

        }


        [HttpGet]
        public string RuleForPayment(string? paymentFor)
        {
            if (paymentFor == null)
            {
                return "Data not found";
            }

            return _ruleService.RuleForPayment(paymentFor);
        }



    }
}
