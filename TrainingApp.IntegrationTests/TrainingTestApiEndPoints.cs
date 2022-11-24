using TraningClientLibrary;

namespace TrainingApp.IntegrationTests
{
    public class TrainingTestApiEndPoints
    {
        private TraningClient traningClient;

        public TraningClient InstantiateClient()
        {
            return new TraningClient("Bilal", "12345", "https://localhost:44362/");
        }

        public void TestGet()
        {
            //using (var server = new WebServer("https://localhost:44362/", TestConnectionString))
            //{
            //    this.traningClient = InstantiateClient();
            //    var response = traningClient.GetFirstRecord();
            //}
        }
    }
}
