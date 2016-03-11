using System;
using System.Collections.Generic;
using System.Linq;
using JsonSong.CashRegister.Domain.Config;
using JsonSong.CashRegister.Domain.Dto;
using JsonSong.CashRegister.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonSong.CashRegister.Test
{
    [TestClass]
    public class CashierTest
    {
        [TestMethod]
        public void CreateTestCashierTest()
        {
            var cashier = InitFactory.CreateTestCashier();

            PrivateObject po = new PrivateObject(cashier);
            var strategyList = (IList<Strategy>)po.GetProperty("StrategyRepo");

            //every rule should have at least one product
            strategyList.All(a=>a.ProductCodeList.Any()).IsTrue();

            //normal rule is must effected for all products
            (strategyList.First(a => a.Name == "").ProductCodeList.Count == ProductBox.GetAll().Count).IsTrue();

           // var priceResults = GetPriceResult(cart);
            var priceResults = (IEnumerable<PriceResult>)po.Invoke("GetPriceResultFromCode", TestData.Data1Json);

            priceResults.Sum(a => a.Total).IsEqule(TestData.Data1Off);
            priceResults.Sum(a => a.Save).IsEqule(TestData.Data1Original - TestData.Data1Off);


        }

       

    }
}
