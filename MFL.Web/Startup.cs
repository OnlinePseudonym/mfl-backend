using MFL.Data;
using MFL.Data.SeedWork;
using MFL.Services.Clients;
using MFL.Services.Players;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Net.Http;
using MFL.Services.Players.Profiles;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MFL.Common.Config;

namespace MFL.Web
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
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            var container = new CookieContainer();
            services.AddSingleton(container);
            services.AddHttpClient<IMFLHttpClient, MFLHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.myfantasyleague.com/");
                client.DefaultRequestHeaders.Add("User-Agent", "DOTNETCOREMFL");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
            {
                CookieContainer = container,
                UseCookies = true
            });

            services.AddAutoMapper(typeof(PlayersProfile));

            services.AddScoped<PlayersService>();

            services.AddControllers();
            services.AddDbContext<MFLContext>(opt => opt.UseInMemoryDatabase("mfl"));



            var _appSettings = Configuration
                .GetSection(AppSettingsOptions.AppSettings)
                .Get<AppSettingsOptions>();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MFL.Web", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MFL.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
