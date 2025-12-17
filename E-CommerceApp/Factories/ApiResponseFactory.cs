using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_CommerceApp.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState
                .Where(m => m.Value.Errors.Any())
                .Select(m => new ValidationError()
                {
                    FieldName = m.Key,
                    Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                });

            var responseObj = new ValidationErrorToReturn()
            {
                ValidationErrors = errors
            };

            return new BadRequestObjectResult(responseObj);
        }
    }
}
