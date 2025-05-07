using BacklogClear.Communication.Responses;
using BacklogClear.Exception.Resources;
using BacklogClear.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BacklogClear.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BacklogClearException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownException(context);
        }
        context.ExceptionHandled = true;
    }
    
    private void HandleProjectException(ExceptionContext context)
    {
        var exception = (BacklogClearException)context.Exception;
        
        context.HttpContext.Response.StatusCode = exception.StatusCode;
        context.Result = new ObjectResult(new ResponseErrorJson(exception.GetErrorMessages()));
    }

    private void ThrowUnknownException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}