using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ConsoleApp1.Annotations;

namespace ConsoleApp1
{
    public class Market_S01L03 : INotifyPropertyChanged    
    {
        private float volatility;

        public float Volatility
        {
            get => volatility;
            set
            {
                if (value.Equals(volatility)) return;
                volatility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    class Program_S01L03
    {
        public static void Main_S01L03(string[] args)
        {
            var market = new Market_S01L03();
            market.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Volatility")
                {
                    Console.WriteLine($"sender: {sender.ToString()}\neventArgs: {e.ToString()}");
                    Console.WriteLine($"args: {e}");
                }
            };
            market.Volatility = 25.00f;
        }
    }
}