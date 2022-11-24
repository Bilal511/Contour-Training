using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingWebservice.MultiThreading.DataLayer.Models;

namespace TrainingWebservice.MultiThreading.DataLayer.Factories
{
    class WorkerFactory
    {
        private readonly string _dbConnectionString;
        public WorkerFactory()
        {
            this._dbConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        }

        public Worker Create(Action<Worker> initializer)
        {
            var worker = new Worker(this._dbConnectionString);
            initializer(worker);
            return worker;
        }

        public Worker Get(int workerId)
        {
            Worker worker = null;

            using (var connection = new SqlConnection(this._dbConnectionString))
            {
                connection.Open();

                var cmd = new SqlCommand(@"Select TOP 1 * From Worker where WorkerId = @Id", connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", workerId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    worker = new Worker(this._dbConnectionString)
                    {
                        WorkerId = Convert.ToInt32((reader["WorkerId"] as int?).GetValueOrDefault()),
                        LastUpdated = Convert.ToDateTime(reader["LastUpdated"] as DateTime?),
                        ThreadId = Convert.ToInt32(reader["ThreadId"] as int?),
                        WorkerStatus = (WorkerStatus)Convert.ToInt32(reader["WorkerStatus"] as int?),
                        TransactionsExecuted = Convert.ToInt32(reader["TransactionsExecuted"] as int?)
                    };
                }

                connection.Close();
            }

            return worker;
        }


    }
}
