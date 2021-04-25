using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Split.Application.Base.Pipeline;

namespace Split.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var asm = typeof(DependencyInjection).Assembly;
            services
                .AddMediatR(asm)
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(MetricsBehavior<,>))
                .AddValidatorsFromAssembly(asm);
            
            return services;
        }
    }
}