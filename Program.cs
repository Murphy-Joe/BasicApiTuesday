
using BasicApi.Data;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Starting up!");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); // Set up the controller style of building an API.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BasicDataContext>(opts =>
{
    // TODO: NEVER DO THIS.
    opts.UseSqlServer(builder.Configuration.GetConnectionString("basic"));
});

// Services have to be registered before this .Build() call.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // when we do configuration - I'll tell you how it knows!
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers(); // Ok, ready - go read all those routing attributes [HttpGet("/status")] 
Console.WriteLine("Fixin to run!");
app.Run(); // It starts running here!
Console.WriteLine("Done running");
