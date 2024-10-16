using Business.Implementation;
using Business.Interface;
using Microsoft.Extensions.Caching.Memory;
using Parser.Implementation;
using Parser.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IJsonParserService, JsonParserService>();
builder.Services.AddScoped<ILoaderService, LoaderService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddKeyedSingleton<IMemoryCache, MemoryCache>("products");


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
