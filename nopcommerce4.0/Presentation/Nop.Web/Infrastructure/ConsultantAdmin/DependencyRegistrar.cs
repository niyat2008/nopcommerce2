using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
//using Nop.Services.Z_Consultant.Category;
using Nop.Services.Z_Consultant.Comment;
using Nop.Services.Z_ConsultantAdmin.Post;
//using Nop.Services.Z_Consultant.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Services.Z_ConsultantAdmin;
using Nop.Services.Z_ConsultantAdmin.Customers;
using Nop.Services.Z_ConsultantAdmin.Categories;
using Nop.Services.Z_ConsultantAdmin.SubCategories;

namespace Nop.Web.Infrastructure.ConsultantAdmin
{
    public class DependencyRegistrar : IDependencyRegistrar
    {

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            //register SquarePaymentManager
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<SubCategoryService>().As<ISubCategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomersService>().As<ICustomerService>().InstancePerLifetimeScope();
            //builder.RegisterType<CustomersService>().As<ICustomerService>().InstancePerLifetimeScope();
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
