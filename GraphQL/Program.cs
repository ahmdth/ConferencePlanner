using ConferencePlanner.GraphQL.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<ApplicationDbContext>(
        options => options.UseNpgsql("Host=127.0.0.1;Port=5432;Username=postgres;Password=;Database=graphql_workshop;"))
    .AddGraphQLServer()
    .AddDbContextCursorPagingProvider()
    .AddPagingArguments()
    .AddFiltering()
    .AddSorting()
    .AddRedisSubscriptions(_ => ConnectionMultiplexer.Connect("127.0.0.1:6379"))
    .AddGraphQLTypes()
    .AddGlobalObjectIdentification()
    .AddMutationConventions();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseWebSockets();
app.MapGraphQL();
await app.RunWithGraphQLCommandsAsync(args);
