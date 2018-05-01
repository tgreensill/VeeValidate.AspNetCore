using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class NumericClientValidatorTests
    {
        [Fact]
        public void AddValidation_adds_validation_rule()
        {
            // Arrange
            var validator = new NumericClientValidator();
            var context = new ClientModelValidationContextBuilder()
                .WithModelType(typeof(int))
                .Build();

            // Act
            validator.AddValidation(context);

            // Assert            
            context.Attributes.ShouldContainKey("data-vv-as");
            context.Attributes.ShouldContainKey("v-validate");
            context.Attributes["v-validate"].ShouldBe("{numeric:true}");
        }
    }
}
