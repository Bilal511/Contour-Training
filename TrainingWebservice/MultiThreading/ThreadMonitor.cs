using System;
using System.Collections.Generic;
using System.Threading;
using TrainingWebservice.MultiThreading.BusinessLayer;
using TrainingWebservice.MultiThreading.DataLayer.Factories;
using TrainingWebservice.MultiThreading.DataLayer.Models;

namespace TrainingWebservice.MultiThreading
{
    public class ThreadMonitor
    {

        private  string _dbConnectionString { get; set; }
        private readonly Dictionary<int, ThreadStatus> _threadDictionary;
        private readonly ThreadConfig _threadConfig;
        private readonly int _maxThreads;
        private readonly int _timeLimit;
        private readonly MultiThreadingBusinessLayer _multiThreadingBusinessLayer;

        public ThreadMonitor(string dbConnectionString)
        {
            ThreadConfig threadConfig;
            this._dbConnectionString = dbConnectionString;
            this._threadDictionary = new Dictionary<int, ThreadStatus>();
            this._threadConfig = new ThreadConfigFactory().Get();
            this._maxThreads = this._threadConfig.NumberOfAllowedWorkerThreads;
            this._timeLimit = this._threadConfig.ThreadTimeoutLimit;
            this._multiThreadingBusinessLayer = new MultiThreadingBusinessLayer();
        }

        public void StartThreadProcess(Worker worker)
        {
            var thread = new Thread(new ThreadStart(() => _multiThreadingBusinessLayer.ThreadProcess(worker)));

            worker.WorkerStatus = WorkerStatus.Busy;
            worker.ThreadId = thread.ManagedThreadId;
            worker.Update();

            this._threadDictionary[worker.WorkerId] = new ThreadStatus(thread, DateTime.Now);
            this._threadDictionary[worker.WorkerId].Thread.Start();
        }
        
        public Worker GetFreeThread(string transactionId)
        {
            try
            {
                var workerFactory = new WorkerFactory();

                for (var i = 1; i <= this._maxThreads; i++)
                {
                    var i1 = i;
                    var worker = workerFactory.Get(i);

                    if (worker == null)
                    {
                        worker = workerFactory.Create(w =>
                        {
                            w.WorkerId = i1;
                            w.WorkerStatus = WorkerStatus.Idle;
                            w.LastUpdated = DateTime.Now;
                            w.TransactionsExecuted = 0;
                            w.ThreadId = 0;
                        });

                        worker.Insert();
                    }
                    else
                    {
                        if (worker.WorkerStatus != WorkerStatus.Busy)
                        {
                            if (!this._threadDictionary.TryGetValue(worker.WorkerId, out _))
                            {
                                this._threadDictionary[worker.WorkerId] = null;
                            }

                            if (this._threadDictionary[worker.WorkerId] == null)
                            {
                                return AllocateWorker(worker, transactionId);
                            }

                            if (CheckThread(worker))
                            {
                                return AllocateWorker(worker, transactionId);
                            }
                        }
                        else
                        {
                            worker.ResetWorkers();
                        }
                    }
                }

            }
            catch
            {
                var worker = new Worker(_dbConnectionString);
                worker.ResetWorkers();
            }
            return null;
        }
        
        private bool CheckThread(Worker worker)
        {
            if (this._threadDictionary[worker.WorkerId].Thread.ThreadState == ThreadState.Aborted ||
                this._threadDictionary[worker.WorkerId].Thread.ThreadState == ThreadState.AbortRequested)
            {
                return false;
            }

            if (!this._threadDictionary[worker.WorkerId].Thread.IsAlive)
            {
                switch (this._threadDictionary[worker.WorkerId].Thread.ThreadState)
                {
                    case ThreadState.Stopped:
                        return true;
                    case ThreadState.Unstarted:
                        return false;
                    default:
                        this._threadDictionary[worker.WorkerId].Thread.Abort(); 
                        return false;
                }
            }

            if ((DateTime.Now - this._threadDictionary[worker.WorkerId].StartTime).TotalSeconds > this._timeLimit)
            {
                this._threadDictionary[worker.WorkerId].Thread.Abort();
                worker.WorkerStatus = WorkerStatus.Error;
                
                var workerLog = new WorkerLogFactory().Create(w =>
                {
                    w.WorkerId = worker.WorkerId;
                    w.WorkerStatus = worker.WorkerStatus;
                    w.Message = "";
                    w.ErrorMessage = "Thread reached time-limit";
                });
            }
            return false;
        }


        public Worker AllocateWorker(Worker worker, string transactionId)
        {
            var workerLog = new WorkerLogFactory().Create(w =>
            {
                w.TransactionId = transactionId;
                w.WorkerId = worker.WorkerId;
                w.ErrorMessage = "";
            });
            
            worker.LastUpdated = DateTime.Now;
            worker.ThreadId = 0;

            worker.WorkerStatus = WorkerStatus.Idle;
            worker.Update();
            workerLog.WorkerStatus = WorkerStatus.Idle;
            
            return worker;
        }
    }
}
