using Shouldly;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RegularExpressionAttributeAdapterTests
    {
        [Theory]
        [InlineData("[a-zA-Z]")]
        [InlineData("/[a-zA-Z]/")]
        public void AddValidation_adds_regex_rule(string pattern)
        {
            // Arrange
            var attribute = new RegularExpressionAttribute(pattern);
            var adapter = new RegularExpressionAttributeAdapter(attribute);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{regex:/[a-zA-Z]/}");
        }
    }
}
