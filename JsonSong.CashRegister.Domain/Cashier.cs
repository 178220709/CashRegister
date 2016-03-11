using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using JsonSong.CashRegister.Domain.Config;
using JsonSong.CashRegister.Domain.Dto;
using JsonSong.CashRegister.Domain.Exception;
using JsonSong.CashRegister.Domain.Models;
using Newtonsoft.Json;

namespace JsonSong.CashRegister.Domain
{
    public class Cashier
    {
        private IEnumerable<Product> ProductRepo { get; set; }
        private IEnumerable<Strategy> StrategyRepo { get; set; }
        public string ShopName { get; set; }

        public Cashier(IEnumerable<Product> productRepo, IEnumerable<Strategy> strategyRepo, string shopName)
        {
            ProductRepo = productRepo;
            StrategyRepo = strategyRepo;
            ShopName = shopName;
        }

        public string GetResultFromCode(string codeJsonStr)
        {
            var priceResults = GetPriceResultFromCode(codeJsonStr);
            return GetResultContent(priceResults);
        }

        private IEnumerable<PriceResult> GetPriceResultFromCode(string codeJsonStr)
        {
            var codeList = JsonConvert.DeserializeObject<IList<string>>(codeJsonStr);
            var cart = GetCart(codeList);
            return GetPriceResult(cart);
        }

        /// <summary>
        /// 将条形码转换成[商品code,商品数量]的结构
        /// </summary>
        /// <param name="codeList"></param>
        /// <returns></returns>
        private static Dictionary<string, double> GetCart(IList<string> codeList)
        {
            var dic = new Dictionary<string, double>();
            if (codeList == null || !codeList.Any())
            {
                return dic;
            }
            codeList.ToList().ForEach(code =>
            {
                if (code.Contains("-"))
                {
                    var _info = code.Split('-');
                    double _num;
                    if (!double.TryParse(_info[1], out _num))
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

        private IEnumerable<PriceResult> GetPriceResult(Dictionary<string, double> cart)
        {
            var productCodes = ProductRepo.Select(a => a.BarCode).ToList();
            if (!cart.Keys.All(key => productCodes.Contains(key)))
            {
                throw new UnknownProductException();
            }
            var results = cart.ToList().Select(kv =>
            {
                var dto = new PriceResult()
                {
                    Product = ProductRepo.First(a => a.BarCode == kv.Key),
                    Num = kv.Value,
                };
                dto.Strategy = StrategyRepo.Where(a => a.ProductCodeList.Contains(kv.Key))
                    .OrderByDescending(a => a.Level).First();
                dto.Total = dto.Strategy.StrategyRule(dto.Product, kv.Value);
                dto.Save = dto.Product.Price * dto.Num - dto.Total;
                return dto;
            });
            return results;
        }

        private string GetResultContent(IEnumerable<PriceResult> priceResults)
        {
            StringBuilder sb = new StringBuilder();
            const string strategySplitLine = "----------------------";
            sb.AppendLine(string.Format("***<{0}>购物清单***", ShopName));


            var results = priceResults as IList<PriceResult> ?? priceResults.ToList();
            results.ToList().ForEach(dto=>sb.AppendLine(dto.Strategy.RenderOutput(dto)));
            sb.AppendLine(strategySplitLine);
            var specialResults = priceResults.Where(a => a.Strategy.Level == StrategyBox.When2Cut1.Level).ToList();

            if (specialResults.Any())
            {
                sb.AppendLine(StrategyBox.When2Cut1.Name + "商品：");
                specialResults.ForEach(dto => sb.AppendLine(string.Format("名称：{0}，数量：{1}{2}",
                    dto.Product.Name,dto.Num,dto.Product.UnitName)));
                sb.AppendLine(strategySplitLine);
            }
            sb.AppendLine(string.Format(" 总计：{0}(元)", results.Sum(a=>a.Total).ToPriceShow()));
            sb.AppendLine(string.Format(" 节省：{0}(元)", results.Sum(a=>a.Save).ToPriceShow()));
            sb.AppendLine("**********************");
            return sb.ToString();
        }
    }

    internal static class CashierHelper
    {
        internal static void AddOrUpdateCount(this Dictionary<string, double> dic, string key, double count = 1)
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

    public static class CashierShowHelper
    {
        public static  string ToPriceShow(this  double source)
        {
            return source.ToString("0.00");
        }
    }
}