using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ContourConstants.CustomerPortalClasses;
using TIDRolloverClient;
using TIDRolloverModels.Models;
using TrainingDataLayer.Training;
using TrainingModels.Models.TestModel;
using TrainingModels.Models.TIDRolloverModels;


namespace TrainingBusinessLayer
{
    public class TrainingBL
    {
        private readonly ITrainingSettings _trainingSettings;
        private readonly TrainingDataLayer.TrainingDataLayer _trainingDataLayer;
        private readonly TIDClient _tidRolloverClient;

        public TrainingBL(ITrainingSettings trainingSettings)
        {
            this._trainingSettings = trainingSettings;
            this._tidRolloverClient = new TIDClient("Rehen", "Basketball", "https://localhost:44384/"); ;
            this._trainingDataLayer = new TrainingDataLayer.TrainingDataLayer(this._trainingSettings);
        }

        public TestCtrlResponse Insert(TestCtrlRequest testCtrlObj)
        {
            var testResponseModel = new TestCtrlResponse();
            try
            {
                var test = ModelMapping.MapTest(testCtrlObj, _trainingSettings);
                if (test.Insert())
                {
                    testResponseModel.Status = ResponseStatus.Success;
                    return testResponseModel;
                }
                else
                {
                    testResponseModel.Status = ResponseStatus.Fail;
                    testResponseModel.Error = new Error() { ErrorMessage = "Error inserting data" };
                    return testResponseModel;
                }
            }
            catch (Exception e)
            {
                testResponseModel.Status = ResponseStatus.Error;
                testResponseModel.Error = new Error() { ExceptionMessage = e.Message, ErrorMessage = "Error inserting data" };
                return testResponseModel;
            }
        }

        public TestCtrlResponse GetById(GetTestByIdCtrlRequest testCtrl)
        {
            var testResponseModel = new TestCtrlResponse();
            try
            {
                var result = _trainingDataLayer.TestFactory.GetById(testCtrl.Id);
                testResponseModel = ModelMapping.MapTest(result);

                if (testResponseModel.TestCtrl.Id > 0)
                {
                    testResponseModel.Status = ResponseStatus.Success;
                    return testResponseModel;
                }
                else
                {
                    testResponseModel.Status = ResponseStatus.Fail;
                    testResponseModel.Error = new Error() { ErrorMessage = "Error finding data" };
                    return testResponseModel;
                }
            }
            catch (Exception e)
            {
                testResponseModel.Status = ResponseStatus.Error;
                testResponseModel.Error = new Error() { ExceptionMessage = e.Message, ErrorMessage = "Error finding data" };
                return testResponseModel;
            }
        }

        public TestCtrlResponse Update(TestCtrlRequest testCtrlObj)
        {
            var testResponseModel = new TestCtrlResponse();
            try
            {
                var result = _trainingDataLayer.TestFactory.GetById(testCtrlObj.Id);
                if (result == null)
                {
                    testResponseModel.Status = ResponseStatus.Fail;
                    testResponseModel.Error.ErrorMessage = "Error finding data";
                    return testResponseModel;
                }

                var mappedModel = ModelMapping.MapTest(testCtrlObj, _trainingSettings);
                var query = mappedModel.Update();
                if (query >= 1)
                {
                    testResponseModel.Status = ResponseStatus.Success;
                    testResponseModel.RowsAffected = query;
                    return testResponseModel;
                }
                else
                {
                    testResponseModel.Status = ResponseStatus.Fail;
                    testResponseModel.Error = new Error() { ErrorMessage = "Error updating data" };
                    return testResponseModel;
                }
            }
            catch (Exception e)
            {
                testResponseModel.Status = ResponseStatus.Error;
                testResponseModel.Error = new Error() { ExceptionMessage = e.Message, ErrorMessage = "Error updating data" };
                return testResponseModel;
            }
        }

        public TestCtrlResponse Delete(DeleteTestByIdCtrlRequest testCtrlObj)
        {
            var testResponseModel = new TestCtrlResponse();
            try
            {
                var result = _trainingDataLayer.TestFactory.GetById(testCtrlObj.Id);
                if (result == null)
                {
                    testResponseModel.Status = ResponseStatus.Fail;
                    testResponseModel.Error.ErrorMessage = "Error finding data";
                    return testResponseModel;
                }

                var mappedModel = ModelMapping.MapTest(testCtrlObj, _trainingSettings);
                var query = mappedModel.DeleteById();
                if (query)
                {
                    testResponseModel.Status = ResponseStatus.Success;
                    return testResponseModel;
                }
                else
                {
                    testResponseModel.Status = ResponseStatus.Fail;
                    testResponseModel.Error = new Error() { ErrorMessage = "Error deleting data" };
                    return testResponseModel;
                }
            }
            catch (Exception e)
            {
                testResponseModel.Status = ResponseStatus.Error;
                testResponseModel.Error = new Error() { ExceptionMessage = e.Message, ErrorMessage = "Error deleting data" };
                return testResponseModel;
            }
        }
        public List<TidRolloverResponse> GetAllTidRollover()
        {
            var model = _trainingDataLayer.TidRolloverFactory.GetAllTidRollover();
            return model.Select(ModelMapping.MapTidRolloverToTidRolloverResponse).ToList();
        }

        public TestCtrlResponse InsertTidRollover(TIDRolloverCtrl tidRolloverCtrl)
        {
            var testResponseModel = new TestCtrlResponse();
            try
            {
                var mappedModel = ModelMapping.MapTidRolloverCtrlToTidRollover(tidRolloverCtrl, _trainingSettings);
                mappedModel.TIDInterimStatus = (int)(TIDRolloverModels.Models.TIDInterimStatus)Enum.Parse(typeof(TIDRolloverModels.Models.TIDInterimStatus), tidRolloverCtrl.TIDInterimStatus.ToString());
                testResponseModel.Status = mappedModel.Insert() ? ResponseStatus.Success : ResponseStatus.Error;
                return testResponseModel;
            }
            catch (Exception e)
            {
                testResponseModel.Status = ResponseStatus.Error;
                testResponseModel.Error = new Error() { ExceptionMessage = e.Message, ErrorMessage = "Error Inserting data" };
                return testResponseModel;
            }
        }

        public void CompareList()
        {
            var myList = GetAllTidRollover();
            var tidList = _tidRolloverClient.GetAll();

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            //foreach (var item in from item in tidList let item1 = item where myList.All(x => x.TidRolloverId != item1.TidRolloverId) select item)
            //{
            //    InsertTidRollover(item);
            //    }
            Parallel.ForEach(tidList, item =>
            {
                if (myList.All(x => x.TidRolloverId != item.TidRolloverId))
                    InsertTidRollover(item);
                
            });
            watch.Stop();
        }
    }
}