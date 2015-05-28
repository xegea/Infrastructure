using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Infrastructure.Ioc
{
    public sealed class WindsorDependencyScope : IDependencyScope
    {
        private readonly IWindsorContainer container;
        private readonly IDisposable scope;

        public WindsorDependencyScope(IWindsorContainer container)
        {
            this.container = container;
            this.scope = container.BeginScope();
        }

        public object GetService(Type t)
        {
            if (container.Kernel.HasComponent(t)) return container.Resolve(t);

            return null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return container.ResolveAll(t).Cast<object>();
        }

        public void Dispose()
        {
            scope.Dispose();
        }
    }
}
