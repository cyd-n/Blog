namespace BackEnd.Middleware {
    public class RequestLoggingMiddleware {
        private readonly RequestDelegate _next; // Handeld HTTP-reqeusten
        private readonly ILogger<RequestLoggingMiddleware> _logger; // log reqeusten to the console

        public RequestLoggingMiddleware(RequestDelegate _next, ILogger<RequestLoggingMiddleware> _logger) {
            this._next = _next;
            this._logger = _logger;
        }

        public async Task InvokeAsync(HttpContext _context) {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            _logger.LogInformation("{method} {path}", _context.Request.Method, _context.Request.Path);

            await _next(_context); // pass to next middleware

            watch.Stop();

            _logger.LogInformation("← {method} {path} {status} ({ms}ms)", _context.Request.Method, _context.Request.Path, _context.Response.StatusCode, watch.ElapsedMilliseconds);
        }
    }
}