using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Exceptions
{
    public static class ValidateData
    {
        public static void Validate(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                List<AppError> errors = new();
                foreach (var item in modelState)
                {
                    var error = item.Value?.Errors?.FirstOrDefault();

                    if (error != null)
                        errors.Add(new AppError
                        {
                            Message = string.IsNullOrEmpty(error.ErrorMessage) ? error?.Exception?.Message : error?.ErrorMessage,
                            Field = item.Key
                        });
                }
                throw new AppValidationException(errors);
            }
        }
    }
}

namespace Utility
{
    public class AppError
    {
        public string Field { get; set; }


        public string Message { get; set; }
    }
}
