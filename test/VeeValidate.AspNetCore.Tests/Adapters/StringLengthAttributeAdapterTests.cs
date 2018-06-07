using System.ComponentModel.DataAnnotations;
using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class StringLengthAttributeAdapterTests
    {
        [Fact]
        public void AddValidation_adds_max_rule()
        {
            // Arrange
            var attribute = new StringLengthAttribute(10);
            var adapter = new StringLengthAttributeAdapter(attribute);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{max:10}");
        }

        [Fact]
        public void AddValidation_adds_max_and_min_rules()
        {
            // Arrange
            var attribute = new StringLengthAttribute(10) { MinimumLength = 1 };
            var adapter = new StringLengthAttributeAdapter(attribute);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{max:10,min:1}");
        }
    }
}
