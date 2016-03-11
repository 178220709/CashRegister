using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonSong.CashRegister.Test
{
    public static  class TestHelper
    {
        public static bool IsEqule(this double source, double targer)
        {
            return Math.Abs(source - targer) < 0.01;
        }

       

        public static void IsTrue(this bool source)
        {
             Assert.IsTrue(source);
        }


    }
}
