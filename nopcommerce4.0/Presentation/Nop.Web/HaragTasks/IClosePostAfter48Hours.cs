using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.ConsultantTasks
{
    public interface IHaragPostPostsTracking
    {
        void RefreashPostRequestService();
        void StartPostDeletingService();
        void SetPostsUnFeaturedHaragPostService();
    }
}
