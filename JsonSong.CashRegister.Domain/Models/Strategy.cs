using System;
using System.Collections.Generic;

namespace JsonSong.CashRegister.Domain.Models
{
    public class Strategy
    {
        public Strategy()
        {
            ProductCodeList = new List<string>();
        }

        public string Name { get; set; }
        /// <summary>
        /// 该优惠策略优先级 
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 优惠策略具体算法
        /// </summary>
        public Func<Product, int, double> StrategyRule { get; set; }
        /// <summary>
        /// 参与该优惠策略的商品的Code
        /// </summary>
        public IList<string> ProductCodeList { get; set; }

    }
}
