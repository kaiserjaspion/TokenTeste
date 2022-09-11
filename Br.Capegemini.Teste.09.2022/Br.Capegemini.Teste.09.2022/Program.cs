using Br.Capegemini.Teste._09._2022.Contexts;
using Br.Capegemini.Teste._09._2022.Repositories;
using Br.Capegemini.Teste._09._2022.Repositories.Interfaces;
using Br.Capegemini.Teste._09._2022.Services;
using Br.Capegemini.Teste._09._2022.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<SymmetricSecurityKey>(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["SecurityKey"])));
builder.Services.AddDbContext<Br.Capegemini.Teste._09._2022.Contexts.TokenContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("SystemsData")));
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mural", Version = "v1" });
        // generate the XML docs that'll drive the swagger docs
        var xmlFile = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    }); 

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
