using System;

namespace Events
{
    public class EntryPoint
    {
        static int score = 0;

        static void Main(string[] args)
        {
            Shooter shooter = new Shooter();

            shooter.ShotsFired += KilledEnemy;
            shooter.ShotsFired += AddScore;

            shooter.OnShoot();
        }

        // Subscriber methods to the event
        static void KilledEnemy(object source, ShotsFiredEventArgs e)
        {
            Console.WriteLine($"I killed an enemy at {e.TimeOfKill.TimeOfDay}!");
            Console.WriteLine($"My score is now {score}");

        }
    
        static void AddScore(object source, EventArgs e)
        {
            score++;
        }
    }
}
