using Contracts;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Repository;
using System.Configuration;

namespace SQL_SanitizeWords_WebApi
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public void ConfigureServices(IServiceCollection services)
        {
            // var builder = WebApplication.CreateBuilder(args);

            
            services.AddScoped<IWord, WordRepository>();
            

            services.AddDbContext<WordContext>
            (options => options.UseSqlServer(Configuration.GetConnectionString("WordsCS")));
            services.AddScoped<DbContext, WordContext>();




            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SQl Sanitize", Version = "v1" });
                

            });

        }
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
               
                //  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SQL_SanitizeWords_WebApi v1"));
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SQL_SanitizeWords_WebApi v1");
                    // c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
                 
                });


            }

            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.MapControllers();

        }


        //// Add services to the container.
        //builder.Services.AddControllers();
        //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();

        //var app = builder.Build();

        //// Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}

        //app.UseHttpsRedirection();

        //app.UseAuthorization();

        //app.MapControllers();

        //app.Run();
    }

}
