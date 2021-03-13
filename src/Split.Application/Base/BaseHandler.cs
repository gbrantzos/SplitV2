using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Split.Application.Base
{
    public abstract class BaseHandler<TRequest, TResult> : IRequestHandler<TRequest, Result<TResult>> 
        where TRequest : IRequest<Result<TResult>>
    {
        public abstract Task<Result<TResult>> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class BaseHandler<TRequest> : BaseHandler<TRequest, bool>
        where TRequest : IRequest<Result<bool>>
    {
        
    }
}