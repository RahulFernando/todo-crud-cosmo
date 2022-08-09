using Microsoft.Azure.Cosmos;
using Todo_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITodoService>(options =>
{
    string url = builder.Configuration.GetSection("AzureCosmoDBSettings").GetValue<string>("URL");
    string primaryKey = builder.Configuration.GetSection("AzureCosmoDBSettings").GetValue<string>("PrimaryKey");
    string dbName = builder.Configuration.GetSection("AzureCosmoDBSettings").GetValue<string>("DatabaseName");
    string containerName = builder.Configuration.GetSection("AzureCosmoDBSettings").GetValue<string>("ContainerName");

    var cosmosClient = new CosmosClient(url, primaryKey);

    return new TodoService(cosmosClient, dbName, containerName);
});

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
