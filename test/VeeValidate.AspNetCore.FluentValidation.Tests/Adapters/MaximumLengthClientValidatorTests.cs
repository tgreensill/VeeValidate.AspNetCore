using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.Builders;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.FluentValidation.Tests.Adapters
{
    public class MaximumLengthClientValidatorTests
    {
        private class TestObject
        {
            public string MaxLength { get; set; }
        }

        [Fact]
        public void AddValidation_adds_max_rule()
        {
            // Arrange
            var property = PropertyRule.Create<TestObject, string>(x => x.MaxLength);
            var adapter = new MaximumLengthClientValidator(property, new MaximumLengthValidator(10));

            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{max:10}");
        }
    }
}
