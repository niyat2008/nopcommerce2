using Nop.Core.Infrastructure;
using System.Data.Entity;

namespace Nop.Plugin.Shipping.ByTotal.Data
{
    /// <summary>
    /// EntityFramework Startup Task
    /// </summary>
    public class EfStartUpTask : IStartupTask
    {
        /// <summary>
        /// Execute task
        /// </summary>
        public void Execute()
        {
            //It's required to set initializer to null (for SQL Server Compact).
            //otherwise, you'll get something like "The model backing the 'your context name' context has changed since the database was created. Consider using Code First Migrations to update the database"
            Database.SetInitializer<ShippingByTotalObjectContext>(null);
        }

        /// <summary>
        /// Order of task implementation
        /// </summary>
        public int Order
        {
            //ensure that this task is run first
            get { return 0; }
        }
    }
}