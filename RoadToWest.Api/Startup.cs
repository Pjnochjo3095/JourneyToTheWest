using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoadToWest.Data.Domains;
using RoadToWest.Data.Models;
using RoadToWest.Data.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RoadToWest.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<RoadToWestContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("RoadToWestContext"));
                options.UseLazyLoadingProxies();
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerOptions =>
            {
                JwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "RoadToWest_Issue",
                    ValidAudience = "RoadToWest_Issue",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes("PRIVATE_RoadToWest_KEY")),
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "My API Road To The West",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                  "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<DbContext, RoadToWestContext>()
                .AddScoped<ISceneRepository, SceneRepository>()
                .AddScoped<IToolRepository, ToolRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IActorRepository,ActorRepository>()
                .AddScoped<IUserRoleRepository, UserRoleRepository>()
                .AddScoped<IOrderToolRepository, OrderToolRepository>()
                .AddScoped<ISceneCharacterRepository, SceneCharacterRepository>()
                .AddScoped<OrderToolDomain>()
                .AddScoped<ActorDomain>()
                .AddScoped<UserDomain>()
                .AddScoped<ToolDomain>()
                .AddScoped<SceneDomain>()
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
