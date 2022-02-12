using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Data.Repository.Repositories;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Business.Service;
using JaVisitei.Brasil.Business.Service.Interfaces;
using System.Text;
using System;

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
            var connetionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            services.AddDbContext<DbJaVisiteiBrasilContext>(o => o.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString)));

            services.AddControllers()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    o.JsonSerializerOptions.MaxDepth = 0;
                });

            services.AddScoped<IPaisRepository, PaisRepository>();
            services.AddScoped<IPaisService, PaisService>();

            services.AddScoped<IEstadoRepository, EstadoRepository>();
            services.AddScoped<IEstadoService, EstadoService>();

            services.AddScoped<IMesorregiaoRepository, MesorregiaoRepository>();
            services.AddScoped<IMesorregiaoService, MesorregiaoService>();

            services.AddScoped<IMicrorregiaoRepository, MicrorregiaoRepository>();
            services.AddScoped<IMicrorregiaoService, MicrorregiaoService>();

            services.AddScoped<IArquipelagoRepository, ArquipelagoRepository>();
            services.AddScoped<IArquipelagoService, ArquipelagoService>();

            services.AddScoped<IMunicipioRepository, MunicipioRepository>();
            services.AddScoped<IMunicipioService, MunicipioService>();

            services.AddScoped<IIlhaRepository, IlhaRepository>();
            services.AddScoped<IIlhaService, IlhaService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            services.AddScoped<ITipoRegiaoRepository, TipoRegiaoRepository>();
            services.AddScoped<ITipoRegiaoService, TipoRegiaoService>();

            services.AddScoped<IVisitaRepository, VisitaRepository>();
            services.AddScoped<IVisitaService, VisitaService>();

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo { Title = "API Já Visitei Mapa do Brasil", Version = "1" });
            });

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'V";
                o.SubstituteApiVersionInUrl = true;
            });

            //var config = new MapperConfiguration(c =>
            //    {
            //        c.CreateMap<LoginRequest, Model.Models.UsuarioEntity>();
            //    });
            //IMapper mapper = config.CreateMapper();
            //services.AddSingleton(mapper);

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"))),
                    ClockSkew = TimeSpan.FromMinutes(15),
                    ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                    ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
                };
            });//.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o => Configuration.Bind("CookieSettings", o));

            services.AddAuthorizationCore(x => x.AddPolicy(JwtBearerDefaults.AuthenticationScheme, new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build()));

            services.AddMvc(o => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1.0");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(o => o.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(o => {
                o.MapControllers();
            });
        }
    }
}
