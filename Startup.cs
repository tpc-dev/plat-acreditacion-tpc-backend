using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PlatAcreditacionTPCBackend.Converters;
using PlatAcreditacionTPCBackend.Models;
using PlatAcreditacionTPCBackend.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlatAcreditacionTPCBackend;

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
        #region CORS
        //services.AddCors( c =>
        //{
        //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
        //});
        services.AddCors();
        #endregion


        #region EMAIL

        services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
        services.AddTransient<IMailService, Servicios.MailService>();

        #endregion

        #region SHAREPOINTSERVICE

        services.AddSingleton<ISharePointService, Servicios.SharePointService>();

        #endregion

        services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
        );

        //services.AddControllers().AddJsonOptions(options =>
        //    options.JsonSerializerOptions.Converters.Add(new TPCDateConverter())
        //);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaulConnection"))
        );

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["llavejwt"])),
                ClockSkew = TimeSpan.Zero
            });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API-TPC", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id ="Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        services.AddAutoMapper(typeof(Startup));

        services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        #region CORS
        app.UseCors(options =>
        {
            options.WithOrigins("http://localhost:4200");
            options.AllowAnyMethod();
            options.AllowAnyHeader();
        });
        #endregion

        //if (env.IsDevelopment())
        //{
            //app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API-TPC v1"));
        //}

        app.UseHttpsRedirection();

        app.UseRouting();

        //   app.UseCors();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

