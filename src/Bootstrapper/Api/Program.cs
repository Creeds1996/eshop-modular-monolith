var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) => 
    config.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services
    .AddProblemDetails()
    .AddExceptionHandler<CustomExceptionHandler>();

// Add services to the container
var app = builder.Build();

app.MapCarter();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline
app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

app.UseExceptionHandler();

app.Run();