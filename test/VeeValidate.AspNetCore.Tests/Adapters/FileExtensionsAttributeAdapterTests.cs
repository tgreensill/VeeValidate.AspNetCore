using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class FileExtensionsAttributeAdapterTests
    {
        [Fact]
        public void AddValidation_adds_validation_rule()
        {
            // Arrange
            var adapter = new FileExtensionsAttributeAdapter();

            // Act
            var result = adapter.GetVeeValidateRule("pdf,png,zip", null);

            // Assert
            result.ShouldBe("ext:['pdf','png','zip']");
        }
    }
}
