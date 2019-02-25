using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Services.Z_Harag.BlackList;
using Nop.Services.Z_Harag.Category;
using Nop.Services.Z_Harag.Comment;
using Nop.Services.Z_Harag.Message;
using Nop.Services.Z_Harag.Notification;
using Nop.Services.Z_Harag.Post;
using Nop.Services.Z_Harag.Rate;
using Nop.Services.Z_Harag.Reports;

using Nop.Services.Z_Harag.Payment;

//using Nop.Services.Z_Harag.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Services.Z_Harag.Follow;

namespace Nop.Web.Infrastructure.Harag
{
    public class DependencyRegistrar : IDependencyRegistrar
    {

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            //register SquarePaymentManager 
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
           // builder.RegisterType<SubCategoryService>().As<ISubCategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
            builder.RegisterType<BlackListService>().As<IBlackListService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageService>().As<IMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerLifetimeScope();
            builder.RegisterType<RateService>().As<IRateSrevice>().InstancePerLifetimeScope();
            builder.RegisterType<ReportService>().As<IReportService>().InstancePerLifetimeScope();

            // Harag Payment Form
            builder.RegisterType<PaymentService>().As<IPaymentService>().InstancePerLifetimeScope();
            builder.RegisterType<FollowService>().As<IFollowService>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 1; }
        }
    }
}
