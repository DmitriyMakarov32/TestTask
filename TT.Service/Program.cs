using Mapster;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using TT.Business;
using TT.Business.Mapping;
using TT.Clients;
using TT.Clients.Base;
using TT.Data;
using TT.Shared.Extensions;
using TT.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

TypeAdapterConfig.GlobalSettings.Scan(typeof(MappingConfig).Assembly,
    typeof(ProviderBase<,>).Assembly);
builder.Services.AddControllers();
builder.Services.AddClients(builder.Configuration);
builder.Services.AddData(builder.Configuration);
builder.Services.AddBusiness(builder.Configuration);
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("postgresConnection"))
    .AddRabbitMQ(new Uri(builder.Configuration
        .GetSection("RabbitSettings")
        .Get<RabbitSettings>().GetConnectionString()));



var app = builder.Build();

TT.Data.Initialization.DatabaseInitializer.Initialize(app);

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();

