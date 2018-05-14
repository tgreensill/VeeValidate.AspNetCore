using Shouldly;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class FileExtensionsClientValidatorTests
    {
        [Fact]
        public void AddValidation_adds_validation_rule()
        {
            // Arrange
            var attribute = new FileExtensionsAttribute
            {
                Extensions = "pdf,png,zip"
            };
            var adapter = new FileExtensionsClientValidator(attribute);

            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert            
            context.Attributes.ShouldContainKey("data-vv-as");
            context.Attributes.ShouldContainKey("v-validate");
            context.Attributes["v-validate"].ShouldBe("{ext:['pdf','png','zip']}");
        }

        [Fact]
        public void AddValidation_replaces_pipe_delimiters()
        {
            // Arrange
            var attribute = new FileExtensionsAttribute
            {
                Extensions = "pdf|png|gif"
            };
            var adapter = new FileExtensionsClientValidator(attribute);

            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert            
            context.Attributes.ShouldContainKey("data-vv-as");
            context.Attributes.ShouldContainKey("v-validate");
            context.Attributes["v-validate"].ShouldBe("{ext:['pdf','png','gif']}");
        }
    }
}
