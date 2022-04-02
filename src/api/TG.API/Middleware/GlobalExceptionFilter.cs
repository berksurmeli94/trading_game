using TG.Core.Exceptions;
using TG.Core.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace TG.API.Code
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                var response = new BaseAPIResponse<bool>();
                response.SetErrorMessage(context.Exception.Message);
                context.Result = new BadRequestResult();
            }
            if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new UnauthorizedResult();
            }
            else if (context.Exception is ValidationException)
            {
                var response = new BaseAPIResponse<bool>();
                response.SetErrorMessage(context.Exception.Message);
                context.Result = new OkObjectResult(response);
            }
        }
    }
}
