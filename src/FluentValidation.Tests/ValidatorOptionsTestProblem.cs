
namespace FluentValidation.Tests
{
    using NUnit.Framework;
    using TestHelper;

    [TestFixture]
    public class ValidatorOptionsTestProblem
    {

        [SetUp]
        public void Setup()
        {
            // clean up just in case some other test in the suite forgot to clean this up
            ValidatorOptions.PropertyNameResolver = null;
        }

        [TearDown]
        public void TearDown()
        {
            ValidatorOptions.PropertyNameResolver = null;
        }


        [Test]
        public void ShouldHaveValidationErrorFor_passes_if_PropertyNameResolver_set_after_rule()
        {
            var validator = new TestValidator();
            validator.RuleFor(x => x.Forename).NotNull();
            ValidatorOptions.PropertyNameResolver = (type, member, expr) => "99 problems";
            validator.ShouldHaveValidationErrorFor(x => x.Forename, (string)null);
        }

        [Test]
        public void ShouldHaveValidationErrorFor_fails_if_PropertyNameResolver_set_before_rule()
        {
            var validator = new TestValidator();
            ValidatorOptions.PropertyNameResolver = (type, member, expr) => "99 problems";
            validator.RuleFor(x => x.Forename).NotNull();
            // If this was all working correctly, this test should fail
            typeof(ValidationTestException).ShouldBeThrownBy(() => validator.ShouldHaveValidationErrorFor(x => x.Forename, (string)null));

        }

    }
}