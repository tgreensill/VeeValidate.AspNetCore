using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class MaxLengthAttributeAdapterTests
    {
        [Fact]
        public void AddValidation_adds_validation_rule()
        {
            // Arrange
            var adapter = new MaxLengthAttributeAdapter();
            
            // Act
            var result = adapter.GetVeeValidateRule("50", null);

            // Assert
            result.ShouldBe("max:50");
        }
    }
}
