using Crud.Api.Infrastructure;
using Crud.Application;
using Crud.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapEndpoints();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.Run();

public partial class Program
{
}