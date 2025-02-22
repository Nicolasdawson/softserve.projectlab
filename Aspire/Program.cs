var builder = DistributedApplication.CreateBuilder(args);

// var sql = builder.AddSqlServer("sql")
//     .WithLifetime(ContainerLifetime.Persistent);
//
// var db = sql.AddDatabase("database");
//
// var salesService = builder.AddProject<Projects.API>("api")
//     .WithReference(db);
//
// builder.AddProject<Projects.Frontend>("frontend")
//     .WithExternalHttpEndpoints()
//     .WithReference(salesService)
//     .WaitFor(salesService);

var salesService = builder.AddProject<Projects.API>("api");

builder.AddProject<Projects.Frontend>("frontend")
    .WithExternalHttpEndpoints()
    .WithReference(salesService)
    .WaitFor(salesService);

builder.Build().Run();