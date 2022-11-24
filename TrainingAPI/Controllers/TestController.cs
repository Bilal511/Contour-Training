using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainingModels.Models.TestModel;

namespace TrainingAPI.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        private readonly TrainingBusinessLayer.TrainingBL _trainingBusiness;

        public TestController(TrainingBusinessLayer.TrainingBL trainingBusiness)
        {
            this._trainingBusiness = trainingBusiness;
        }
        
        [Authorize]
        [HttpPost, Route("GetById")]
        public HttpResponseMessage GetById([FromBody] GetTestByIdCtrlRequest testCtrl)
        {
            return Request.CreateResponse(HttpStatusCode.OK,this._trainingBusiness.GetById(testCtrl));
        }

        [Authorize]
        [HttpPost, Route("Insert")]
        public HttpResponseMessage Insert([FromBody] TestCtrlRequest testCtrl)
        {
            return Request.CreateResponse(HttpStatusCode.OK,_trainingBusiness.Insert(testCtrl));
        }

        [Authorize]
        [HttpPost, Route("Update")]
        public HttpResponseMessage Update([FromBody] TestCtrlRequest testCtrl)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _trainingBusiness.Update(testCtrl));
        }

        [Authorize]
        [HttpPost, Route("DeleteById")]
        public HttpResponseMessage DeleteById([FromBody] DeleteTestByIdCtrlRequest testCtrl)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _trainingBusiness.Delete(testCtrl));
        }


        [HttpPost, Route("CompareList")]
        public void CompareList()
        {
            _trainingBusiness.CompareList();
        }
    }
}