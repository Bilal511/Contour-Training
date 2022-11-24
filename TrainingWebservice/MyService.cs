using System;
using TrainingClientLibrary;

namespace TrainingWebservice
{
    public class MyService
    {
        private const string UserName = "Bilal";
        private const string Password = "Basketball";
        private const string ClintUrl = "https://localhost:44362/";

        private readonly TrainingClient _trainingClient;

        public MyService()
        {
            _trainingClient = new TrainingClient(UserName, Password, ClintUrl);
        }

         public void BackUpTidRollOver() 
        {
            try { _trainingClient.CompareList(); }
            catch
            {
                // ignored
            }
        }

         private void ProcessTidRollOverBackUp()
         {
            
             try
             {

                 BackUpTidRollOver();
             }
             catch (Exception ex)
             {
                 throw;
             }
         }
    }
}
