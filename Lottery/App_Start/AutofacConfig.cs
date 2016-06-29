using Autofac;
using Autofac.Integration.WebApi;
using Lottery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Lottery
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterType<LotteryGeneratorService>().As<ILotteryGeneratorService>().InstancePerRequest();
            builder.RegisterApiControllers(typeof(AutofacConfig).Assembly);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}