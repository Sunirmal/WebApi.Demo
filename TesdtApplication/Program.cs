using Microsoft.AspNetCore.Http.Features;
using TesdtApplication.Caching;
using TesdtApplication.Middleware;
using TesdtApplication.Models;
using TesdtApplication.Services;
using WebApi.Demo.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSingleton(new LRUCache<int, Customer>(100));
builder.Services.AddSingleton<CustomerService>();
builder.Services.AddSingleton<ITransactionProcessingService, TransactionProcessingService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();
var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
