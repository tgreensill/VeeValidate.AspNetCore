using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
using VeeValidate.AspNetCore.FluentValidation.Tests.Builders;
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
