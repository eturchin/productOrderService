using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductOrderService.DataContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")!);
});

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });

var mapperConfiguration = new MapperConfiguration(p => { p.AddMaps(Assembly.GetExecutingAssembly()); });
var mapper = mapperConfiguration.CreateMapper();

builder.Services.AddScoped<IMapperBase>(_ => mapper);
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IMapperBase>(_ => mapper);

builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{ 
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var dbContext = services.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
        logger.LogInformation("Database successfully have been migrated.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();