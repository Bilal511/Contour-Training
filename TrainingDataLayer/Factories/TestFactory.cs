using System;
using System.CodeDom;
using System.Data.SqlClient;
using System.Data;
using TrainingDataLayer.Model;
using static System.Int32;

namespace TrainingDataLayer.Factories
{
    public class TestFactory
    {
        private readonly string _dbConnectionString;

        public TestFactory(string dbConnectionString)
        {
            this._dbConnectionString = dbConnectionString;
        }

        public Test Create(Action<Test> initializer)
        {
            var test = new Test(this._dbConnectionString);
            initializer(test);
            return test;
        }
     
        public Test GetById(int id)
        {
            var testObj = Create(x => { });
            using (var dbConnection = new SqlConnection(this._dbConnectionString))
            {
                var cmd = new SqlCommand(@"SELECT [Id]
                                                        ,[FirstName]
                                                        ,[LastName]
                                                        ,[CellNumber]
                                                   FROM [dbo].[Test]
                                                   WHERE [Id] = (@id)", dbConnection);
                cmd.Parameters.AddWithValue("@id", id);
                dbConnection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        testObj.Id = Parse(reader["Id"].ToString());
                        testObj.FirstName = reader["FirstName"].ToString();
                        testObj.LastName = reader["LastName"].ToString();
                    }
                }
                dbConnection.Close();
            }
            return testObj;
        }
        
    }
}
