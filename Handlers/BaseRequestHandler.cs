using MediatR;

namespace LoginProductMinimalApi.Handlers
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
                                      where TRequest : IRequest<TResponse>
    {

        protected abstract Task<TResponse> HandleInternal(TRequest request, CancellationToken cancellationToken);

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {

            return await HandleInternal(request, cancellationToken);
        }
    }
}
