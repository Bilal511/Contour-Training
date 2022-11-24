using System;
using TIDRolloverModels.Models;
using TrainingDataLayer.Model;
using TrainingDataLayer.Training;
using TrainingModels.Models.TestModel;
using TrainingModels.Models.TIDRolloverModels;

namespace TrainingBusinessLayer
{
    class ModelMapping
    {
        public static Test MapTest(TestCtrlRequest test, ITrainingSettings settings)
        {
            return new TrainingDataLayer.TrainingDataLayer(settings).TestFactory.Create(x =>
            {
                x.Id = test.Id;
                x.FirstName = test.FirstName;
                x.LastName = test.LastName;
                x.CellNumber = test.CellNumber;
            });
        }

        public static TestCtrlResponse MapTest(Test test)
        {
            return new TestCtrlResponse()
            {
                TestCtrl = new TestCtrl()
                {
                    Id = test.Id,
                    FirstName = test.FirstName,
                    LastName = test.LastName,
                    CellNumber = test.CellNumber,
                }

            };
        }
     
        public static Test MapTest(DeleteTestByIdCtrlRequest test, ITrainingSettings settings)
        {
            return new TrainingDataLayer.TrainingDataLayer(settings).TestFactory.Create(x =>
            {
                x.Id = test.Id;
            });
        }


        public static TidRolloverResponse MapTidRolloverToTidRolloverResponse(TidRollover request)
        {
            return new TidRolloverResponse()
            {
                TidRolloverId = request.TidRolloverId,
                MeterNo = request.MeterNo,
                Municipality = request.Municipality,
                Suburb = request.Suburb,
                PostalCode = request.PostalCode,
                CustomerEmail = request.CustomerEmail,
                CustomerCellNo = request.CustomerCellNo,
                TIDInterimStatus = request.TIDInterimStatus,
                DateOfRequest = request.DateOfRequest,
                TIDMeterBaseDate = request.TIDMeterBaseDate,
                TIDKRN = request.TIDKRN,
                TIDSGC = request.TIDSGC,
                TIDTI = request.TIDTI,
                EntityCode = request.EntityCode,
                DateTokensGenerated = request.DateTokensGenerated,
                NumberOfElectricityPurchases = request.NumberOfElectricityPurchases,
                DateElecPurchase1 = request.DateElecPurchase1,
                DateElecPurchase2 = request.DateElecPurchase2,
                DateElecPurchase3 = request.DateElecPurchase3,
                WardCode = request.WardCode,
                GeneratedBy = request.GeneratedBy,
                ActiveFlag = request.ActiveFlag
            };
        }

        public static TidRollover MapTidRolloverCtrlToTidRollover(TIDRolloverCtrl request, ITrainingSettings settings)
        {
            return new TrainingDataLayer.TrainingDataLayer(settings).TidRolloverFactory.Create(x =>
            {
                x.TidRolloverId = request.TidRolloverId;
                x.MeterNo = request.MeterNumber;
                x.Municipality = request.Municipality;
                x.Suburb = request.Suburb;
                x.PostalCode = request.PostalCode;
                x.CustomerEmail = request.CustomerEmail;
                x.CustomerCellNo = request.CustomerCellNo;
                x.DateOfRequest = (DateTime)request.DateOfRequest;
                x.TIDMeterBaseDate = (DateTime)request.TIDMeterBaseDate;
                x.TIDKRN = request.TIDKRN;
                x.TIDSGC = request.TIDSGC;
                x.TIDTI = request.TIDTI;
                x.EntityCode = request.EntityCode;
                x.DateTokensGenerated = request.DateTokensGenerated;
                x.NumberOfElectricityPurchases = request.NumberOfElectricityPurchases;
                x.DateElecPurchase1 = request.DateElecPurchase1;
                x.DateElecPurchase2 = request.DateElecPurchase2;
                x.DateElecPurchase3 = request.DateElecPurchase3;
                x.WardCode = request.WardCode;
                x.GeneratedBy = request.GeneratedBy;
                x.ActiveFlag = request.ActiveFlag;
            });
        }
    }
}
