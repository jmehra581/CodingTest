using CodingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Repository
{
    public interface IStockService
    {
        public void InitializeSKU(List<Stock> listStock);
    }
}
