using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SmartTradeAdvisor.Core.Configurations;
using SmartTradeAdvisor.Core.IndexCalculators;
using SmartTradeAdvisor.Core.IndexService;
using SmartTradeAdvisor.Data.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddDbContext<IndexDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.Configure<AlgorithmsConfiguration>(builder.Configuration.GetSection("AlgorithmConfiguration"));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IIndexCalculatorFactory, IndexCalculatorFactory>();

builder.Services.AddScoped<IIndexService, IndexService>();

var app = builder.Build();

app.MapControllers();

app.Run();

