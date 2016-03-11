using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JsonSong.CashRegister.Domain.Models;

namespace JsonSong.CashRegister.Domain.Config
{
    /// <summary>
    /// 所有的销售策略在此定义,注意,不包含参与优惠的商品的定义
    /// 目前只支持根据商品的数量和单价的优惠策略,后期考虑进行更高层次的抽象
    /// </summary>
    public static class StrategyBox
    {
        public static IList<Strategy> GetAll()
        {
            var types = typeof(StrategyBox).GetFields();
            return types.Select(a => a.GetValue(null)).Cast<Strategy>().ToList();
        }

        public static Strategy Discount95 = new Strategy()
        {
            Name = "95折",
            Level = 1,
            StrategyRule = (pro, num) => (pro.Price * num * 0.95)
        };

        public static Strategy When2Cut1 = new Strategy()
        {
            Name = "买2赠1",
            Level = 2,
            StrategyRule = (pro, num) => (num - num / 3) * pro.Price
        };
    }
}
