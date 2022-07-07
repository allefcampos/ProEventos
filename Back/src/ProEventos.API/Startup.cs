using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ProEventos.API
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
            services.AddControllers();
            services.AddCors();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.API", Version = "v1" });
                // options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                // {
                //     Description = @"JWT Authorization header usando Bearer.
                //                 Entre com 'Bearer ' [espaço] então coloque seu token.
                //                 Exemplo: 'Bearer 12345abcdef'",
                //     Name = "Authorization",
                //     In = ParameterLocation.Header,
                //     Type = SecuritySchemeType.ApiKey,
                //     Scheme = "Bearer"
                // });

                // options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                // {
                //     {
                //         new OpenApiSecurityScheme
                //         {
                //             Reference = new OpenApiReference
                //             {
                //                 Type = ReferenceType.SecurityScheme,
                //                 Id = "Bearer"
                //             },
                //             Scheme = "oauth2",
                //             Name = "Bearer",
                //             In = ParameterLocation.Header
                //         },
                //         new List<string>()
                //     }
                // });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthentication();
            app.UseAuthorization();

            // app.UseCors(x => x.AllowAnyHeader()
            //                   .AllowAnyMethod()
            //                   .AllowAnyOrigin());

            // app.UseStaticFiles(new StaticFileOptions()
            // {
            //     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
            //     RequestPath = new PathString("/Resources")
            // });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
