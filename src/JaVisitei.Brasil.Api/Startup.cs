using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Api.Configuration;
using System.Text;
using System;
using StackExchange.Redis;
using Asp.Versioning;

namespace JaVisitei.Brasil.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNETION_BASE");
            services.AddDbContext<DbJaVisiteiBrasilContext>(o => o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddCors(o =>
            {
                o.AddPolicy("MapPolicy",
                p => {
                    p
                    .WithOrigins(Environment.GetEnvironmentVariable("ORIGINS").Split(","))
                    .WithMethods("GET","PUT","POST","DELETE")
                    .AllowAnyHeader();
                });
            });

            services.AddControllers()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    o.JsonSerializerOptions.MaxDepth = 0;
                    o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            services.AddServiceDependency();

            services.AddStackExchangeRedisCache(o => {
                o.InstanceName = "RedisInstance";
                o.ConfigurationOptions = new ConfigurationOptions
                {
                    AbortOnConnectFail = false,
                    EndPoints = { Environment.GetEnvironmentVariable("REDIS_ENDPOINT") }
                };
            });

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo { Title = "API Já Visitei Mapa do Brasil", Version = "1" });
            });

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                o.RequireHttpsMetadata = false;
                o.IncludeErrorDetails = true;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"))),
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                    ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                    RequireExpirationTime = true
                };
            });

            services.AddAuthorizationCore(x => x
                .AddPolicy(JwtBearerDefaults.AuthenticationScheme, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build()));

            services.AddMvc(o =>
            {
                o.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
            });

            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MapPolicy");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1.0");
            });

            app.UseEndpoints(o => {
                o.MapControllers();
            });
        }
    }
}