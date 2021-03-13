using MediatR;

namespace Split.Application.Base
{
    public abstract class BaseRequest<TResult> : IRequest<Result<TResult>>
    {
        
    }

    public abstract class BaseRequest : BaseRequest<bool>
    {
        
    }
}