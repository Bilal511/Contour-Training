using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingDataLayer.Model;

namespace TrainingDataLayer.Factories
{
    public class TidRolloverFactory
    {
        private readonly string _dbConnectionString;

        public TidRolloverFactory(string dbConnectionString)
        {
            this._dbConnectionString = dbConnectionString;
        }

        public TidRollover Create(Action<TidRollover> initializer)
        {
            var tidRollover = new TidRollover(this._dbConnectionString);
            initializer(tidRollover);
            return tidRollover;
        }


        public List<TidRollover> GetAllTidRollover()
        {
            var tidRolloverList = new List<TidRollover>();
            using (var dbConnection = new SqlConnection(this._dbConnectionString))
            {
                var cmd = new SqlCommand(@"SELECT [TidRolloverId]
                                     ,[MeterNo]
                                     ,[Municipality]
                                     ,[Suburb]
                                     ,[PostalCode]
                                     ,[CustomerEmail]
                                     ,[CustomerCellNo]
                                     ,[TIDInterimStatus]
                                     ,[DateOfRequest]
                                     ,[TIDMeterBaseDate]
                                     ,[TIDKRN]
                                     ,[TIDSGC]
                                     ,[TIDTI]
                                     ,[EntityCode]
                                     ,[DateTokensGenerated]
                                     ,[NumberOfElectricityPurchases]
                                     ,[DateElecPurchase1]
                                     ,[DateElecPurchase2]
                                     ,[DateElecPurchase3]
                                     ,[WardCode]
                                     ,[GeneratedBy]
                                     ,[ActiveFlag]
                                 FROM TIDRollover", dbConnection);
                dbConnection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tidRolloverList.Add(new TidRollover(this._dbConnectionString)
                    {
                        TidRolloverId = Convert.ToInt32((reader["TidRolloverId"] as int?).GetValueOrDefault()),
                        MeterNo = reader["MeterNo"].ToString() ?? string.Empty,
                        Municipality = reader["Municipality"].ToString() ?? string.Empty,
                        Suburb = reader["Suburb"].ToString() ?? string.Empty,
                        PostalCode = reader["PostalCode"].ToString() ?? string.Empty,
                        CustomerEmail = reader["CustomerEmail"].ToString() ?? string.Empty,
                        CustomerCellNo = reader["CustomerCellNo"].ToString() ?? string.Empty,
                        TIDInterimStatus = Convert.ToInt32((reader["TIDInterimStatus"] as int?).GetValueOrDefault()),
                        DateOfRequest = Convert.ToDateTime((reader["DateOfRequest"] as DateTime?).GetValueOrDefault()),
                        TIDMeterBaseDate = Convert.ToDateTime((reader["TIDMeterBaseDate"] as DateTime?).GetValueOrDefault()),
                        TIDKRN = Convert.ToInt32((reader["TIDKRN"] as int?).GetValueOrDefault()),
                        TIDSGC = reader["TIDSGC"].ToString() ?? string.Empty,
                        TIDTI = reader["TIDTI"].ToString() ?? string.Empty,
                        EntityCode = reader["EntityCode"].ToString() ?? string.Empty,
                        DateTokensGenerated = Convert.ToDateTime((reader["DateTokensGenerated"] as DateTime?).GetValueOrDefault()),
                        NumberOfElectricityPurchases = Convert.ToInt32((reader["NumberOfElectricityPurchases"] as int?).GetValueOrDefault()),
                        DateElecPurchase1 = Convert.ToDateTime((reader["DateElecPurchase1"] as DateTime?).GetValueOrDefault()),
                        DateElecPurchase2 = Convert.ToDateTime((reader["DateElecPurchase2"] as DateTime?).GetValueOrDefault()),
                        DateElecPurchase3 = Convert.ToDateTime((reader["DateElecPurchase3"] as DateTime?).GetValueOrDefault()),
                        WardCode = Convert.ToInt32((reader["WardCode"] as int?).GetValueOrDefault()),
                        GeneratedBy = reader["GeneratedBy"].ToString() ?? string.Empty,
                        ActiveFlag = Convert.ToBoolean((reader["ActiveFlag"] as bool?).GetValueOrDefault())
                    });
                }
                dbConnection.Close();
            }
            return tidRolloverList;
        }
    }
}
