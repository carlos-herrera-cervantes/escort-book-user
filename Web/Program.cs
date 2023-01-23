using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EscortBookUser.Web.Contexts;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Services;
using EscortBookUser.Web.Backgrounds;
using EscortBookUser.Web.Handlers;
using EscortBookUser.Web.Extensions;
using EscortBookUser.Web.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<EscortBookUserContext>(options
    => options.UseNpgsql(Postgres.ConnectionString));
builder.Services.AddS3Client();
builder.Services.AddTransient<IAvatarRepository, AvatarRepository>();
builder.Services.AddTransient<IConnectionLogRepository, ConnectionLogRepository>();
builder.Services.AddTransient<IDictumRepository, DictumRepository>();
builder.Services.AddTransient<IRequestLogRepository, RequestLogRepository>();
builder.Services.AddTransient<IStatusCategoryRepository, StatusCategoryRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAwsS3Service, AwsS3Service>();
builder.Services.AddSingleton(typeof(IOperationHandler<>), typeof(OperationHandler<>));
builder.Services.AddHostedService<S3Consumer>();

var app = builder.Build();

app.UseHttpLogging();
app.UseRouting();
app.MapControllers();
app.Run();
