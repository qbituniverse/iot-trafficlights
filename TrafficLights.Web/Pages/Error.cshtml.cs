using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrafficLights.Web.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string? ExceptionMessage { get; set; }

        public string? ExceptionSource { get; set; }

        public string? ExceptionStackTrace { get; set; }

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            ExceptionMessage = exception.Error.Message;
            ExceptionSource = exception.Error.Source;
            ExceptionStackTrace = exception.Error.StackTrace;

            _logger.LogError(@"Error: RequestId {RequestId} Message {Message} Source {Source} StackTrace {StackTrace}",
                RequestId, ExceptionMessage, ExceptionSource, ExceptionStackTrace);
        }
    }
}