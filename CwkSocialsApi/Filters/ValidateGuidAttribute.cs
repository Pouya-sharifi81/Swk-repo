﻿using CwkSocialsApi.Contract.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CwkSocialsApi.Filters
{
    public class ValidateGuidAttribute : ActionFilterAttribute
    {
        private readonly List<string> _keys;
        public ValidateGuidAttribute(params string[] keys)
        {
            _keys = keys.ToList();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool hasError = false;
            var apiError = new ErrorResponse();
            _keys.ForEach(k =>
            {
                if (!context.ActionArguments.TryGetValue(k, out var value)) return;
                if (!Guid.TryParse(value?.ToString(), out var guid))
                {
                    hasError = true;
                    apiError.Errors.Add($"The identifier for {k} is not a correct GUID format");
                }
            });

            if (hasError)
            {
                apiError.StatusCode = 400;
                apiError.Statusphrase = "Bad request";
                apiError.TimeStamp = DateTime.Now;
                context.Result = new ObjectResult(apiError);
            }
        }
    }
}
