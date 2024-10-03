using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace NLayerArchitectureV2.Services.ExceptionHandlers
{
    public class CriticalExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is CriticalException)
            {
                Console.WriteLine("Sms gönderildi");
            }
            //false ise başka handlera gider true ise burda yönetir vs..
            return ValueTask.FromResult(false);
        }
    }
}
