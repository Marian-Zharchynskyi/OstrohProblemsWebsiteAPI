using System.Text;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Services.HashPasswordService;
using Application.Services.ImageService;
using Application.Services.TokenService;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;

namespace Infrastructure.Persistence;

public static class ConfigurePersistence
{
    public static void AddPersistence(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var dataSourceBuild =
            new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
        dataSourceBuild.EnableDynamicJson();
        var dataSource = dataSourceBuild.Build();

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(
                    dataSource,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .UseSnakeCaseNamingConvention()
                .ConfigureWarnings(w => w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning)));

        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddRepositories();
        services.AddJwtTokenAuth(builder);
        services.AddSwaggerAuth();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<StatusRepository>();
        services.AddScoped<IProblemStatusRepository>(provider =>
            provider.GetRequiredService<StatusRepository>());
        services.AddScoped<IProblemStatusQueries>(provider => provider.GetRequiredService<StatusRepository>());

        services.AddScoped<ProblemRepository>();
        services.AddScoped<IProblemRepository>(provider => provider.GetRequiredService<ProblemRepository>());
        services.AddScoped<IProblemQueries>(provider => provider.GetRequiredService<ProblemRepository>());

        services.AddScoped<CategoryRepository>();
        services.AddScoped<IProblemCategoryRepository>(provider =>
            provider.GetRequiredService<CategoryRepository>());
        services.AddScoped<IProblemCategoryQueries>(
            provider => provider.GetRequiredService<CategoryRepository>());

        services.AddScoped<CommentRepository>();
        services.AddScoped<ICommentRepository>(provider => provider.GetRequiredService<CommentRepository>());
        services.AddScoped<ICommentQueries>(provider => provider.GetRequiredService<CommentRepository>());
        
        services.AddScoped<RoleRepository>();
        services.AddScoped<IRoleQueries>(provider => provider.GetRequiredService<RoleRepository>());

        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IHashPasswordService, HashPasswordService>();
        services.AddScoped<IImageService, ImageService>();

        services.AddScoped<RefreshTokenRepository>();
        services.AddScoped<IRefreshTokenRepository>(provider => provider.GetRequiredService<RefreshTokenRepository>());
    }

     private static void AddJwtTokenAuth(this IServiceCollection services, WebApplicationBuilder builder)
     {
         services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             })
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     RequireExpirationTime = true,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero,
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey =
                         new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder!.Configuration["AuthSettings:key"]!)),
                     ValidIssuer = builder.Configuration["AuthSettings:issuer"],
                     ValidAudience = builder.Configuration["AuthSettings:audience"]
                 };
             });
     }

     private static void AddSwaggerAuth(this IServiceCollection services)
     {
         services.AddSwaggerGen(options =>
         {
             options.SwaggerDoc("v1", new OpenApiInfo { Title = "NPR321", Version = "v1" });

             options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
             {
                 Name = "Authorization",
                 Type = SecuritySchemeType.Http,
                 Scheme = "Bearer",
                 BearerFormat = "JWT",
                 In = ParameterLocation.Header,
                 Description = "Введіть JWT токен"
             });

             options.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                 {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     },
                     Array.Empty<string>()
                 }
             });
         });
     }
}