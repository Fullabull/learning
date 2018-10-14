using System;
using System.ComponentModel;

namespace ConsoleApp1
{
    public class S01L03_ODP_BetterPriceList
    {
        public class Market
        {
            public BindingList<float> Prices = new BindingList<float>();

            public void AddPrice(float price)
            {
                Prices.Add(price);
            }
        }
    
        class Program
        {
            public static void MainX(string[] args)
            {
                var market = new Market();
                market.Prices.ListChanged += (sender, eventArgs) =>
                {
                    if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                    {
                        float price = ((BindingList<float>) sender)[eventArgs.NewIndex];
                        Console.WriteLine($"Binding List new price: {price}");

                    }
                };
                market.AddPrice(13);
            }
        }       
    }
}