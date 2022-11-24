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
    class ThreadConfigFactory
    {
        private readonly string _dbConnectionString;
        public ThreadConfigFactory()
        {
            this._dbConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        }

        public ThreadConfig  Create(Action<ThreadConfig> initializer)
        {
            var threadConfig = new ThreadConfig();
            initializer(threadConfig);
            return threadConfig;
        }

        public ThreadConfig Get()
        {
            ThreadConfig threadConfig = null;

            using (var con = new SqlConnection(this._dbConnectionString))
            {
                con.Open();

                var cmd = new SqlCommand(@"SELECT TOP 1 [Id]
                                          ,[NumberOfAllowedWorkerThreads]
                                          ,[NumberOfAllowedRetryThreads]
                                          ,[AutoAttemptRetryMaxCount]
                                          ,[ThreadTimeoutLimit]
                                          FROM [dbo].[ThreadConfig] ORDER BY ID DESC", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@WorkerStatus", WorkerStatus.Idle);
                cmd.Parameters.AddWithValue("@Enabled", true);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    threadConfig = new ThreadConfigFactory().Create(t =>
                    {
                        t.NumberOfAllowedWorkerThreads = Convert.ToInt32((reader["NumberOfAllowedWorkerThreads"] as int?).GetValueOrDefault());
                        t.NumberOfAllowedRetryThreads = Convert.ToInt32((reader["NumberOfAllowedRetryThreads"] as int?).GetValueOrDefault());
                        t.AutoAttemptRetryMaxCount = Convert.ToInt32((reader["AutoAttemptRetryMaxCount"] as int?).GetValueOrDefault());
                        t.ThreadTimeoutLimit = Convert.ToInt32((reader["ThreadTimeoutLimit"] as int?).GetValueOrDefault());
                    });
                }
                con.Close();
            }
            return threadConfig;
        }
    }
}
