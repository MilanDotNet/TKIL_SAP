using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using TKILSAPRFC.API.Handlers;
using TKILSAPRFC.Core.Context;
using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Model.Mapper;
using TKILSAPRFC.Service.Services;

namespace TKILSAPRFC.API
{
    public class Startup
    {
        public static readonly ILoggerFactory _efLoggerFactory = LoggerFactory.Create(builder => { builder.AddDebug(); });
        public IConfiguration Configuration { get; }
        public static IConfiguration? StaticConfiguration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfiguration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options
                                                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
#if DEBUG
                                                    .UseLoggerFactory(_efLoggerFactory)
#endif
                                                    , ServiceLifetime.Transient);

            services.AddMemoryCache();
#pragma warning disable ASP5001 // Type or member is obsolete
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().AddRequirements(new AuthorizationGrantTypeRequirement()).Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
#pragma warning restore ASP5001 // Type or member is obsolete

            
            services.ConfigureAppSettings(Configuration);
            services.AddHttpClient();
            services.AddHttpClient<ConnectionService>(client =>
            {
                client.BaseAddress = new Uri("http://host.docker.internal:5000/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddTransient<MyHttpClientService>();
            services.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
            });

            services.ConfigureSwagger();

            services.ConfigureCors();

            services.ConfigureHttpContextAndServices(Configuration);

            services.ConfigureAuthentication();

            services.ConfigureAuthorization();
            services.AddHttpClient();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                c.DefaultModelsExpandDepth(-1);
            });

            app.UseCors("AllowAll");

            app.UseMiddleware<RequestLoggingMiddleware>();

            app.ConfigureExceptionHandler();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = "";
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    }
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
