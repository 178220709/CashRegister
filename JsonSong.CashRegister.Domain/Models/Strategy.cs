using System;
using System.Collections.Generic;

namespace JsonSong.CashRegister.Domain.Models
{
    public class Strategy
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public Func<IList<Product>,float> Price { get; set; }
       
    }
}
