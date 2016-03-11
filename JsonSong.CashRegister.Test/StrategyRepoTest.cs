using System;
using System.Linq;
using JsonSong.CashRegister.Domain.Config;
using JsonSong.CashRegister.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonSong.CashRegister.Test
{
    [TestClass]
    public class StrategyRepoTest
    {
        [TestMethod]
        public void BaseConfigTest()
        {
            var list = StrategyBox.GetAll();

            Assert.IsTrue(list.Count > 0);
            //no level conflict
            Assert.AreEqual(list.GroupBy(a => a.Level).Count(g => g.Count() > 1),0);
        }

        [TestMethod]
        public void Discount95Test()
        {
            const double price = 2.74;
            var strategy = StrategyBox.Discount95;
            strategy.StrategyRule(new Product { Price = price },3).IsEqule(price * 3 * 0.95).IsTrue();
        }

        [TestMethod]
        public void When2Cut1Test()
        {
            const double price = 2.74;
            var strategy = StrategyBox.When2Cut1;
            var product = new Product { Price = price };
            strategy.StrategyRule(product,3).IsEqule(price * 2).IsTrue();

            strategy.StrategyRule(product,7).IsEqule(price * 5).IsTrue();
        }

    }
}
