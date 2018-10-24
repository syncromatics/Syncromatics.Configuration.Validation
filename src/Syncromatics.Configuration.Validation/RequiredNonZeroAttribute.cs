using System;
using System.ComponentModel.DataAnnotations;

namespace Syncromatics.Configuration.Validation
{
    public class RequiredNonZeroAttribute : ValidationAttribute
    {
        public RequiredNonZeroAttribute()
            : base(() => "The {0} field is required and must be non-zero.")
        { }

        public override bool IsValid(object value)
        {
            switch (value)
            {
                case TimeSpan unboxed:
                    return unboxed != TimeSpan.Zero;
                default:
                    return !value.Equals(0);
            }
        }
    }
}