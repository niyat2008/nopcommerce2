using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Services.Z_HaragAdmin.BlackList;
using Nop.Services.Z_HaragAdmin.Categories;
using Nop.Services.Z_HaragAdmin.Comment;
using Nop.Services.Z_Harag.Message;
using Nop.Services.Z_Harag.Notification;
using Nop.Services.Z_HaragAdmin.Post;
using Nop.Services.Z_HaragAdmin.Rate;
using Nop.Services.Z_HaragAdmin.Report;
//using Nop.Services.Z_Harag.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Services.Z_HaragAdmin.Customers;
using Nop.Services.Z_HaragAdmin.Cities;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Z_HaragAdmin.Setting;
using Nop.Services.Z_HaragAdmin.BankAccount;

namespace Nop.Web.Infrastructure.HaragAdmin
{
    public class DependencyRegistrar:IDependencyRegistrar
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
            builder.RegisterType<RateService>().As<IRateService>().InstancePerLifetimeScope();
            builder.RegisterType<ReportService>().As<IReportService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<CityService>().As<ICityService>().InstancePerLifetimeScope();
            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();
            builder.RegisterType<BankAccountService>().As<IBankAccountService>().InstancePerLifetimeScope();

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
