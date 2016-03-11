using JsonSong.CashRegister.Domain.Config;

namespace JsonSong.CashRegister
{
    public class TestData
    {
        /// <summary>
        /// 三个可乐,8个羽毛球,4.5斤苹果,3个篮球
        /// 原价 
        /// </summary>
        public static string Data1Json = @"['000001','000001','000001' ,
'000002-8','000003-4.5','000004-3']";

        public static double Data1Original = ProductBox.CocaCola.Price*3 + ProductBox.Badminton.Price*8 +
                                             ProductBox.Apple.Price*4.5 + ProductBox.Basketball.Price*3;

        public static double Data1Off = ProductBox.CocaCola.Price*2 + ProductBox.Badminton.Price*6 +
                                         ProductBox.Apple.Price * 4.5 * 0.95 + ProductBox.Basketball.Price * 3;
    }
}