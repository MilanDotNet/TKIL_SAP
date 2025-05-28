using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            services.TryAddTransient<IGetPRByDateRangeService, GetPRByDateRangeService>();
            services.TryAddTransient<IGetPRByDateRangeRepository, GetPRByDateRangeRepository>();
            services.TryAddTransient<IGetPRNumberByDateRangeService, GetPRNumberByDateRangeService>();
            services.TryAddTransient<IGetPRNumberByDateRangeRepository, GetPRNumberByDateRangeRepository>();
            services.TryAddTransient<IGetPRByPRNOService, GetPRByPRNOService>();
            services.TryAddTransient<IGetPRByPRNORepository, GetPRByPRNORepository>();
            //services.AddScoped<RfcConnection>(provider =>
            //{
            //    var connection = new RfcConnection(
            //                        userName: "SAFAL_COMUSR",
            //                        password: @"GSj2C%w4Ykz{ERTNt]P]/3N<WlS\FD6#kT@b#4#R",
            //                        hostname: "10.66.38.115",
            //                        client: "100",
            //                        language: "EN",
            //                        systemNumber: "00",
            //                        sncMyname: "QA"
            //                    );
            //    return connection;
            //});

            string GetEnv(string key) => Environment.GetEnvironmentVariable(key) ?? throw new InvalidOperationException($"Missing env var: {key}");
            services.AddScoped<RfcConnection>(provider =>
            {
                var connection = new RfcConnection(
                    userName: GetEnv("SAP_USERNAME"),
                    password: GetEnv("SAP_PASSWORD"),
                    hostname: GetEnv("SAP_HOST"),
                    client: GetEnv("SAP_CLIENT"),
                    language: GetEnv("SAP_LANG"),
                    systemNumber: GetEnv("SAP_SYSNR"),
                    sncMyname: GetEnv("SAP_SNC_NAME")
                );
                return connection;
            });
            services.AddSingleton(new S3Credential
            {
                AccessKey = GetEnv("S3BucketAccessKey"),
                SecretKey = GetEnv("S3BucketSecretKey")
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
