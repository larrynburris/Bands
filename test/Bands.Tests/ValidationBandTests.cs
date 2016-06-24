using Bands.Validation;
using NUnit.Framework;

namespace Bands.Tests
{
    [TestFixture]
    class ValidationBandTests
    {
        public class ValidatablePayload : IValidatablePayload
        {
            public int Counter { get; set; }

            public override string ToString()
            {
                return string.Format("Payload type: {0}\n Counter: {1}", this.GetType(), Counter);
            }
        }

        public class PayloadValidator : IValidator<ValidatablePayload>
        {
            public bool Validate(ValidatablePayload validatable)
            {
                return validatable.Counter == 0;
            }
        }

        PayloadValidator Validator;
        ValidatablePayload Payload;

        [TestFixtureSetUp]
        public void TestFixture()
        {
            Validator = new PayloadValidator();
        }

        [Test]
        public void TestValidatorBand_IsValid()
        {
            Payload = new ValidatablePayload { Counter = 0 };
            var band = new ValidationBand<ValidatablePayload>(IncrementCounter, Validator);
            Assert.DoesNotThrow(() => band.Run(Payload));
            Assert.IsTrue(Payload.Counter == 1); //Increment counter ran because validation passed
        }

        [Test]
        public void TestValidatorBand_IsInvalid()
        {
            Payload = new ValidatablePayload { Counter = 1 };
            var band = new ValidationBand<ValidatablePayload>(IncrementCounter, Validator);
            Assert.Throws<ValidationException<ValidatablePayload>>(() => band.Run(Payload));
            Assert.IsTrue(Payload.Counter == 1); //Increment counter not ran because validation failed
        }

        public void IncrementCounter(ValidatablePayload payload)
        {
            payload.Counter++;
        }
    }
}
