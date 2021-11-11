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
    public class BusinessRuleUnitTest
    {
        //private readonly BookService _service;
        private readonly BusinessRuleController _controller;
        private readonly BusinessRuleService _service;
        List<BusinessRule> listRules;


        public BusinessRuleUnitTest()
        {
            _service = new BusinessRuleService(listRules);
            _controller = new BusinessRuleController(_service);
        }


        [Fact]
        public void InitializeRules_Test()
        {
            //Arrange

            //Act
            var result = _controller.InitializeRules();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<BusinessRule>>(list.Value);

            var listt = list.Value as List<BusinessRule>;
            Assert.Equal(7, listt.Count);
        }


        [Fact]
        public void AddRule_Test()
        {
            //CORRECT TEST START

            //Arrange
            var item = new BusinessRule()
            {
                PaymentFor = "Payment for testing",
                Rule = "Send an Email for tesing"
            };

            //Act
            var createdResponse = _controller.AddRule(item);

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
            string response = _controller.AddRule(null);

            //Assert
            Assert.Equal("Business Rule is empty", response);


            //Arrange
            var erroritem = new BusinessRule()
            {
                PaymentFor = "testing rule"
            };

            //Act
            _controller.ModelState.AddModelError("Rule", "Rule is requried.");
            var badResponse = _controller.AddRule(erroritem);

            //Assert
            Assert.Equal("Validations failed", badResponse);

            //BADREQUEST AND MODELSTATE ERROR END
        }




        [Theory]
        [InlineData("Physical product", null)]
        public void RuleForPayment(string correctPayment, string incorrectPayment)
        {
            //Arrange
            string validPayment = correctPayment;
            string InvalidPayment = incorrectPayment;

            //Act
            var notFoundResult = _controller.RuleForPayment(InvalidPayment);
            var okResult = _controller.RuleForPayment(validPayment);

            //Assert
            Assert.IsType<string>(notFoundResult);
            Assert.IsType<string>(okResult);


            Assert.Equal("Data not found", notFoundResult);
            Assert.Equal("Generate a packing slip for shipping", okResult);
        }


    }
}


