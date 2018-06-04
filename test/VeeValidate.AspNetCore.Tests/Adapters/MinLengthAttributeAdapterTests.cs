using Shouldly;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class MinLengthAttributeAdapterTests
    {
        [Fact]
        public void AddValidation_adds_min_rule()
        {
            // Arrange
            var attribute = new MinLengthAttribute(3);
            var adapter = new MinLengthAttributeAdapter(attribute);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{min:3}");
        }
    }
}
