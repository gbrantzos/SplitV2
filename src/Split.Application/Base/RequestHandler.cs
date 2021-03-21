using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Split.Application.Base
{
    public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, Result<TResult>>
        where TRequest : IRequest<Result<TResult>>
    {
        public async Task<Result<TResult>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleInternal(request, cancellationToken);
            }
            catch (Exception e)
            {
                return Result.Failure($"Unhandled exception in request {typeof(TRequest).Name}", e);
            }
        }

        protected abstract Task<Result<TResult>> HandleInternal(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class RequestHandler<TRequest> : RequestHandler<TRequest, bool>
        where TRequest : IRequest<Result<bool>>
    {
    }
}