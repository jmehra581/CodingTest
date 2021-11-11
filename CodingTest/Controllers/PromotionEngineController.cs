using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingTest.Models;
using CodingTest.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionEngineController : ControllerBase
    {
        private readonly iCartService _cartService;
        private readonly iPromotionService _promotionService;
        private readonly IStockService _stockService;
 
        public PromotionEngineController(iCartService cartService, iPromotionService promotionService, IStockService stockService)
        {
            _cartService = cartService;
            _promotionService = promotionService;
            _stockService = stockService;

        }

      

        List<Stock> stocks;
        CartItems items;
        List<Promotion> promotions;





        [HttpGet]
        public ActionResult<IEnumerable<Stock>> InitializeSKU()
        {
            _stockService.InitializeSKU(stocks);
            return Ok();
        }


        [HttpPost]
        public string AddToCart([FromBody] CartItems items)
        {
            if (items == null)
            {
                return "Cart items are empty";
            }

            if (!ModelState.IsValid)
            {
                return "Validations failed";
            }

            return _cartService.AddToCart(items);
        }

        [HttpPost]
        public string RemoveFromCart([FromBody] CartItems items)
        {
            if (items == null)
            {
                return "Cart items are empty";
            }

            if (!ModelState.IsValid)
            {
                return "Validations failed";
            }

            return _cartService.RemoveFromCart(items);
        }

        [HttpGet]
        public ActionResult<CartItems> GetCartItems(int? cartItemId)
        {
            if (cartItemId == null)
            {
                return NotFound();
            }

            items = _cartService.GetCartItems(cartItemId);
            return Ok(items);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Promotion>> InitializePromotions()
        {
            var promos = _promotionService.InitializePromotions();
            return Ok(promos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<PromotionType>> InitializePromotionTypes()
        {
            var types = _promotionService.InitializePromotionTypes();
            return Ok(types);
        }

        [HttpGet]
        public ActionResult<IEnumerable<PromotionRule>> InitializePromotionRules()
        {
            var rules = _promotionService.InitializePromotionRules();
            return Ok(rules);
        }


        [HttpGet]
        public ActionResult<IEnumerable<Promotion>> GetActivePromotions()
        {
            var promotions = _promotionService.GetActivePromotions();
            return Ok(promotions);
        }

        [HttpPost]
        public string AddPromotion(Promotion promotion)
        {

            if (items == null)
            {
                return "Promotion is empty";
            }

            if (!ModelState.IsValid)
            {
                return "Validations failed";
            }

            return _promotionService.AddPromotion(promotion);

        }




        [HttpPost]
        public string CheckOut()
        {
            decimal totalAmount = _promotionService.ApplyPromotion();
            return "Checked Out with Amount " + totalAmount.ToString();
        }




 
    }
}
