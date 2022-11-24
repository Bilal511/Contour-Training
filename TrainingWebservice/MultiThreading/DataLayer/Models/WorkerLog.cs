using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingWebservice.MultiThreading.DataLayer.Models
{
    public class WorkerLog
    {
        private readonly string _dbConnectionString;
        public WorkerLog(string dbConnectionString)
        {
            this._dbConnectionString = dbConnectionString;
        }
        public int WorkerId { get; set; }

        public string TransactionId { get; set; }

        public WorkerStatus WorkerStatus { get; set; }

        public string Message { get; set; }

        public string ErrorMessage { get; set; }
    }
}
