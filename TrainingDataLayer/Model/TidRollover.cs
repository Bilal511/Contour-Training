using System;
using System.Data;
using System.Data.SqlClient;

namespace TrainingDataLayer.Model
{
    public class TidRollover
    {
        private readonly string _dbConnectionString;

        public TidRollover(string dbConnectionString)
        {
            this._dbConnectionString = dbConnectionString;
        }

        public int TidRolloverId { get; set; }
        public string MeterNo { get; set; }
        public string Municipality { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerCellNo { get; set; }
        public int TIDInterimStatus { get; set; }
        public DateTime DateOfRequest { get; set; }
        public DateTime TIDMeterBaseDate { get; set; }
        public int TIDKRN { get; set; }
        public string TIDSGC { get; set; }
        public string TIDTI { get; set; }
        public string EntityCode { get; set; }
        public DateTime? DateTokensGenerated { get; set; }
        public int NumberOfElectricityPurchases { get; set; }
        public DateTime? DateElecPurchase1 { get; set; }
        public DateTime? DateElecPurchase2 { get; set; }
        public DateTime? DateElecPurchase3 { get; set; }
        public int WardCode { get; set; }
        public string GeneratedBy { get; set; }
        public bool ActiveFlag { get; set; }

        public bool Insert()
        {
            using var dbConnection = new SqlConnection(this._dbConnectionString);
            var cmd = new SqlCommand(@"INSERT INTO [dbo].[TIDRollover]
                                                                   ([TidRolloverId]
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
                                                                   ,[ActiveFlag])
                                                             VALUES
                                                                   (@TidRolloverId,
                                                                   @MeterNo,
                                                                   @Municipality,
                                                                   @Suburb,
                                                                   @PostalCode,
                                                                   @CustomerEmail,
                                                                   @CustomerCellNo,
                                                                   @TIDInterimStatus,
                                                                   @DateOfRequest,
                                                                   @TIDMeterBaseDate,
                                                                   @TIDKRN,
                                                                   @TIDSGC,
                                                                   @TIDTI, 
                                                                   @EntityCode,
                                                                   @DateTokensGenerated, 
                                                                   @NumberOfElectricityPurchases, 
                                                                   @DateElecPurchase1, 
                                                                   @DateElecPurchase2, 
                                                                   @DateElecPurchase3, 
                                                                   @WardCode, 
                                                                   @GeneratedBy,
                                                                   @ActiveFlag)", dbConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@TidRolloverId", string.IsNullOrEmpty(this.TidRolloverId.ToString()) ? DBNull.Value : this.TidRolloverId);
            cmd.Parameters.AddWithValue("@MeterNo", string.IsNullOrEmpty(this.MeterNo) ? DBNull.Value : this.MeterNo);
            cmd.Parameters.AddWithValue("@Municipality", string.IsNullOrEmpty(this.Municipality) ? DBNull.Value : this.Municipality);
            cmd.Parameters.AddWithValue("@Suburb", string.IsNullOrEmpty(this.Suburb) ? DBNull.Value : this.Suburb);
            cmd.Parameters.AddWithValue("@PostalCode", string.IsNullOrEmpty(this.PostalCode) ? DBNull.Value : this.PostalCode);
            cmd.Parameters.AddWithValue("@CustomerEmail", string.IsNullOrEmpty(this.CustomerEmail) ? DBNull.Value : this.CustomerEmail);
            cmd.Parameters.AddWithValue("@CustomerCellNo", string.IsNullOrEmpty(this.CustomerCellNo) ? DBNull.Value : this.CustomerCellNo);
            cmd.Parameters.AddWithValue("@TIDInterimStatus", string.IsNullOrEmpty(this.TIDInterimStatus.ToString()) ? DBNull.Value : this.TIDInterimStatus);
            cmd.Parameters.AddWithValue("@DateOfRequest", DateOfRequest == default(DateTime) ? DBNull.Value : this.DateOfRequest);
            cmd.Parameters.AddWithValue("@TIDMeterBaseDate", TIDMeterBaseDate == default(DateTime) ? DBNull.Value : this.TIDMeterBaseDate);
            cmd.Parameters.AddWithValue("@TIDKRN", string.IsNullOrEmpty(this.TIDKRN.ToString()) ? DBNull.Value : this.TIDKRN);
            cmd.Parameters.AddWithValue("@TIDSGC", string.IsNullOrEmpty(this.TIDSGC) ? DBNull.Value : this.TIDSGC);
            cmd.Parameters.AddWithValue("@TIDTI", string.IsNullOrEmpty(this.TIDTI) ? DBNull.Value : this.TIDTI);
            cmd.Parameters.AddWithValue("@EntityCode", string.IsNullOrEmpty(this.EntityCode) ? DBNull.Value : this.EntityCode);
            cmd.Parameters.AddWithValue("@DateTokensGenerated", DateTokensGenerated == default(DateTime) ? DBNull.Value : this.DateTokensGenerated);
            cmd.Parameters.AddWithValue("@NumberOfElectricityPurchases", string.IsNullOrEmpty(this.NumberOfElectricityPurchases.ToString()) ? DBNull.Value : this.NumberOfElectricityPurchases);
            cmd.Parameters.AddWithValue("@DateElecPurchase1", this.DateElecPurchase1 == default(DateTime) ? DBNull.Value : this.DateElecPurchase1);
            cmd.Parameters.AddWithValue("@DateElecPurchase2", this.DateElecPurchase2 == default(DateTime) ? DBNull.Value : this.DateElecPurchase2);
            cmd.Parameters.AddWithValue("@DateElecPurchase3", this.DateElecPurchase3 == default(DateTime) ? DBNull.Value : this.DateElecPurchase3);
            cmd.Parameters.AddWithValue("@WardCode", string.IsNullOrEmpty(this.WardCode.ToString()) ? DBNull.Value : this.WardCode);
            cmd.Parameters.AddWithValue("@GeneratedBy", string.IsNullOrEmpty(this.GeneratedBy) ? DBNull.Value : this.GeneratedBy);
            cmd.Parameters.AddWithValue("@ActiveFlag", string.IsNullOrEmpty(this.ActiveFlag.ToString()) ? DBNull.Value : this.ActiveFlag);

            dbConnection.Open();
            var insertedRow = cmd.ExecuteNonQuery();
            dbConnection.Close();
            return insertedRow >= 1;
        }
    }

    public enum TidInterimStatus
    {
        Identified,
        Scheduled,
        Requested,
        Generated,
        Communicated,
        PendingConfirmation,
        Confirmed
    }
}
