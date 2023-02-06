using Items.API.Services.UsersServices;
using Items.Data;
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
app.UseAuthorization();
app.MapControllers();

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
    var key = builder.Configuration["JwtTokenSecret"];
    var bytes = Encoding.ASCII.GetBytes(key);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(bytes),
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
}