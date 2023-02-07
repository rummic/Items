using Items.API.Services.ColorsService;
using Items.API.Services.UsersServices;
using Items.Data;
using Items.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

AddJwtToken();
RegisterServices();

var app = builder.Build();

app.MapHealthChecks("/status");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var salesContext = scope.ServiceProvider.GetRequiredService<ItemsDbContext>();
        salesContext.Database.EnsureDeleted();
        salesContext.Database.EnsureCreated();
    }
}

app.Run();


void AddJwtToken()
{
    var secret = builder.Configuration["JwtTokenSecret"];
    var key = Encoding.ASCII.GetBytes(secret);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

void RegisterServices()
{
    builder.Services.AddDbContext<ItemsDbContext>(options =>
        options.UseNpgsql(builder.Configuration["ItemsDbContext"]));
    builder.Services.AddScoped<IUsersService, UsersService>();
    builder.Services.AddScoped<IColorsService, ColorsService>();
    builder.Services.AddScoped<IRepository, Repository>();

}