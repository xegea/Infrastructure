Infrastructure
===================

Infrastructure.Dapper
---------------------

Wrapper for Dapper

How to use it
----------------

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
