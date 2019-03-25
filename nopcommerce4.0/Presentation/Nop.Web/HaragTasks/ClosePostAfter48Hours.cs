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
    public class HaragPostPostsTracking : IHaragPostPostsTracking
    {
        ILocalizationService _localizationService;

        public HaragPostPostsTracking(ILocalizationService localizationService)
        {
            this._localizationService = localizationService;
        }

        public void RefreashPostRequestService()
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
                            command.CommandText = "checkHaragPostTime";
                            
                            sqlConnection.Open();
                            int u = command.ExecuteNonQuery(); 
                        }
                    }
                     Thread.Sleep(TimeSpan.FromHours(1).Milliseconds);
                }

              

            }).Start();



        }

        public void StartPostDeletingService()
        {
                            Console.WriteLine("StartPostDeletingService: ");
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
                            command.CommandText = "deleteHaragPost";
                            sqlConnection.Open();
                            int u = command.ExecuteNonQuery(); 
                        }
                    }
                    Thread.Sleep(TimeSpan.FromHours(5).Milliseconds);
                }

                
            }).Start();



        }


        public void SetPostsUnFeaturedHaragPostService()
        {
                            Console.WriteLine("SetPostsUnFeaturedHaragPostService: ");
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
                            command.CommandText = "SetPostsUnFeaturedHaragPost";
                            sqlConnection.Open();
                            int u = command.ExecuteNonQuery();
                        }
                    }
                     Thread.Sleep(TimeSpan.FromHours(1).Milliseconds);
                }

                
            }).Start();



        }




    }
}
