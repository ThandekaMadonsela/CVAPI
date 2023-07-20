using CVAPI.Data;
using CVAPI.DataSeeding;
using CVAPI.Interfaces;
using CVAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Added by me
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<ICVInterface, CVRepository>();
builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            // Disable reference loop handling to avoid $id fields
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// Seed the data
await app.SeedDataAsync();
app.Run();
