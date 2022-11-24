using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingWebservice.MultiThreading.DataLayer.Models
{
    public class ThreadConfig
    {
        public int Id { get; }
        public int NumberOfAllowedWorkerThreads { get; set; }
        public int NumberOfAllowedRetryThreads { get; set; }
        public int AutoAttemptRetryMaxCount { get; set; }
        public int ThreadTimeoutLimit { get; set; }
    }
}
