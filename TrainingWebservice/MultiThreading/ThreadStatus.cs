using System;
using System.Threading;

namespace TrainingWebservice.MultiThreading
{
    public class ThreadStatus
    {

        public Thread Thread { get; private set; }

        public DateTime StartTime { get; private set; }

        public ThreadStatus(Thread thread, DateTime startTime)
        {
            this.Thread = thread;
            this.StartTime = startTime;
        }
    }
}
