using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Middleware {
    public class GlobalExceptionHandler : IExceptionHandler {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) {
            this._logger = _logger;
        }

        public async ValueTask<bool> TryHandleAsync( HttpContext _ctx, Exception _exception, CancellationToken _token) {
            _logger.LogError(_exception, "Unhandled exception: {message}", _exception.Message);

            var problem = new ProblemDetails {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Something went wrong",
                Detail = _exception.Message
            };

            _ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await _ctx.Response.WriteAsJsonAsync(problem, _token);

            return true; // true = exception is handled, stop propagating
        }
    }
}