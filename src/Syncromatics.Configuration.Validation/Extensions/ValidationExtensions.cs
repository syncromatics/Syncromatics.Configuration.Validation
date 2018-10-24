using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Syncromatics.Configuration.Validation.Extensions
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Examines all ValidationAttributes present on the supplied object's class properties and throws an exception
        /// if any is invalid, with a human-readable message of what is missing.
        /// TODO: Should support arbitrary levels of nesting.
        /// </summary>
        /// <param name="obj">The object to validate</param>
        /// <exception cref="Exception">Thrown if any properties fail validation</exception>
        public static void EnsureIsValid(this object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            var success = Validator.TryValidateObject(obj, context, results, true);
            if (success) return;

            var errorMessage = new StringBuilder();
            errorMessage.AppendLine("Provided settings are not valid:");
            foreach (var result in results)
            {
                switch (result)
                {
                    case CompositeValidationResult compositeResult:
                        errorMessage.AppendLine(compositeResult.ErrorMessage);
                        foreach (var individualResult in compositeResult.Results)
                        {
                            errorMessage.AppendLine($" * {individualResult.ErrorMessage}");
                        }
                        break;
                    default:
                        errorMessage.AppendLine(result.ErrorMessage);
                        break;
                }
            }
            throw new Exception(errorMessage.ToString());
        }
    }
}