using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Infrastructure.Ioc
{
    public sealed class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            this.container = container;
        }

        public object GetService(Type t)
        {
            if (container.Kernel.HasComponent(t))
                return container.Resolve(t);

            return null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return container.ResolveAll(t).Cast<object>();
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(container);
        }

        public void Dispose()
        {
            container.Dispose();
        }

    }
}
