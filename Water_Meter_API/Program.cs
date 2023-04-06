using Microsoft.EntityFrameworkCore;
using Water_Meter_Model;
using System.Configuration;
using Water_Meter_Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configura politica de Default para CORS de tal forma que el API pueda ser llamado desde cualquier lugar
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
                                                     builder.AllowAnyOrigin().
                                                     AllowAnyHeader().
                                                     AllowAnyMethod()));

//Agrega DBContext a Dependency Injection
builder.Services.AddDbContext<Utsa_WaterMeterContext>(dbcontextoptions =>
{
   
  dbcontextoptions.UseSqlServer(builder.Configuration.GetConnectionString("WaterMeterConnectionString"));
  dbcontextoptions.EnableSensitiveDataLogging();
  //dbcontextoptions.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
}
);

// Autoregistro de servicios en Dependency Injection via libreria de escaneo
builder.Services.Scan(options =>
{
  options.FromAssemblyOf<deviceService>()
         .AddClasses(publicOnly: true)
         .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
         .AsSelf()
         .WithScopedLifetime();
}
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
