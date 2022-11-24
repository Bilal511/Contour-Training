using System.Data;
using System.Data.SqlClient;

namespace TrainingDataLayer.Model
{
    public class Test
    {
        private readonly string _dbConnectionString;

        public Test(string dbConnectionString)
        {
            this._dbConnectionString = dbConnectionString;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CellNumber { get; set; }


        public bool Insert()
        {
            using (var dbConnection = new SqlConnection(this._dbConnectionString))
            {
                var cmd = new SqlCommand(@"INSERT INTO [dbo].[Test]
                                                       ([FirstName]
                                                       ,[LastName]
                                                       ,[CellNumber])
                                                 VALUES
                                                       (@FirstName
                                                       ,@LastName
                                                       ,@CellNumber)", dbConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FirstName", this.FirstName);
                cmd.Parameters.AddWithValue("@LastName", this.LastName);
                cmd.Parameters.AddWithValue("@CellNumber", this.CellNumber);
                dbConnection.Open();
                var insertedRow = cmd.ExecuteNonQuery();
                dbConnection.Close();
                if (insertedRow >= 1)
                    return true;
            }
            return false;
        }



        public int Update()
        {
            var affectedRows = 0;
            using (var dbConnection = new SqlConnection(this._dbConnectionString))
            {
                var cmd = new SqlCommand(@"UPDATE [dbo].[Test]
                                                   SET [FirstName] = (@FirstName)
                                                      ,[LastName] =  (@LastName)
                                                      ,[CellNumber] =  (@CellNumber)
                                                  WHERE [Id] = (@Id)", dbConnection);
                cmd.Parameters.AddWithValue("@Id", this.Id);
                cmd.Parameters.AddWithValue("@FirstName", this.FirstName);
                cmd.Parameters.AddWithValue("@LastName", this.LastName);
                cmd.Parameters.AddWithValue("@CellNumber", this.CellNumber);
                dbConnection.Open();
                affectedRows = cmd.ExecuteNonQuery();
                dbConnection.Close();
            }
            return affectedRows;
        }

        public bool DeleteById()
        {
            using (var dbConnection = new SqlConnection(this._dbConnectionString))
            {
                var cmd = new SqlCommand(@"DELETE FROM [dbo].[Test] WHERE [Id] = (@Id)", dbConnection);
                cmd.Parameters.AddWithValue("@Id", this.Id);
                dbConnection.Open();
                var deletedRows = cmd.ExecuteNonQuery();
                dbConnection.Close();
                if (deletedRows >= 1)
                    return true;
            }

            return false;
        }

    }
}