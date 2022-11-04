var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("CacheConnectionString");
    options.SchemaName = "dbo";
    options.TableName = "CacheTable";
});

builder.Services.AddDistributedSqlServerCache(options => {
    options.DefaultSlidingExpiration = TimeSpan.FromMinutes(10);
});

builder.Services.AddDistributedSqlServerCache(options => {
    options.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(6);
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
