using Castle.Windsor;
using System;

namespace Infrastructure.Ioc
{
    public static class ContainerManager
    {
        public static IWindsorContainer Container { get; set; }

        public static T Resolve<T>()
        {
            if (Container.Kernel.HasComponent(typeof(T)))
                return (T)Container.Resolve(typeof(T));

            return default(T);
        }

        public static object GetService(Type t)
        {
            if (Container.Kernel.HasComponent(t))
                return Container.Resolve(t);

            return null;
        }
    }
}
