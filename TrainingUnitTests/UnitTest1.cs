using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingClientLibrary;
using TrainingModels.Models.TestModel;

namespace TrainingUnitTests
{
    [TestClass]
    public class UnitTest1
    {

        public static readonly string UserName = "Bilal";
        public static readonly string Password = "Basketball";
        public static readonly string ClintUrl = "https://localhost:44362/";

        [TestMethod]
        public void Insert()
        {
            var clint = new TrainingClient(UserName, Password, ClintUrl);
            var result = clint.Insert(new TestCtrlRequest(){ FirstName = "Toby", LastName = "Sims", CellNumber = "0826485138" });
            Assert.IsTrue(result is TestCtrlResponse);
        }


        [TestMethod]
        public void GetById()
        {
            var clint = new TrainingClient(UserName, Password, ClintUrl);
            var result = clint.GetById(new GetTestByIdCtrlRequest() { Id = 14 });
            Assert.IsTrue(result is TestCtrlResponse);
        }

        [TestMethod]
        public void Update()
        {
            var clint = new TrainingClient(UserName, Password, ClintUrl);

            var result = clint.Update(new TestCtrlRequest() { Id=14,FirstName = "Toby", LastName = "Sims", CellNumber = "0826485138" });
            Assert.IsTrue(result is TestCtrlResponse);
        }
        [TestMethod]
        public void Delete()
        {
            var clint = new TrainingClient(UserName, Password, ClintUrl);

            var result = clint.DeleteById(new DeleteTestByIdCtrlRequest() { Id = 15});
            Assert.IsTrue(result is TestCtrlResponse);
        }

        //[TestMethod]
        //public void CompareList()
        //{
        //    var clint = new TrainingClient(UserName, Password, ClintUrl);
        //    var result = clint.CompareList();
        //    Assert.IsTrue(result is TestCtrlResponse);
        //}

    }


}
