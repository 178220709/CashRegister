using System;
using JsonSong.CashRegister.Domain.Config;

namespace JsonSong.CashRegister
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var cashier = InitFactory.CreateTestCashier();
            var result = cashier.GetResultFromCode(TestData.Data1Json);
            System.Console.Write(result);

            System.Console.ReadLine();
        }
    }
}