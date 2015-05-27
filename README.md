Infrastructure.Dapper
===================

How to use it
----------------

1) Install NuGet package

2) Add Connection String called "Db" in Web.config file.

```
<connectionStrings>
    <add name="Db" connectionString="Server=MySqlServer;Initial Catalog=MyDb;Integrated Security=false;user id=user;password=pass;" />
    ...
  
```
3) Set in the IoC Container the IDapperRepository implementation
```
container.Register(Component.For<IDapperRepository>().ImplementedBy<DapperRepository>().LifestyleTransient());
```
4) Use the DapperRepository as follows

```
public class MyService : IMyService
{
    private readonly IDapperRepository dapperRepository;
    public DeliveryRepository(IDapperRepository dapperRepository)
    {
        this.dapperRepository = dapperRepository;
    }

    public IEnumerable<Delivery> GetDataByDate(DateTime day)
    {
        return dapperRepository.Query<Delivery>("spGetMyDatabyDate", new {dateFrom = day}, CommandType.StoredProcedure);
    }
}
```
