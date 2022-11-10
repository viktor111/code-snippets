public class RequestSizeLimitChecker : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var requestSize = context.HttpContext.Request.ContentLength;

        if(requestSize.HasValue)
        {
            if(requestSize.Value > FileSizeCalculator.MegaBytes(25))
            {
                var statusResult = new StatusCodeResult(StatusCodes.Status413PayloadTooLarge);
                context.Result = statusResult;
                return;
            }
        }

        await next();
    }
}