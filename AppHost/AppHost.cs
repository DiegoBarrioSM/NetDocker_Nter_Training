using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin();

var myDatabase = postgres.AddDatabase("dbAspire");

builder.AddProject<NetDocker>("api")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Aspire")
    .WithReference(myDatabase)    
    .WaitFor(myDatabase);

builder.Build().Run();