using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Market_S01L03_ODP_PriceList
    {
        private List<float> prices = new List<float>();

        public void AddPrice(float price)
        {
            prices.Add(price);
            PriceAdded?.Invoke(this, price);
        }

        public event EventHandler<float> PriceAdded;
    }

    public class Program_S01L03b
    {
        static void Main_S01L03b(string[] args)
        {
            var market = new Market_S01L03_ODP_PriceList();
            market.PriceAdded += (sender, f) => { Console.WriteLine($"Price received is: {f}"); };
            market.AddPrice(13);
        }
    }
}