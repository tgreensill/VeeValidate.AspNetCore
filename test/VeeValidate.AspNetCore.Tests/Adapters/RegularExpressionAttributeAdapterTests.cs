using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RegularExpressionAttributeAdapterTests
    {
        [Fact]
        public void AddValidation_adds_validation_rule()
        {
            // Arrange
            var adapter = new RegularExpressionAttributeAdapter();

            // Act
            var result = adapter.GetVeeValidateRule("[a-zA-Z]", null);

            // Assert
            result.ShouldBe("regex:/[a-zA-Z]/");
        }
    }
}
