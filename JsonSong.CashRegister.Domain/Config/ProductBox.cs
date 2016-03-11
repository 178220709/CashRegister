using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JsonSong.CashRegister.Domain.Models;

namespace JsonSong.CashRegister.Domain.Config
{
    /// <summary>
    /// 所有的销售策略在此定义
    /// </summary>
    public static class ProductBox
    {
        public static IList<Product> GetAll()
        {
            var types = typeof(ProductBox).GetFields();
            return types.Select(a => a.GetValue(null)).Cast<Product>().ToList();
        }

        public static Product CocaCola = new Product()
        {
            Name = "可口可乐",
            UnitName = "瓶",
            Price = 3.00,
            BarCode = "000001"
        };

        public static Product When2Cut1 = new Product()
        {
            Name = "羽毛球",
            UnitName = "个",
            Price = 2.00,
            BarCode = "000002"
        };

        public static Product Apple = new Product()
        {
            Name = "苹果",
            UnitName = "斤",
            Price = 5.50,
            BarCode = "000003"
        };
    }
}
