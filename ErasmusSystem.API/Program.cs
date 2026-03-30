using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ErasmusSystem.Business;
using ErasmusSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using ErasmusSystem.DataAccess.Repositories;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// PostgreSQL veritabaný baðlantýsýnýn sisteme tanýtma
builder.Services.AddDbContext<ErasmusDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ApplicationService>();

// JWT Kimlik Doðrulama Ayarlarý
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecretKey"]);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; // Geliþtirme ortamýnda (localhost) sorun çýkmamasý için
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, // Þimdilik esnek tut
        ValidateAudience = false
    };
});

// Biri senden IUserRepository isterse, ona UserRepository sýnýfýný ver
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Merkezi Hata Yönetimi
app.UseMiddleware<ErasmusSystem.API.Middlewares.GlobalExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
