using System;
using System.Configuration;
using System.Data.SqlClient;
using TrainingWebservice.MultiThreading.BusinessLayer;
using TrainingWebservice.MultiThreading.DataLayer.Models;

namespace TrainingWebservice.MultiThreading
{
    public class BackUpProcessingHandler
    {
        private readonly MultiThreadingBusinessLayer _multiThreadingBusinessLayer;
        private readonly System.Timers.Timer _timer= null;
        private readonly WorkerLog  _workerLog;
        private readonly ThreadMonitor _threadMonitor;
        public int TimerIntervalInMilliSeconds = 15;
        private readonly string _dbConnectionString = null;

        public BackUpProcessingHandler(string dbConnectionString)
        {
           this._dbConnectionString = dbConnectionString;
            try
            {
                this._workerLog = new WorkerLog(dbConnectionString)
                {
                    ErrorMessage = "",
                    WorkerId = 0,
                    WorkerStatus = WorkerStatus.Busy,
                    TransactionId = "",
                    Message = $""
                };
                
                this.TimerIntervalInMilliSeconds = 15;

                _timer = new System.Timers.Timer();
                _timer.Elapsed += TimerElapsed;
                _timer.Interval = this.TimerIntervalInMilliSeconds;
                _timer.Enabled = true;

                this._threadMonitor = new ThreadMonitor(dbConnectionString);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void TimerElapsed(object sender, EventArgs e)
        {
            try
            {
                _timer.Enabled = false;
                using (var connection = new SqlConnection(_dbConnectionString))
                {
                    connection.Open();
                    var worker = _threadMonitor.GetFreeThread("");
                    if (worker == null)
                    {
                        return;
                    }
                    this._multiThreadingBusinessLayer.ThreadProcess(worker);

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
