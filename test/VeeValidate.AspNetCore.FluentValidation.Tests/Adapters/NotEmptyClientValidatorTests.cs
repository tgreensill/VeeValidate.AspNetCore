using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.Builders;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.FluentValidation.Tests.Adapters
{
    public class NotEmptyClientValidatorTests
    {
        private class TestObject
        {
            public string NotEmpty { get; set; }
        }

        [Fact]
        public void AddValidation_adds_required_rule()
        {
            // Arrange
            var property = PropertyRule.Create<TestObject, string>(x => x.NotEmpty);
            var adapter = new NotEmptyClientValidator(property, new NotEmptyValidator(null));

            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{required:true}");
        }
    }
}
