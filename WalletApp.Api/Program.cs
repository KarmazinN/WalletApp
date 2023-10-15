using Microsoft.EntityFrameworkCore;
using WalletApp.Domain.DataBase;
using WalletApp.Domain.Repositories.Implementation;
using WalletApp.Domain.Repositories.Interface;
using WalletApp.Application;
using WalletApp.Application.Interfaces;
using WalletApp.Application.Services;
using WalletApp.Api.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationsContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddEntityRepositories();
builder.Services.AddEntityServices();
builder.Services.AddUnitOfWork();
builder.Services.AddAutoMapperConfig();
builder.Services.AddGenericRepository();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
