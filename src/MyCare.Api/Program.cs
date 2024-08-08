using Microsoft.EntityFrameworkCore;
using MyCare.Application.Services.Medicine;
using MyCare.Application.Services.Password;
using MyCare.Application.Services.User;
using MyCare.Application.UseCases.Medicine;
using MyCare.Application.UseCases.User;
using MyCare.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<IMedicineInterface, MedicineService>();
builder.Services.AddScoped<IPasswordInterface, PasswordService>();


builder.Services.AddDbContext<MyCareDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
