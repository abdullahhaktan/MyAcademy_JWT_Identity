using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicApp.API.Data.Context;
using MusicApp.API.Data.Entities;
using MusicApp.API.Services.TokenServices;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddIdentityCore<AppUser>()
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>();

// Kimlik doðrulama (Authentication) sistemini JWT þemasýyla çalýþacak þekilde yapýlandýrýyoruz.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        // Gelen Token'ýn geçerli olup olmadýðýný hangi kriterlere göre kontrol edeceðiz?
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // 1. Ýmza Kontrolü: Token bizim gizli anahtarýmýzla (Key) mi imzalanmýþ?
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

            // 2. Yayýncý Kontrolü: Bu token'ý bizim API (Issuer) mý üretmiþ?
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            // 3. Hedef Kitle Kontrolü: Bu token bizim Uygulamamýz (Audience) için mi üretilmiþ?
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            // 4. Zaman Kontrolü: Token'ýn süresi (Expire Date) dolmuþ mu?
            ValidateLifetime = true,

            // 5. Hassasiyet: Sunucu saati ile dünya saati arasýndaki 5 dk'lýk esneklik payýný sýfýrlýyoruz.
            // Süre bittiði saniyede token geçersiz olsun diye.
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlConnection");

    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers().AddJsonOptions(config=>
{
    config.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MusicApp API", Version = "v1" });

    // 1. Swagger'a "Bearer" þemasýný tanýmlýyoruz
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Lütfen baþýna 'Bearer ' yazarak tokený yapýþtýrýn. Örn: 'Bearer abc123...'",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // 2. Tüm API metodlarýna bu güvenlik gereksinimini ekliyoruz
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
var app = builder.Build();

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
