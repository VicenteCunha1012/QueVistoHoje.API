using Microsoft.OpenApi.Models;
using QueVistoHoje.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using QueVistoHoje.API.Repositories.Categorias;
using QueVistoHoje.API.Repositories.Produtos;
using QueVistoHoje.API.Repositories.Empresas;
using QueVistoHoje.API.Repositories.Encomendas;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// JWT Authentication Configuration
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options => {
//        options.TokenValidationParameters = new TokenValidationParameters {
//            ValidAudience = builder.Configuration["JWT:Audience"],
//            ValidIssuer = builder.Configuration["JWT:Issuer"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            // Configure the role claim type to match the one in your token
//            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
//        };

//        // Logging events
//        options.Events = new JwtBearerEvents {
//            OnAuthenticationFailed = context => {
//                Console.WriteLine("Authentication failed: " + context.Exception.Message);
//                return Task.CompletedTask;
//            },
//            OnTokenValidated = context => {
//                Console.WriteLine("Token validated successfully.");
//                return Task.CompletedTask;
//            },
//            OnChallenge = context => {
//                // Log the challenge event to understand why access is denied
//                Console.WriteLine("Authorization failed. Reason: " + context.Error);
//                return Task.CompletedTask;
//            }
//        };
//    });

// Logging Configuration
builder.Services.AddLogging(logging => {
    logging.ClearProviders();
    logging.AddConsole(); // Enable console logging
    logging.SetMinimumLevel(LogLevel.Debug); // Set to debug for verbose logs
});

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    // Add security definition for Basic Auth
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        Description = "Input your email and password in the format: `email:password`"
    });

    // Add a requirement to use the security scheme
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "basic"
                    }
                },
                new string[] { }
            }
        });
});

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => {
        builder.AllowAnyOrigin() // Or restrict it to the tunnel's domain
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// JSON Serialization Configuration
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    })
    .AddNewtonsoftJson(options => {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

// Database Configuration
var connection = builder.Configuration.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connection)
           .EnableSensitiveDataLogging());

// Identity Configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Repository Configuration
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IEncomendaRepository, EncomendaRepository>();

// Add API Endpoints and Identity routes
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "QueVistoHoje - Cliente");
    });
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.Run();
