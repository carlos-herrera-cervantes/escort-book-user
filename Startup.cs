using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EscortBookUser.Contexts;
using Microsoft.EntityFrameworkCore;
using EscortBookUser.Repositories;
using EscortBookUser.Services;
using EscortBookUser.Backgrounds;
using EscortBookUser.Handlers;

namespace EscortBookUser;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson();
        services.AddDbContext<EscortBookUserContext>(options
            => options.UseNpgsql(Environment.GetEnvironmentVariable("PG_DB_CONNECTION")));
        services.AddTransient<IAvatarRepository, AvatarRepository>();
        services.AddTransient<IConnectionLogRepository, ConnectionLogRepository>();
        services.AddTransient<IDictumRepository, DictumRepository>();
        services.AddTransient<IRequestLogRepository, RequestLogRepository>();
        services.AddTransient<IStatusCategoryRepository, StatusCategoryRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IAWSS3Service, AWSS3Service>();
        services.AddSingleton(typeof(IOperationHandler<>), typeof(OperationHandler<>));
        services.AddHostedService<S3Consumer>();
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
