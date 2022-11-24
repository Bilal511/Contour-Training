using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using TrainingClientLibrary;
using TrainingModels.Models;
using TrainingModels.Models.TestModel;

namespace TrainingConsoleAppTests
{
    internal class Program
    {
        public static readonly string UserName = "Bilal";
        public static readonly string Password = "Basketball";
        public static readonly string ClintUrl = "https://localhost:44362/";

        private static void Main(string[] args)
        {
            Insert();
            GetById();
            Update();
            DeleteById();
        }

        public static void Insert()
        {
            var clint = new TrainingClient(UserName, Password, ClintUrl);
            var result = clint.Insert(new TestCtrlRequest() { FirstName = "Toby", LastName = "Sims", CellNumber = "0826485138" });
            Console.WriteLine(result.Status + Environment.NewLine + result.Error);
            //Console.ReadLine();
        }

        public static void GetById()
        {
            var clint = new TrainingClient(UserName, Password, ClintUrl);
            var result = clint.GetById(new GetTestByIdCtrlRequest() { Id = 14 });
            Console.WriteLine(result.TestCtrl.Id + Environment.NewLine + result.TestCtrl.FirstName + Environment.NewLine + result.TestCtrl.LastName + Environment.NewLine + result.Status + Environment.NewLine + result.Error);
            Console.ReadLine();
        }

        public static void Update()
        {
            var clint = new TrainingClient(UserName, Password, ClintUrl);
            var result = clint.Update(new TestCtrlRequest() { Id = 15, FirstName = "Aadil", LastName = "Person", CellNumber = "0847315982"});
            Console.WriteLine(result.RowsAffected + Environment.NewLine + result.Status + Environment.NewLine + result.Error);
            //Console.ReadLine();
        }

        public static void DeleteById()
        {
            var clint = new TrainingClient(UserName, Password, ClintUrl);
            var result = clint.DeleteById(new DeleteTestByIdCtrlRequest(){Id = 21});
            Console.WriteLine(result.Status + Environment.NewLine + result.Error);
            Console.ReadLine();
        }
    }
}
