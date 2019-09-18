using Autofac;
using Autofac.Integration.Mvc;
using KiteConnectWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KiteConnectWeb
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            // You can register controllers all at once using assembly scanning...
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
          
            builder.RegisterType<ConfigSettings>().As<IConfigSettings>().SingleInstance();
            builder.RegisterType<ConnectHelper>().As<IConnectHelper>().SingleInstance();
            builder.RegisterType<ConnectLogger>().As<IConnectLogger>().SingleInstance();

            Container = builder.Build();

            return Container;
        }
    }
}