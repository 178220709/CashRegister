using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JsonSong.CashRegister.Domain.Models;

namespace JsonSong.CashRegister.Domain.Config
{
    /// <summary>
    /// 从配置或者数据源或缓存中构造出需要的收音机,这里直接mock了
    /// </summary>
    public sealed class InitFactory
    {
        /// <summary>
        /// 构造一个包含所有产品,苹果9.5折,羽毛球和可乐买二送一的收银机
        /// </summary>
        /// <returns></returns>
        public static Cashier CreateTestCashier()
        {
            var rule1 = StrategyBox.Discount95;
            rule1.ProductCodeList.Add(ProductBox.Apple.BarCode);

            var rule2 = StrategyBox.When2Cut1;
            rule2.ProductCodeList.Add(ProductBox.CocaCola.BarCode);
            rule2.ProductCodeList.Add(ProductBox.Badminton.BarCode);

            var rule3 = StrategyBox.Normal;
            ProductBox.GetAll().ToList().ForEach(a => rule3.ProductCodeList.Add(a.BarCode));
            return new Cashier(ProductBox.GetAll(), new List<Strategy> { rule1, rule2, rule3 }, "没钱赚商店");
        }
    }
}
