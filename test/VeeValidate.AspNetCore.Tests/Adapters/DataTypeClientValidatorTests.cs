using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class DataTypeClientValidatorTests
    {
        [Theory]
        [MemberData(nameof(AddValidationCases))]
        public void AddValidation_adds_validation_rule(DataTypeAttribute attribute, string rule)
        {
            // Arrange            
            var adapter = new DataTypeClientValidator(attribute, rule);

            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)                
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.ShouldContainKey("data-vv-as");
            context.Attributes.ShouldContainKey("v-validate");
            context.Attributes["v-validate"].ShouldBe("{" + rule + "}");
        }

        public static IEnumerable<object[]> AddValidationCases =>
            new List<object[]>
            {
                new object[] { new EmailAddressAttribute(), "email:true" },
                new object[] { new CreditCardAttribute(), "credit_card:true" },
                new object[] { new UrlAttribute(), "url:true" }                
            };
    }
}
