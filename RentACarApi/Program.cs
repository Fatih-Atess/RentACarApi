using RentACarApi.Repositories;
using RentACarApi.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Replace with your actual frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger (OpenAPI) dokümantasyonu için gerekli servisler
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SOLID: Dependency Injection (Bağımlılık Enjeksiyonu) Kaydımız
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();

var app = builder.Build();
// test

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Geliştirme ortamında görsel Swagger arayüzünü aktifleştir
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:4200")
          .AllowAnyMethod()
          .AllowAnyHeader()
);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();