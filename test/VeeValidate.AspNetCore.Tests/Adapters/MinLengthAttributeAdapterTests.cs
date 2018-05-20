using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class MinLengthAttributeAdapterTests
    {
        [Fact]
        public void AddValidation_adds_validation_rule()
        {
            // Arrange
            var adapter = new MinLengthAttributeAdapter();
            
            // Act
            var result = adapter.GetVeeValidateRule("10", null);

            // Assert
            result.ShouldBe("min:10");
        }
    }
}
