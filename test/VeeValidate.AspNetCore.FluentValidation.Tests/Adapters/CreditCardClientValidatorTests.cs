using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
using VeeValidate.AspNetCore.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.FluentValidation.Tests.Adapters
{
    public class CreditCardClientValidatorTests
    {
        private class TestObject
        {
            public string CreditCardNo { get; set; }
        }

        [Fact]
        public void AddValidation_adds_credit_card_rule()
        {
            // Arrange
            var property = PropertyRule.Create<TestObject, string>(x => x.CreditCardNo);
            var adapter = new CreditCardClientValidator(property, new CreditCardValidator());
            
            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{credit_card:true}");
        }
    }
}
