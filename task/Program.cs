using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using task.Models;
using task.Repositories.ProductRepository;
using task.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




// to inform app the dbcontext


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



// to enable cors that prevent external requests if it disable 

    //builder.Services.AddCors();


builder.Services.AddSwaggerGen();

// Use Repository 
builder.Services.AddScoped<IProductRepository, ProductRepository >();

//add Identity User
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//Authentication service

builder.Services.AddScoped<IAuthService,AuthService>();


//configer JWT

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(o =>
  {
      o.RequireHttpsMetadata = false;
      o.SaveToken = true;
      o.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidIssuer = builder.Configuration["JWT:Issuer"],
          ValidAudience = builder.Configuration["JWT:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
      };
  });

var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

// second step to enable cors  must be  before authorization not after 
// allow any header that deal with requests and read data from api
// allow any method that deal with requests and read data from api
// allow any any url (origin) that deal with requests and read data from api

   //app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); 

//must test authentication before authorization

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
