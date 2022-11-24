using System.Configuration;

namespace TrainingDataLayer.Training
{
    public abstract class ITrainingSettings
    {
        public abstract string GetDbConnectionString();
    }

    public class TrainingSettings:ITrainingSettings
    {
        public override string GetDbConnectionString()
        {
            return  ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
    }

    public class TestTrainingSettings : ITrainingSettings
    {
        private readonly string _connectionString;

        public TestTrainingSettings(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public override string GetDbConnectionString()
        {
            return this._connectionString;
        }
    }


    }
