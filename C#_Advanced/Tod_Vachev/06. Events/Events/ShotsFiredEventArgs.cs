using System;

namespace Events
{
    // Custom Event Args to pass additional information
    public class ShotsFiredEventArgs : EventArgs
    {
        public DateTime TimeOfKill { get; set; }

        public ShotsFiredEventArgs(DateTime time)
        {
            this.TimeOfKill = time;
        }
    }
}
