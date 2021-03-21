using MediatR;

namespace Split.Application.Base
{
    public abstract class Request<TResult> : IRequest<Result<TResult>>
    {
        
    }

    public abstract class Request : Request<bool>
    {
        
    }
}