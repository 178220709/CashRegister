using JsonSong.CashRegister.Domain.Models;

namespace JsonSong.CashRegister.Domain.Dto
{
    /// <summary>
    /// 收银机返回格式
    /// 名称：苹果，数量：2斤，单价：5.50(元)，小计：10.45(元)，节省0.55(元) (优惠类型)
    /// </summary>
    public class PriceResult
    {
        public Product Product { get; set; }
        public double Num { get; set; }
        public double Total { get; set; }
        public double Save { get; set; }
        public Strategy Strategy { get; set; }
    }
}
