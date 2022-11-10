public class SendFilter: ActionFilterAttribute
{
    public SendFilter()
    {
        
    }
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var modelContext = context.ActionArguments.FirstOrDefault(a => a.Key == "sendDto");
        var dto = modelContext.Value as SendDto;
        
        await next();
    }
}