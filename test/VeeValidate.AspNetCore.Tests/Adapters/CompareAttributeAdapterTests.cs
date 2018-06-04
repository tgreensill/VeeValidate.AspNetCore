using Shouldly;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class CompareAttributeAdapterTests
    {
        [Fact]
        public void AddValidation_adds_confirmed_rule()
        {
            // Arrange
            var attribute = new CompareAttribute("PropertyName");
            var adapter = new CompareAttributeAdapter(attribute);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();
            
            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{confirmed:'PropertyName'}");
        }
    }
}
