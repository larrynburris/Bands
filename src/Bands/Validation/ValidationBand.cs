using System;

namespace Bands.Validation
{
    /// <summary>
    /// Helper class for wrapping some inner functionality with validation logic to prevent
    /// execution of inner functionality using invalid data.
    /// </summary>
    /// <typeparam name="TPayload">Type of validatable payload</typeparam>
    public class ValidationBand<TPayload> : Band<TPayload> where TPayload : IValidatablePayload
    {
        IValidator<TPayload> _validator;

        public ValidationBand(Action<TPayload> action, IValidator<TPayload> validator)
            : base(action)
        {
            _validator = validator;
        } 

        public ValidationBand(IBand<TPayload> innerBand, IValidator<TPayload> validator)
            : base(innerBand)
        {
            _validator = validator;
        }

        /// <summary>
        /// Validate <paramref name="payload"/> prior to executing inner bands and/or wrapped functionality
        /// </summary>
        /// <param name="payload">A validatable payload</param>
        /// <exception cref="ValidationException{TValidatable}">Thrown if validation fails</exception>
        public new void Run(TPayload payload)
        {
            if (!_validator.Validate(payload))
                throw new ValidationException<TPayload>(payload);

            InnerBand.Run(payload);
        }
    }
}
