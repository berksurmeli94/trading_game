using TG.API.Code;
using TG.Domain.Context;
using TG.Domain.Repository;
using TG.Helpers.ServiceExtensions;
using TG.Services;
using TG.Services.Concrete;
using TG.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;
using TG.Common.Services.CacheService.Distributed;
using TG.Common.Services.CacheService.Memory;
using TG.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace TG.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(configure => { configure.Filters.Add(typeof(GlobalExceptionFilter)); });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<RedisServer>();
            services.AddSingleton<IDistributedCacheService, RedisCacheService>();
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();

            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>))
                    .AddScoped<IUnitOfWork, UnitOfWork>()
                    .AddScoped<ICurrentUserService, CurrentUserService>()
                    .AddScoped<IUserService, UserService>()
                    .AddScoped<IShareService, ShareService>()
                    .AddScoped<ISharePriceService, SharePriceService>()
                    .AddScoped<IPortfolioService, PortfolioService>()
                    .AddScoped<ITransactionService, TransactionService>();

            services.Configure<ApiBehaviorOptions>(opts => opts.SuppressModelStateInvalidFilter = true);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateActor = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Issuer"],
                            IssuerSigningKey =
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                        };
                    });

            services.AddMvc(option => { option.EnableEndpointRouting = false; }).AddJsonOptions(
               options =>
                  options.JsonSerializerOptions.PropertyNamingPolicy = null
            );

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetSection("ConnectionStrings").GetSection("Postgres").Value));
            
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.DescribeAllParametersInCamelCase();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TG.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "TG.API",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TG.API v1"));
            }
            
            app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true)
              .AllowCredentials());

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseResponseCaching();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "TG.API v1"); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
