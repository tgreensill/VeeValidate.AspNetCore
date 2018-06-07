using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.Builders;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.FluentValidation.Tests.Adapters
{
    public class MinimumLengthClientValidatorTests
    {
        private class TestObject
        {
            public string MinLength { get; set; }
        }

        [Fact]
        public void AddValidation_adds_min_rule()
        {
            // Arrange
            var property = PropertyRule.Create<TestObject, string>(x => x.MinLength);
            var adapter = new MinimumLengthClientValidator(property, new MinimumLengthValidator(2));

            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{min:2}");
        }
    }
}
