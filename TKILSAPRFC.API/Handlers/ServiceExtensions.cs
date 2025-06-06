﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TKILSAPRFC.Core.Helpers.Interface;
using TKILSAPRFC.Core.Helpers;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Service.Services.Interface;
using TKILSAPRFC.Service.Services;
using TKILSAPRFC.Infrastructure.Repository;
using TKILSAPRFC.Model.Mapper;
using TKILSAPRFC.Model.ViewModels;
using NwRfcNet;

namespace TKILSAPRFC.API.Handlers
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                   builder => builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
            });
        }

        public static void ConfigureHttpContextAndServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.TryAddTransient<ILoginRepository, LoginRepository>();
            services.TryAddTransient<ILoginService, LoginService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.TryAddTransient<IAutomappers, AutoMappers>();
            services.TryAddTransient<IConnectionService, ConnectionService>();
            services.TryAddTransient<IConnectionRepository, ConnectionRepository>();
            services.TryAddTransient<IMasterService, MasterService>();
            services.TryAddTransient<IMasterRepository, MasterRepository>();
            services.AddScoped<RfcConnection>(provider =>
            {
                var config = ConnectionRepository.GetParameters();

                var connection = new RfcConnection(
                                    userName: config.User,
                                    password: config.Password,
                                    hostname: config.AppServerHost,
                                    client: config.Client,
                                    language: config.Language,
                                    systemNumber: config.SystemNumber,
                                    sapRouter: config.SapRouter,
                                    sncQop: config.SncQop,
                                    sncMyname: config.Name
                                );
                return connection;
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, Path.GetFileName($"{Assembly.GetEntryAssembly().GetName().Name}.xml")));

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                } });
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = AppSettings.Current.JwtConfiguration.Audience,
                    ValidIssuer = AppSettings.Current.JwtConfiguration.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Current.JwtConfiguration.SigningKey)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddSingleton<IAuthorizationHandler, RefreshTokenGrantTypeHandler>();
        }
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration config)
        {
            AppSettings? appSettings = config.GetSection(nameof(AppSettings)).Get<AppSettings>();
            ConnectionStrings? connectionStrings = config.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        }
    }
}
