using Keycloak.AuthServices.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) => 
    config.ReadFrom.Configuration(context.Configuration));

var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;
var orderingAssembly = typeof(OrderingModule).Assembly;

builder.Services
    .AddCarterWithAssemblies(
        catalogAssembly,
        basketAssembly,
        orderingAssembly
    );

builder.Services
    .AddMediatRWithAssemblies(
        catalogAssembly, 
        basketAssembly,
        orderingAssembly
    );

builder.Services
    .AddValidatorsFromAssemblies(
        [catalogAssembly, basketAssembly, orderingAssembly]
    );

builder.Services
    .AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
    });

builder.Services
    .AddMassTransitWithAssemblies(
        builder.Configuration,
        catalogAssembly,
        basketAssembly,
        orderingAssembly
    );

builder.Services
    .AddKeycloakWebApiAuthentication(builder.Configuration);

builder.Services
    .AddAuthorization();

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
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline
app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

app.UseExceptionHandler();

app.Run();