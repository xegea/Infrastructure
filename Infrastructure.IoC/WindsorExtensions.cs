using Castle.MicroKernel;
using Castle.MicroKernel.Handlers;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using System;
using System.Linq;
using System.Text;

namespace Infrastructure.Ioc
{
    public class WindsorExtensions
    {
        public static void CheckForPotentiallyMisconfiguredComponents(IWindsorContainer container)
        {
            var host = (IDiagnosticsHost)container.Kernel.GetSubSystem(SubSystemConstants.DiagnosticsKey);
            var diagnostics = host.GetDiagnostic<IPotentiallyMisconfiguredComponentsDiagnostic>();

            var handlers = diagnostics.Inspect();

            if (handlers.Any())
            {
                var message = new StringBuilder();
                var inspector = new DependencyInspector(message);

                foreach (IExposeDependencyInfo handler in handlers)
                {
                    handler.ObtainDependencyDetails(inspector);
                }

                throw new Exception(message.ToString());
            }
        }
    }
}
