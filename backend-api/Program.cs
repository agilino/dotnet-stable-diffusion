using backend_api.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    // connect within net work
    //options.Configuration = "redis:6379"; // redis is the container name of the redis service. 6379 is the default port
    // Connect via localhost
    options.Configuration = "localhost:6379"; // redis is the container name of the redis service. 6379 is the default port
    options.InstanceName = "SampleInstance";
});



// Add Postgres DB context
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("image-db"));
});

// As soon as our application starts and initializes AutoMapper,
// AutoMapper will scan our application and look for classes that
// inherit from the Profile class and load their mapping configurations.
// But may not good for performance
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();
