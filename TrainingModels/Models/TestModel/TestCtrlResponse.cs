using ContourConstants.CustomerPortalClasses;

namespace TrainingModels.Models.TestModel
{
    public class TestCtrlResponse : GenericResponse
    {
        public TestCtrl TestCtrl { get; set; }
        public int RowsAffected { get; set; }
    }
}
