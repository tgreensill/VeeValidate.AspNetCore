using Shouldly;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RequiredAttributeAdapterTests
    {
        [Fact]
        public void AddValidation_adds_required_rule()
        {
            // Arrange
            var attribute = new RequiredAttribute();
            var adapter = new RequiredAttributeAdapter(attribute);
            
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
