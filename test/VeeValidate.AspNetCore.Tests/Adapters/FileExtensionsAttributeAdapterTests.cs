using Shouldly;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class FileExtensionsAttributeAdapterTests
    {
        [Theory]
        [InlineData("pdf,png,zip")]
        [InlineData("pdf|png|zip")]
        public void AddValidation_adds_ext_rule(string extensions)
        {
            // Arrange
            var attribute = new FileExtensionsAttribute { Extensions = extensions };
            var adapter = new FileExtensionsAttributeAdapter(attribute);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{ext:['pdf','png','zip']}");
        }
    }
}
