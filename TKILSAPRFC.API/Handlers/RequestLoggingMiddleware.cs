using System.Text;

namespace TKILSAPRFC.API.Handlers
{
    public class RequestLoggingMiddleware
    {
        //private static ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                var request = httpContext.Request;
                if (request.Path.StartsWithSegments(new PathString("/api")))
                {
                    var requestInfo = string.Format("{0}", request.Path);

                    var requestBodyContent = await ReadRequestBody(request);

                    Console.WriteLine("Request: {0} - {1}", requestInfo, requestBodyContent);

                    string headers = string.Empty;
                    foreach (var key in request.Headers)
                        headers += key.Key + "=" + string.Join(",", key.Value) + Environment.NewLine;

                    Console.WriteLine("Headers {0}", headers);
                }
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }
    }
}
