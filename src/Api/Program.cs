using FluentValidation;
using HumbleMediator;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using SimpleInjector;
using WebApiTemplate.Api;
using WebApiTemplate.Application;
using WebApiTemplate.Application.Logging;
using WebApiTemplate.Application.Products.Commands;
using WebApiTemplate.Application.Products.Queries;
using WebApiTemplate.Application.Validation;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Products;
using WebApiTemplate.Infrastructure.Persistence;
using WebApiTemplate.Infrastructure.Products;
using Container = SimpleInjector.Container;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog(); // replace built-in logging with Serilog

    // Add services to the container.
    builder.Services.AddControllers();

    // swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddHealthChecks().AddDbContextCheck<AppDbContext>();
    builder.Services.AddScoped<GlobalExceptionHandlerMiddleware>();
    // Persistence
    var connString =
        builder.Configuration.GetConnectionString("Default")
        ?? throw new ArgumentNullException("connectionString");

    builder.Services.AddPooledDbContextFactory<AppDbContext>((sp, options) =>
        options.UseSqlServer(connString));

    builder.Services.AddDbContext<AppDbContext>((sp, options) =>
        options.UseSqlServer(connString), ServiceLifetime.Singleton);


    builder.Services.AddDistributedMemoryCache();

    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

    // SimpleInjector
    var container = WebApiTemplate.Api.Program.Container;
    container.Options.DefaultLifestyle = Lifestyle.Singleton;
    builder.Services.AddSimpleInjector(
        container,
        options => options.AddAspNetCore().AddControllerActivation()
    );

    container.Register<IProductReadRepository, ProductReadRepository>();
    container.Register<IProductWriteRepository, ProductWriteRepository>();
    container.Register<IUnitOfWorkFactory, UnitOfWorkFactory>();

    // validators
    container.Collection.Register(
        typeof(IValidator<>),
        typeof(GetProductByIdQueryValidator).Assembly
    );

    // mediator
    container.Register<IMediator>(() => new Mediator(container.GetInstance));

    // mediator handlers
    container.Register(typeof(ICommandHandler<,>), typeof(CreateProductCommandHandler).Assembly);
    container.Register(typeof(IQueryHandler<,>), typeof(GetProductByIdQueryHandler).Assembly);

    // mediator handlers decorators - queries pipeline
    container.RegisterDecorator(
        typeof(IQueryHandler<,>),
        typeof(QueryHandlerValidationDecorator<,>)
    );
    container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(QueryHandlerCachingDecorator<,>));
    container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(QueryHandlerLoggingDecorator<,>));

    // mediator handlers decorators - commands pipeline
    container.RegisterDecorator(
        typeof(ICommandHandler<,>),
        typeof(CommandHandlerValidationDecorator<,>)
    );
    container.RegisterDecorator(
        typeof(ICommandHandler<,>),
        typeof(CommandHandlerLoggingDecorator<,>)
    );

    var app = builder.Build();

    app.Services.UseSimpleInjector(container);

    // Apply pending EF Core migrations automatically in development mode.
    // To do that in production, especially in multi-instance scenarios, you need
    // to make sure that migrations are applied as a separate deploy step to prevent data corruption.
    if (app.Environment.IsDevelopment())
    {
        var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<AppDbContext>>();
        var dbContext = await dbContextFactory.CreateDbContextAsync();
        if (dbContext.Database.IsRelational())
        {
            // It will throw if the db is not relational
            await dbContext.Database.MigrateAsync();
        }
    }

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }

    app.MapHealthChecks("/health");

    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    app.UseAuthorization();
    app.MapControllers();

    container.Verify();

    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

namespace WebApiTemplate.Api
{
    public partial class Program
    {
        public static readonly Container Container = new();
    }
}
