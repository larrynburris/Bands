using System;

namespace Bands.Validation
{
    /// <summary>
    /// An exception for payloads that fail validation.
    /// </summary>
    /// <typeparam name="TValidatable">Type of validatable payload.</typeparam>
    public class ValidationException<TValidatable> : Exception
    {
        public TValidatable ValidatablePayload { get; set; }

        public ValidationException(TValidatable validatable) :
            base(string.Format("Failed validation on {0}: \n{1}", typeof(TValidatable), validatable.ToString()))
        {
            ValidatablePayload = validatable;
        }
    }
}
