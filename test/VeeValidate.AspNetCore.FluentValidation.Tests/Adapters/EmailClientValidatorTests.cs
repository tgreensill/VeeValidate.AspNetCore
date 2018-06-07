using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.Builders;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.FluentValidation.Tests.Adapters
{
    public class EmailClientValidatorTests
    {
        private class TestObject
        {
            public string EmailAddress { get; set; }
        }

        [Fact]
        public void AddValidation_adds_email_rule()
        {
            // Arrange
            var property = PropertyRule.Create<TestObject, string>(x => x.EmailAddress);
            var adapter = new EmailClientValidator(property, new EmailValidator());

            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{email:true}");
        }
    }
}
