using CodingTest.Controllers;
using CodingTest.Models;
using CodingTest.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestProject
{
    public class PromotionEngineUnitTest
    {
        //private readonly BookService _service;
        private readonly PromotionEngineController _controller;
        private readonly CartService _cartService;
        private readonly PromotionService _promotionService;
        private readonly StockService _stockService;

        List<CartItems> list;
        List<Promotion> listPromotion;
        List<PromotionRule> rules;
        List<PromotionType> types;
        List<Stock> listStock;


        public PromotionEngineUnitTest()
        {
            _cartService = new CartService(list);
            _promotionService = new PromotionService(listPromotion, rules, types, list);
            _stockService = new StockService(listStock);

            _controller = new PromotionEngineController(_cartService, _promotionService, _stockService);
        }


        [Fact]
        public void InitializeSKU_Test()
        {
            //Arrange

            //Act
            var result = _controller.InitializeSKU();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<Stock>>(list.Value);

            var listStocks = list.Value as List<Stock>;
            Assert.Equal(4, listStocks.Count);
        }


        [Fact]
        public void AddToCart_Test()
        {
            //CORRECT TEST START

            //Arrange
            var item = new CartItems()
            {
                CartId = 1,
                CartItemsId = 1,
                Quantity = 1,
                SKU_Id = "A"
            };

            //Act
            var createdResponse = _controller.AddToCart(item);

            //Assert
            Assert.IsType<string>(createdResponse);

            //value of the result
            var result = createdResponse as string;
            Assert.Equal("Added", result);

            //CORRECT TEST END




            //BADREQUEST AND MODELSTATE ERROR START

            //Arrange

            //Act
            //_controller.ModelState.AddModelError("Title", "Title is a requried filed");
            string response = _controller.AddToCart(null);

            //Assert
            Assert.Equal("Cart items are empty", response);


            //Arrange
            var erroritem = new CartItems()
            {
                CartId = 1,
                CartItemsId = 1,
                Quantity = 1

            };

            //Act
            _controller.ModelState.AddModelError("SKU_Id", "SKU_Id is requried.");
            var badResponse = _controller.AddToCart(erroritem);

            //Assert
            Assert.Equal("Validations failed", badResponse);

            //BADREQUEST AND MODELSTATE ERROR END
        }


        [Fact]
        public void RemoveFromCart_Test()
        {
            //CORRECT TEST START

            //Arrange
            var item = new CartItems()
            {
                CartId = 1,
                CartItemsId = 1,
                Quantity = 1,
                SKU_Id = "A"
            };

            //Act
            var createdResponse = _controller.RemoveFromCart(item);

            //Assert
            Assert.IsType<string>(createdResponse);

            //value of the result
            var result = createdResponse as string;
            Assert.Equal("Removed", result);

            //CORRECT TEST END




            //BADREQUEST AND MODELSTATE ERROR START

            //Arrange

            //Act
            //_controller.ModelState.AddModelError("Title", "Title is a requried filed");
            string response = _controller.RemoveFromCart(null);

            //Assert
            Assert.Equal("Cart items are empty", response);


            //Arrange
            var erroritem = new CartItems()
            {
                CartId = 1,
                CartItemsId = 1,
                Quantity = 1

            };

            //Act
            _controller.ModelState.AddModelError("SKU_Id", "SKU_Id is requried.");
            var badResponse = _controller.RemoveFromCart(erroritem);

            //Assert
            Assert.Equal("Validations failed", badResponse);

            //BADREQUEST AND MODELSTATE ERROR END
        }


        [Theory]
        [InlineData(1, null)]
        public void GetCartItems_Test(int cartItemId, int? incorrectCartItemId)
        {
            //Arrange
            int validId = cartItemId;
            int? invalidId = incorrectCartItemId;

            //Act
            var notFoundResult = _controller.GetCartItems(invalidId);
            var okResult = _controller.GetCartItems(validId);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
            Assert.IsType<OkObjectResult>(okResult.Result);


            //Now we need to check the value of the result for the ok object result.
            var item = okResult.Result as OkObjectResult;

            //We need cart item
            Assert.IsType<CartItems>(item.Value);

            //Checking values
            var cartItem = item.Value as CartItems;
            Assert.Equal(validId, cartItem.CartItemsId);
            Assert.Equal("A", cartItem.SKU_Id);
        }


        [Fact]
        public void InitializePromotions_Test()
        {
            //Arrange

            //Act
            var result = _controller.InitializePromotions();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<Promotion>>(list.Value);

            var listt = list.Value as List<Promotion>;
            Assert.Equal(3, listt.Count);
        }

        [Fact]
        public void InitializePromotionTypes_Test()
        {
            //Arrange

            //Act
            var result = _controller.InitializePromotionTypes();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<PromotionType>>(list.Value);

            var listt = list.Value as List<PromotionType>;
            Assert.Equal(2, listt.Count);
        }




        [Fact]
        public void InitializePromotionRules_Test()
        {
            //Arrange

            //Act
            var result = _controller.InitializePromotionRules();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<PromotionRule>>(list.Value);

            var listt = list.Value as List<PromotionRule>;
            Assert.Equal(2, listt.Count);
        }


        [Fact]
        public void GetActivePromotions_Test()
        {
            //Arrange

            //Act
            var result = _controller.GetActivePromotions();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<Promotion>>(list.Value);

            var listt = list.Value as List<Promotion>;
            Assert.Equal(3, listt.Count);
        }




        [Fact]
        public void AddPromotion_Test()
        {
            //CORRECT TEST START

            //Arrange
            var item = new Promotion()
            {
                PromotionId = 1,
                PromotionTypeId = 1,
                IsActive = true
            };

            //Act
            var createdResponse = _controller.AddPromotion(item);

            //Assert
            Assert.IsType<string>(createdResponse);

            //value of the result
            var result = createdResponse as string;
            Assert.Equal("Added", result);

            //CORRECT TEST END




            //BADREQUEST AND MODELSTATE ERROR START

            //Arrange

            //Act
            //_controller.ModelState.AddModelError("Title", "Title is a requried filed");
            string response = _controller.AddToCart(null);

            //Assert
            Assert.Equal("Promotion is empty", response);


            //Arrange
            var erroritem = new Promotion()
            {
                PromotionId = 1,
                PromotionTypeId = 1
            };

            //Act
            _controller.ModelState.AddModelError("IsActive", "IsActive is requried.");
            var badResponse = _controller.AddPromotion(erroritem);

            //Assert
            Assert.Equal("Validations failed", badResponse);

            //BADREQUEST AND MODELSTATE ERROR END
        }


         
    }
}


