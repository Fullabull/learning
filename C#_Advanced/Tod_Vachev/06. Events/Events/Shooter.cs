using System;
using System.Threading;

namespace Events
{
    public class Shooter
    {
        // Event to be invoked upon successful kill
        public event EventHandler<ShotsFiredEventArgs> ShotsFired;

        private Random rng = new Random();

        public void OnShoot()
        {
            while (true)
            {
                if (rng.Next(0, 100) % 2 == 0)
                {
                    ShotsFired?.Invoke(this, new ShotsFiredEventArgs(DateTime.Now)); // EventName? means if Event is not Null (if it has subscribers)
                }
                else
                {
                    Console.WriteLine("I Missed!");
                }

                Thread.Sleep(1000);
            }
        }
    }
}
