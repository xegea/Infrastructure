Infrastructure
===================

Infrastructure.IoC
------------------

Wrapper for Castle Windsor IoC

Dependency Resolver version
---------------------------
Use DependencyResolver with IDependencyScope.

> **More info:**

> - Mike Hadlow, "The MVC 3.0 IDependencyResolver interface is broken. Don’t use it with Windsor," 2011.
http://mikehadlow.blogspot.com/2011/02/mvc-30-idependencyresolver-interface-is.html 



1) Create a class WebApiControllerInstaller.cs
```
public class WebApiControllerInstaller : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        container.Register(Component.For<IMyInterface>().ImplementedBy<IMyImplementation>().LifestyleScoped());
        ....
        
        container.Register(Classes.FromThisAssembly().BasedOn<IHttpController>().LifestyleScoped());

        WindsorExtensions.CheckForPotentiallyMisconfiguredComponents(container);
    }
}
```
2) In Global.asax Application_Start()
```
var container = new WindsorContainer().Install(new WebApiControllerInstaller());
var httpDependencyResolver = new WindsorDependencyResolver(container);
GlobalConfiguration.Configuration.DependencyResolver = httpDependencyResolver;
```
Composition Root version
------------------------
1) Create a class WebApiControllerInstaller.cs
```
public class WebApiControllerInstaller : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        container.Register(Component.For<IMyInterface>().ImplementedBy<IMyImplementation>().LifestyleTransient());
        ....
        
        container.Register(Classes.FromThisAssembly().BasedOn<IHttpController>().LifestyleTransient());

        WindsorExtensions.CheckForPotentiallyMisconfiguredComponents(container);
    }
}
```
2) In Startup.cs 
```
var container = new WindsorContainer().Install(new WebApiControllersInstaller());
config.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
ContainerManager.Container = container;
```

In case of ASP.NET MVC check this link: https://github.com/castleproject/Windsor/blob/master/docs/mvc-tutorial-intro.md

----------


Infrastructure.Dapper
---------------------

Wrapper for Dapper

How to use it
-------------

1) Add Connection String called "Db" in Web.config file.

```
<connectionStrings>
    <add name="Db" connectionString="Server=MySqlServer;Initial Catalog=MyDb;Integrated Security=false;user id=user;password=pass;" />
    ...
  
```
2) Set in the IoC Container the IDapperRepository implementation
```
container.Register(Component.For<IDapperRepository>().ImplementedBy<DapperRepository>().LifestyleTransient());
```
3) Use the DapperRepository as follows

```
public class MyService : IMyService
{
    private readonly IDapperRepository dapperRepository;
    public MyService(IDapperRepository dapperRepository)
    {
        this.dapperRepository = dapperRepository;
    }

    public IEnumerable<MyEntity> TableGetData()
    {
        return dapperRepository.Query<MyEntity>("Select Id, Name from MyTable").ToList();
    }
    
    public IEnumerable<MyEntity> TableGetData(DateTime day)
    {
        return dapperRepository.Query<MyEntity>("Select Id, Name from MyTable where dateFrom = @day", new { @day = day } ).ToList();
    }
    
    public IEnumerable<MyEntity> StoredProcedureGetDataByDate(DateTime day)
    {
        return dapperRepository.Query<MyEntity>("spGetMyDatabyDate", new { dateFrom = day }, CommandType.StoredProcedure);
    }
}
```
