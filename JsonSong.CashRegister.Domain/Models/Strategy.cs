using System;
using System.Collections.Generic;
using JsonSong.CashRegister.Domain.Dto;

namespace JsonSong.CashRegister.Domain.Models
{
    public class Strategy
    {
        public Strategy()
        {
            ProductCodeList = new List<string>();
            RenderOutput = result => string.Format("名称：{0}，数量：{1}{2}，单价：{3}(元)，小计：{4}(元)",
                result.Product.Name, result.Num, result.Product.UnitName, result.Product.Price.ToString("0.00"), result.Total.ToString("0.00"));
        }

        public string Name { get; set; }
        /// <summary>
        /// 该优惠策略优先级 
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 优惠策略具体算法
        /// </summary>
        public Func<Product, double, double> StrategyRule { get; set; }
        /// <summary>
        /// 优惠策略具体算法
        /// </summary>
        public Func<PriceResult, string> RenderOutput { get; set; }
        /// <summary>
        /// 参与该优惠策略的商品的Code
        /// </summary>
        public IList<string> ProductCodeList { get; set; }

    }
}
