using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingWebservice.MultiThreading.DataLayer.Models
{
    public class Worker
    {
        private readonly string _dbConnectionString;

        public Worker(string dbConnectionString)
        {
            this._dbConnectionString = dbConnectionString;
        }

        public int ThreadId { get; set; }

        public int TransactionsExecuted { get; set; }

        public int WorkerId { get; set; }

        public DateTime LastUpdated { get; set; }

        public WorkerStatus WorkerStatus { get; set; }
        public void Update()
        {
            string sql = @"UPDATE [dbo].[Worker]
                           SET [TransactionsExecuted] = TransactionsExecuted+1
                              ,[LastUpdated] = @LastUpdated
                              ,[WorkerStatus] = @WorkerStatus
                              ,[CallbackURL] = ''
                              
                         WHERE WorkerId = @WorkerId";

            using (var con = new SqlConnection(this._dbConnectionString))
            {
                con.Open();
                var cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@WorkerId", WorkerId);
                cmd.Parameters.AddWithValue("@TransactionsExecuted", TransactionsExecuted);
                cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@WorkerStatus", WorkerStatus);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public bool Insert()
        {
            int ret = 0;

            using (var con = new SqlConnection(this._dbConnectionString))
            {
                con.Open();
                var cmd = new SqlCommand(@"INSERT INTO [dbo].[Worker]
                           ([TransactionsExecuted]
                           ,[WorkerId]
                           ,[LastUpdated]
                           ,[WorkerStatus]
                            ,[TransactionProcessingId]
                            ,ThreadId
                           ,[CallbackURL])
                            
                     VALUES
                           (@TransactionsExecuted
                           ,@WorkerId
                           ,@LastUpdated
                           ,@WorkerStatus
                           ,@TransactionProcessingId
                            ,@ThreadId
                           ,@CallbackURL)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@WorkerId", WorkerId);
                cmd.Parameters.AddWithValue("@TransactionsExecuted", TransactionsExecuted);
                cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
                cmd.Parameters.AddWithValue("@CallbackURL", "");
                cmd.Parameters.AddWithValue("@WorkerStatus", WorkerStatus.Idle);
                cmd.Parameters.AddWithValue("@TransactionProcessingId", WorkerId);
                cmd.Parameters.AddWithValue("@ThreadId", WorkerId);
                ret = cmd.ExecuteNonQuery();
                con.Close();
            }
            return ret > 0;
        }
        public void ResetWorkers()
        {
            string sql = @"UPDATE [dbo].[Worker]
                           SET [WorkerStatus] = 0";

            using (var con = new SqlConnection(this._dbConnectionString))
            {
                con.Open();
                var cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
    public enum WorkerStatus
    {
        Idle,
        Busy,
        Error
    }
}
