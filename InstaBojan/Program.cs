using InstaBojan.Core.Models;
using InstaBojan.Helpers;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



//EntityFrameworkCore
builder.Services.AddDbContext<InstagramStoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BokiInsta")));

//Authentication
builder.Services.AddAuthentication(options =>
{
   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
   options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
   options.SaveToken = true;
   options.RequireHttpsMetadata = false;
options.TokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidAudience = null,
    ValidIssuer = null,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-long-secret-key"))
   };
});




//Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IUserRepository, UsersRepository>();
builder.Services.AddScoped<IProfilesRepository, ProfilesRepository>();
builder.Services.AddScoped<ICompanyMapper, CompanyMapper>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
