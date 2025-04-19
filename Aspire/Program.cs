using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);


// var sql = builder.AddSqlServer("sql")
//     .WithLifetime(ContainerLifetime.Persistent);
//
// var db = sql.AddDatabase("database");
//
// var salesService = builder.AddProject<Projects.API>("api")
//     .WithReference(db);
//

// this is just to make it simpler to open both projects, its not part of the course but its something
// new from MS so its definitely something worth to checking out

var salesService = builder.AddProject<Projects.API>("api");

builder.AddProject<Projects.Frontend>("frontend")
    .WithExternalHttpEndpoints()
    .WithReference(salesService)
    .WaitFor(salesService);

builder.Build().Run();