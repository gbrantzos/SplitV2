using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Split.Common.Utilities;

namespace Split.Application.Base.Pipeline
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) 
            => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogDebug("[{RequestName:l}] Executing => {Request}\r\n{RequestJson}", 
                requestName,
                request,
                request.AsJson());

            try
            {
                var sw = new Stopwatch();
                sw.Start();

                var response = await next();
                sw.Stop();
            
                if (response is Result responseResult)
                {
                    if (responseResult.Failed)
                    {
                        var formattedMessage = AddIndent(responseResult.Message);
                        _logger.LogError(
                            $"[{requestName}] Request result has errors => {request} ({sw.ElapsedMilliseconds}ms)\r\n{formattedMessage}");
                    }
                    else
                    {
                        _logger.LogInformation(
                            $"[{requestName}] Executed successfully => {request} ({sw.ElapsedMilliseconds}ms)");
                    }
                }
                else
                {
                    // No details to display
                    _logger.LogInformation($"Request executed, {requestName}: {request} [{sw.ElapsedMilliseconds}ms]");
                }
                return response;
            }
            catch (Exception x)
            {
                var all     = x.GetAllMessages();
                var message = String.Join(Environment.NewLine, all);
                _logger.LogError(x, "Request execution failed: {Message}\r\n{@Request}", message, request);

                throw;
            }
        }
        
        private static string AddIndent(string message)
        {
            if (String.IsNullOrEmpty(message))
                return message;

            var lines = message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var line in lines)
                sb.AppendLine($"    {line}");

            return sb.ToString().Trim('\r', '\n');
        }
    }
}