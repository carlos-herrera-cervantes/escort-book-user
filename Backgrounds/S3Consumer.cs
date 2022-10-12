using System.Threading;
using System.Threading.Tasks;
using EscortBookUser.Handlers;
using EscortBookUser.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EscortBookUser.Backgrounds;

public class S3Consumer : BackgroundService
{
    #region snippet_Properties

    private readonly IOperationHandler<string> _operationHandler;

    private readonly IAWSS3Service _s3Service;

    #endregion

    #region snippet_Constructors

    public S3Consumer
    (
        IOperationHandler<string> operationHandler,
        IServiceScopeFactory factory
    )
    {
        _operationHandler = operationHandler;
        _s3Service = factory
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<IAWSS3Service>();
    }

    #endregion

    #region snippet_ActionMethods

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var fn = (string key) => _s3Service.DeleteObjectAsync(key);
        _operationHandler.Subscribe("S3Consumer", async key => await fn(key));

        return Task.CompletedTask;
    }

    #endregion
}
