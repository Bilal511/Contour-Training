using System;
using Contour.BaseClient;
using Newtonsoft.Json;
using TrainingModels.Models.TestModel;

namespace TrainingClientLibrary
{
    public class TrainingClient : BaseClient
    {
        public TrainingClient(string username, string password, string clientUrl) : base(username, password, clientUrl)
        {
        }

        public TestCtrlResponse Insert(TestCtrlRequest testObj)
        {
            const string endPoint = "api/test/Insert";
            try
            {
                var response = PerformPostOperation(endPoint, testObj);
                return JsonConvert.DeserializeObject<TestCtrlResponse>(response);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public TestCtrlResponse GetById(GetTestByIdCtrlRequest testByIdCtrlRequest)
        {
            const string endPoint = "api/test/GetById";
            try
            {
                var response = PerformPostOperation(endPoint, testByIdCtrlRequest);
                return JsonConvert.DeserializeObject<TestCtrlResponse>(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public TestCtrlResponse Update(TestCtrlRequest testCtrl)
        {
            const string endPoint = "api/test/Update";
            try
            {
                var response = PerformPostOperation(endPoint, testCtrl);
                return JsonConvert.DeserializeObject<TestCtrlResponse>(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public TestCtrlResponse DeleteById(DeleteTestByIdCtrlRequest testCtrl)
        {
            const string endPoint = "api/test/DeleteById";
            try
            {
                var response = PerformPostOperation(endPoint, testCtrl);
                return JsonConvert.DeserializeObject<TestCtrlResponse>(response);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void CompareList()
        {
            const string endPoint = "api/test/CompareList";
            try
            {
                var response = PerformPostOperation(endPoint,null);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
