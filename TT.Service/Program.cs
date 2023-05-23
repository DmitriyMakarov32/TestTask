using Mapster;
using TT.Business;
using TT.Business.Mapping;
using TT.Clients;
using TT.Data;

var builder = WebApplication.CreateBuilder(args);

TypeAdapterConfig.GlobalSettings.Scan(typeof(MappingConfig).Assembly,
    typeof(TT.Clients.ProviderOne.Mapping.MappingConfig).Assembly);
builder.Services.AddControllers();
builder.Services.AddClients(builder.Configuration);
builder.Services.AddData(builder.Configuration);
builder.Services.AddBusiness(builder.Configuration);
builder.Services.AddSwaggerGen();

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

