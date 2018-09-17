using ENIDABackendAPI.Repository;
using ENIDABackendAPI.Service;
using ENIDABackendAPI.db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ENIDABackend
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ENIDADbContext>(options => 
                options.UseSqlite(
                    GetDbConnection()
                )
            );
            services.AddScoped<InformationRepository, DbContextInformationRepository>();
            services.AddScoped<InformationService>();
        }
        
        private string GetDbConnection()
        {
            return Configuration.GetConnectionString("DefaultConnection");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            loggerFactory.AddFile(Configuration.GetSection("Logging"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
