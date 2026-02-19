using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginProductMinimalApi.Handlers
{
    public class LoggingCQRSPipelineBehaviour<T, K> : IPipelineBehavior<T, K>
        where T : IRequest<K>
    {
        private readonly ILogger<LoggingCQRSPipelineBehaviour<T, K>> _logger;

        public LoggingCQRSPipelineBehaviour(ILogger<LoggingCQRSPipelineBehaviour<T, K>> logger)
        {
            _logger = logger;
        }

        public async Task<K> Handle(T request, RequestHandlerDelegate<K> next, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Handling {RequestName} with data: {@RequestData}", typeof(T).Name, request);
            var response = await next(cancellationToken);
            _logger.LogInformation("Handled {RequestName} with response: {@ResponseData}", typeof(T).Name, response);
            return response;
        }


    }
}
