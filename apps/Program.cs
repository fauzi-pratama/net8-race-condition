
using apps.Configs;
using apps.Services;
using apps.Models.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

//Config Env
builder.Services.Configure<AppConfig>(builder.Configuration);
AppConfig? appConfig = builder.Configuration.Get<AppConfig>();

//Config PostgreSql
builder.Services.AddDbContext<DataDbContext>(options =>
{
    options.UseNpgsql(appConfig!.PostgreSqlConnectionString);
});

//Config Redis
builder.Services.AddSingleton<IDatabase>(provider =>
{
    var redisConfig = ConfigurationOptions.Parse(appConfig!.RedisConnectionString!);
    redisConfig.AbortOnConnectFail = false; 
    redisConfig.ConnectRetry = 3; 
    redisConfig.ConnectTimeout = 5000;
    var redisConn = ConnectionMultiplexer.Connect(redisConfig);

    return redisConn.GetDatabase(); 
});

//Config Automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Confgi DI
builder.Services.AddScoped<IMasterService, MasterService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

//Config Controller
builder.Services.AddControllers();

//Config Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Apply MigrateDb
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataDbContext>();
    db.Database.Migrate();
}

//Run Swagger
app.UseSwagger();
app.UseSwaggerUI();

//Run Auth
app.UseAuthorization();

//Run Controllers
app.MapControllers();

//Run App
app.Run();
