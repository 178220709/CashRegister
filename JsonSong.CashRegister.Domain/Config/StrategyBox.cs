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
    /// 规则使用属性构造,保证每次返回的是新的规则对象
    /// </summary>
    public static class StrategyBox
    {
        public static IList<Strategy> GetAll()
        {
            var types = typeof (StrategyBox).GetProperties();
            return types.Select(a => a.GetValue(null)).Cast<Strategy>().ToList();
        }

        public static Strategy Normal
        {
            get
            {
                return new Strategy()
                {
                    Name = "",
                    Level = 0,
                    StrategyRule = (pro, num) => (pro.Price*num)
                };
            }
        }

        public static Strategy Discount95
        {
            get
            {
                return new Strategy()
                {
                    Name = "95折",
                    Level = 1,
                    StrategyRule = (pro, num) => (pro.Price*num*0.95),
                    RenderOutput = result => string.Format("名称：{0}，数量：{1}{2}，单价：{3}(元)，小计：{4}(元)，节省{5}(元)",
                        result.Product.Name, result.Num, result.Product.UnitName, result.Product.Price.ToPriceShow(), result.Total.ToPriceShow(),
                        result.Save.ToPriceShow())
                };
            }
        }


        public static Strategy When2Cut1
        {
            get
            {
                return new Strategy()
                {
                    Name = "买二赠一",
                    Level = 2,
                    StrategyRule = (pro, num) => (num - (int) num/3)*pro.Price
                };
            }
        }
    }
}