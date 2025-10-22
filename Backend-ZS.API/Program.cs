using Backend_ZS.API.Data;
using Backend_ZS.API.Mappings;
using Backend_ZS.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject DbContext
builder.Services.AddDbContext<ZsDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ZSConnectionString")));

// Inject AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfiles));

// Inject Repositories
builder.Services.AddScoped<IClientRepository, SqlClientRepository>();

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
