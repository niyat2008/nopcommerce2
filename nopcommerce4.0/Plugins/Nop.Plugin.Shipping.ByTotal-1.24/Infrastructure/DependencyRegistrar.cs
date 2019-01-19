using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Shipping.ByTotal.Data;
using Nop.Plugin.Shipping.ByTotal.Domain;
using Nop.Plugin.Shipping.ByTotal.Services;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Shipping.ByTotal.Infrastructure
{
    /// <summary>
    /// Dependency Registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register plugin services
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type Finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<ShippingByTotalService>().As<IShippingByTotalService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<ShippingByTotalObjectContext>(builder, "nop_object_context_shipping_total");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<ShippingByTotalRecord>>()
                .As<IRepository<ShippingByTotalRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_shipping_total"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}