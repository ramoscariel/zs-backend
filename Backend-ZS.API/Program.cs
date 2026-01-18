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
        p.WithOrigins("http://localhost:3000", "https://localhost:3000") // Next dev
         .AllowAnyHeader()
         .AllowAnyMethod()
    // Si en algún momento usas cookies/autenticación, agrega:
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- activar CORS antes de MapControllers ---
app.UseCors(corsPolicy);

app.UseAuthorization();
app.MapControllers();
app.Run();
