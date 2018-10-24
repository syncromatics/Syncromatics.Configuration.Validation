using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Syncromatics.Configuration.Validation
{
    // adapted from https://social.msdn.microsoft.com/Forums/vstudio/en-US/4cbf4482-f4f5-489d-bbf1-65810f307d16/data-annontation-validation-with-nested-object?forum=csharpgeneral
    public class ValidateNestedObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(value, null, null);

            Validator.TryValidateObject(value, context, results, true);

            if (results.Count == 0) return ValidationResult.Success;

            var errorMessage = $"Validation for {validationContext.DisplayName} failed:";
            var compositeResults = new CompositeValidationResult(errorMessage);
            results.ForEach(compositeResults.AddResult);

            return compositeResults;

        }
    }
}