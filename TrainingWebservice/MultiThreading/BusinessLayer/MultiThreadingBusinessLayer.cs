using System;
using TIDRolloverClient;
using TrainingClientLibrary;
using TrainingWebservice.MultiThreading.DataLayer.Models;

namespace TrainingWebservice.MultiThreading.BusinessLayer
{
    public class MultiThreadingBusinessLayer
    {

        public static readonly string UserName = "Bilal";
        public static readonly string Password = "Basketball";
        public static readonly string ClintUrl = "https://localhost:44362/";

        public void ThreadProcess(Worker worker)
        {
            try
            {
                var clint = new TrainingClient(UserName, Password, ClintUrl);
                clint.CompareList();
                worker.WorkerStatus = WorkerStatus.Idle;
                worker.Update();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
