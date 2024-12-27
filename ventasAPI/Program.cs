
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ventasAPI;
using ventasAPI.Services;

var builder = WebApplication.CreateBuilder(args);


//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();

    });
});


// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile)); // Registra el perfil de AutoMapper

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Enable CORS

// Add services to the container.

builder.Services.AddControllers();
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
