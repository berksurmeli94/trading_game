using TG.Core.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TG.API.Code
{
    public class ValidateModel : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var modelStateResponse = new BaseAPIResponse<bool>();

                modelStateResponse.ValidationErrors = context.ModelState.Select(x => new ValidationError
                {
                    Key = x.Key,
                    Value = x.Value.Errors.FirstOrDefault() != null ? x.Value.Errors.FirstOrDefault().ErrorMessage : ""
                }).ToList();

                if (context.ModelState.Any(x => x.Value.Errors.Any(y => y.ErrorMessage == "invalid-id")))
                {
                    context.Result = new BadRequestResult();
                }
                else
                {
                    if (modelStateResponse.ValidationErrors.Any())
                        foreach (var item in modelStateResponse.ValidationErrors)
                            if (item.Key.Any())
                            {
                                if (item.Key.StartsWith("$."))
                                    item.Key = item.Key.Substring(2);


                                if (!string.IsNullOrWhiteSpace(item.Value))
                                {
                                    if (item.Value.Contains("to type 'System.Guid'"))
                                        item.Value = "Please select an item from the list.";
                                    else if (item.Value.Contains("is required"))
                                        item.Value = "Please fill in this field.";
                                    else if (item.Value.Contains("not a valid e-mail address"))
                                        item.Value = "Please enter a valid e-mail address.";
                                    else if (item.Value.Contains("between"))
                                        item.Value = "Please enter a valid value.";
                                    else if (item.Value.Contains("System.DateTime"))
                                        item.Value = "Please enter a valid date.";
                                }
                            }

                    modelStateResponse.ValidationErrors = modelStateResponse.ValidationErrors
                        .Where(x => !string.IsNullOrWhiteSpace(x.Key)).ToList();
                    if (modelStateResponse.ValidationErrors.Any())

                        modelStateResponse.ValidationErrors.ForEach(async x =>
                        {
                            if (!string.IsNullOrWhiteSpace(x.Value))
                                x.Value = x.Value;
                        });

                    modelStateResponse.SetErrorMessage("Please check for invalid values.");
                    context.Result = new OkObjectResult(modelStateResponse);
                }
            }
            else
            {
                await next();
            }
        }
    }
}