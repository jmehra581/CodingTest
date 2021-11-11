using CodingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Repository
{
    public class StockService : IStockService
    {
        private readonly List<Stock> _listStock;

        public StockService(List<Stock> listStock)
        {
            _listStock = listStock;
        }

        public void InitializeSKU(List<Stock> listStock)
        {
            Stock[] stocks = {
                new Stock(){ SKU_Id="A",UnitPrice=10 },
                new Stock(){ SKU_Id="B",UnitPrice=10 },
                new Stock(){ SKU_Id="C",UnitPrice=10 },
                new Stock(){ SKU_Id="D",UnitPrice=10 }
            };

            _listStock.AddRange(stocks);


        }
    }
}
