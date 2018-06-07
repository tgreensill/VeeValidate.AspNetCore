using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.Builders;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.FluentValidation.Tests.Adapters
{
    public class RegularExpressionClientValidatorTests
    {
        private class TestObject
        {
            public string Password { get; set; }
        }

        [Theory]
        [InlineData("/[a-z]/")]
        [InlineData("[a-z]")]
        public void AddValidation_adds_regex_rule(string value)
        {
            // Arrange
            var property = PropertyRule.Create<TestObject, string>(x => x.Password);
            var adapter = new RegularExpressionClientValidator(property, new RegularExpressionValidator(value));

            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{regex:/[a-z]/}");
        }
    }
}
