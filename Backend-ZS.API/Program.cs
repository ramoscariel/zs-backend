using System.Text.Json.Serialization;
using Backend_ZS.API.Data;
using Backend_ZS.API.Mappings;
using Backend_ZS.API.Repositories;
using Backend_ZS.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- CORS: permitir llamadas desde el front ---
var corsPolicy = "_nextCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, p =>
        p.WithOrigins(
            "http://localhost:3000",
            "https://localhost:3000",
            // prod: tu front en Vercel/Netlify/etc (ajusta el dominio real)
            "https://zs-frontend.vercel.app"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
    // .AllowCredentials()
    );
});

// MVC
builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        o.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<ZsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZSConnectionString")));

// AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfiles));

// Repos
builder.Services.AddScoped<IClientRepository, SqlClientRepository>();
builder.Services.AddScoped<IBarProductRepository, SqlBarProductRepository>();
builder.Services.AddScoped<IPaymentRepository, SqlPaymentRepository>();
builder.Services.AddScoped<ITransactionRepository, SqlTransactionRepository>();
builder.Services.AddScoped<IBarOrderRepository, SqlBarOrderRepository>();
builder.Services.AddScoped<IKeyRepository, SqlKeyRepository>();
builder.Services.AddScoped<IAccessCardRepository, SqlAccessCardRepository>();
builder.Services.AddScoped<IParkingRepository, SqlParkingRepository>();
builder.Services.AddScoped<IEntranceTransactionRepository, SqlEntranceTransactionRepository>();
builder.Services.AddScoped<IEntranceAccessCardRepository, SqlEntranceAccessCardRepository>();

// Services
builder.Services.AddScoped<IBarOrderService, SqlBarOrderService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ZsDbContext>();
    db.Database.SetCommandTimeout(300);
    db.Database.Migrate();
}


// ✅ Swagger habilitado SIEMPRE (Production incluido)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend-ZS.API v1");
    c.RoutePrefix = "swagger"; // https://zs-backend-api-fbb7ftdzfxd3bqh9.canadacentral-01.azurewebsites.net/swagger
});

app.UseHttpsRedirection();

// --- activar CORS antes de MapControllers ---
app.UseCors(corsPolicy);

app.UseAuthorization();
app.MapControllers();

app.Run();
