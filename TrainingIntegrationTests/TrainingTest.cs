using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using TrainingClientLibrary;
using TrainingModels.Models.TestModel;
using ContourConstants;


namespace TrainingIntegrationTests
{
    public class TrainingTest
    {
        private static readonly Random Random = new Random();

        public const string TrainingDatabaseRelativePath = @"C:\Users\BilalM\source\repos\TrainingAPI\TrainingIntegrationTests\Data\BilalDB.bak";

        private TestDatabase _testDb;

        private string _password;
        private TrainingClient _client;
        private int _portTracker = 44362;
        

        [OneTimeSetUp]
        public void Setup()
        {
            this._testDb = new TestDatabase();

            this._testDb.RestoreDatabase(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), TrainingDatabaseRelativePath),
                                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            this._password = "Basketball";
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            this._testDb?.Dispose();
        }

        private string GetBaseAddress()
        {
            this._portTracker++;

            return $"http://localhost:{this._portTracker}/";
        }

        public TrainingClient InstantiateClient(string baseAddress)
        {
            return new TrainingClient("Bilal", _password, baseAddress);
        }

        [Test]
        public void CanInitialiseAndPerformSimpleQueryOnTestDatabase()
        {
            using (var dummyTestDatabase = new TestDatabase())
            {
                dummyTestDatabase.RestoreDatabase(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), TrainingDatabaseRelativePath),
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                using (var connection = dummyTestDatabase.GetConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"select COUNT(*) from Test";
                    var result = command.ExecuteScalar();
                    Assert.True(result is int);
                }
            }
        }

        [Test]
        public void CanStartUpDatabaseAndServerTogether()
        {
            var dummyDb = new TestDatabase();

            dummyDb.RestoreDatabase(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                    TrainingDatabaseRelativePath), Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var baseAddress = GetBaseAddress();
            using (new WebServer(baseAddress, dummyDb.GetConnectionString()))
            {
                this._client = InstantiateClient(baseAddress);
                var response = _client.GetById(new GetTestByIdCtrlRequest(){Id = 22});
                Assert.NotNull(response.TestCtrl.FirstName);
            }
        }
    }
}
