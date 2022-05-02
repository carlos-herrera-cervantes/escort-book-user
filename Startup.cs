using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EscortBookUser.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EscortBookUser.Repositories;
using EscortBookUser.Services;

namespace EscortBookUser
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<EscortBookUserContext>(options
                => options.UseNpgsql(Configuration["ConnectionStrings:Default"]));
            services.AddTransient<IAvatarRepository, AvatarRepository>();
            services.AddTransient<IConnectionLogRepository, ConnectionLogRepository>();
            services.AddTransient<IDictumRepository, DictumRepository>();
            services.AddTransient<IRequestLogRepository, RequestLogRepository>();
            services.AddTransient<IStatusCategoryRepository, StatusCategoryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAWSS3Service, AWSS3Service>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
