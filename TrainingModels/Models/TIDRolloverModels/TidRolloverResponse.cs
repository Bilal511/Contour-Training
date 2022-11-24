using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingModels.Models.TIDRolloverModels
{
    public class TidRolloverResponse
    {
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
    }
}
