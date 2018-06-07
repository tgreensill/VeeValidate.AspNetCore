using Shouldly;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class CreditCardAttributeAdapterTests
    {
        [Fact]
        public void AddValidation_adds_credit_card_rule()
        {
            // Arrange
            var attribute = new CreditCardAttribute();
            var adapter = new CreditCardAttributeAdapter(attribute);
            
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
