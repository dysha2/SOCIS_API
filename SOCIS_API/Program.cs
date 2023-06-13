global using SOCIS_API.Model;
global using SOCIS_API.Repositoryies;
global using SOCIS_API.Controllers;
global using SOCIS_API.Interfaces;
global using System.Linq;
global using Microsoft.EntityFrameworkCore;
global using HelpfulProjectCSharp;
global using HelpfulProjectCSharp.ASP;
global using SOCIS_API.ModelDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = builder.Configuration.GetSection("AuthOptions:Issuer").Value,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = builder.Configuration.GetSection("AuthOptions:Audience").Value,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AuthOptions:Key").Value)),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<EquipmentContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IAuthRep, AuthRep>();
builder.Services.AddTransient<IRequestRep, RequestRep>();
builder.Services.AddTransient<IPersonRep, PersonRep>();
builder.Services.AddTransient<IWorkOnRequestRep, WorkOnRequestRep>();
builder.Services.AddTransient<ICrudRep, CrudRep<EquipmentContext>>();
builder.Services.AddTransient<IRequestUnitsRep, RequestUnitsRep>();
builder.Services.AddTransient<IFullNameUnitRep, FullNameUnitRep>();
builder.Services.AddTransient<IUnitRespPersonRep, UnitRespPersonRep>();
builder.Services.AddTransient<IPlaceRep, PlaceRep>();
builder.Services.AddTransient<IUnitPlaceRep, UnitPlaceRep>();
builder.Services.AddTransient<IShortTermMoveRep, ShortTermMoveRep>();
builder.Services.AddTransient<IAccountingUnitRep, AccountingUnitRep>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(opt => { opt.AllowAnyHeader(); opt.AllowAnyOrigin(); opt.AllowAnyMethod(); });
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
