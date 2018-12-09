using Nop.Core.Data;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nop.Web.ConsultantTasks
{
    public class ClosePostAfter48Hours : IClosePostAfter48Hours
    {
        ILocalizationService _localizationService;

        public ClosePostAfter48Hours(ILocalizationService localizationService)
        {
            this._localizationService = localizationService;
        }

        public void StartClosingService()
        {
            new Thread(() =>
            {
                while (true)
                {
                    var connectionString = new DataSettingsManager().LoadSettings().DataConnectionString;
                    using (var sqlConnection = new SqlConnection(connectionString))
                    {
                        using (var command = sqlConnection.CreateCommand())
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "closePosts";
                            sqlConnection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                    int numberOfMinutes = 60;
                    try
                    {
                        if (int.TryParse(_localizationService.GetResource("Consultant.Api.Post.NumberOfMinutesToWaitBetweenEveryClosePostsTask"), out numberOfMinutes))
                        {
                            Thread.Sleep(numberOfMinutes * 60 * 1000);
                        }
                        else
                        {
                            Thread.Sleep(numberOfMinutes * 60 * 1000);//1 hour
                        }
                    }
                    catch { }

                }
            }).Start();

        }



    }
}
