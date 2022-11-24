using TrainingDataLayer.Factories;
using TrainingDataLayer.Training;

namespace TrainingDataLayer
{
    public class TrainingDataLayer
    {
        private readonly ITrainingSettings _trainingSettings;
        private readonly string _dbConnectionString;
        private readonly TestFactory _testFactory;
        private readonly TidRolloverFactory _tidRolloverFactory;

        public TrainingDataLayer(ITrainingSettings trainingSettings)
        {
            this._trainingSettings = trainingSettings;
            this._dbConnectionString = this._trainingSettings.GetDbConnectionString();
            this._testFactory = new TestFactory(this._dbConnectionString);
            this._tidRolloverFactory = new TidRolloverFactory(this._dbConnectionString);
        }

        public TestFactory TestFactory
        {
            get => this._testFactory;
            private set { }
        }

        public TidRolloverFactory TidRolloverFactory
        {
            get => this._tidRolloverFactory;
            private set { }
        }
    }
}
