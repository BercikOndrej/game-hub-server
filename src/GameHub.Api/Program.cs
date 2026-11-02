using GameHub.Application.Configuration;
using GameHub.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();

// Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
