using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingWebservice.MultiThreading.DataLayer.Models;

namespace TrainingWebservice.MultiThreading.DataLayer.Factories
{
    class WorkerLogFactory
    {
        private readonly string _dbConnectionString;
        public WorkerLogFactory()
        {
            this._dbConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        }

        public WorkerLog Create(Action<WorkerLog> initializer)
        {
            var workerLog = new WorkerLog(this._dbConnectionString);
            initializer(workerLog);
            return workerLog;
        }
    }
}
