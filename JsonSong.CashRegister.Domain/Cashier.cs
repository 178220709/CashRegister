using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonSong.CashRegister.Domain.Core;
using JsonSong.CashRegister.Domain.Models;
using Newtonsoft.Json;

namespace JsonSong.CashRegister.Domain
{
    public class Cashier
    {
        private IEnumerable<Product>  ProductRepo { get; set; }
        private IEnumerable<Strategy> StrategyRepo { get; set; }

        public Cashier(IEnumerable<Product> productRepo, IEnumerable<Strategy> strategyRepo)
        {
            ProductRepo = productRepo;
            StrategyRepo = strategyRepo;
        }

        public double GetTotal(string codeJsonStr)
        {
            var codeList = JsonConvert.DeserializeObject<IList<string>>(codeJsonStr);
            var cart = GetCart(codeList);

        }

        /// <summary>
        /// 将条形码转换成[商品code,商品数量]的结构
        /// </summary>
        /// <param name="codeList"></param>
        /// <returns></returns>
        private static Dictionary<string, int> GetCart(IList<string> codeList)
        {
            var dic = new Dictionary<string, int>();
            if (codeList == null || !codeList.Any())
            {
                return dic;
            }
            codeList.ToList().ForEach(code =>
            {
                if (code.Contains("-"))
                {
                    var _info = code.Split('-');
                    int _num;
                    if (!int.TryParse(_info[1],out _num))
                    {
                        throw new BarcodeException();
                    }
                    dic.AddOrUpdateCount(_info[0], _num);
                }
                else
                {
                    dic.AddOrUpdateCount(code);
                }
            });

            return dic;
        }

        private  double CountCart(Dictionary<string, int> cart)
        {
            var productCodes = ProductRepo.Select(a => a.BarCode).ToList();
            if (!cart.Keys.All(key => productCodes.Contains(key)))
            {
                throw new UnknownProductException();
            }
            cart.ToList().ForEach(a=>);


        }
    }

    internal static class CashierHelper
    {
        internal static void AddOrUpdateCount(this Dictionary<string, int> dic,string key,int count=1)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = dic[key] + count;
            }
            else
            {
                dic[key] = count;
            }
        }
    }



}
