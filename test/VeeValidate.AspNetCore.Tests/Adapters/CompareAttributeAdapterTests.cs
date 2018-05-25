using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class CompareAttributeAdapterTests
    {
        [Fact]
        public void AddValidation_adds_validation_rule()
        {
            // Arrange
            var adapter = new CompareAttributeAdapter();
            
            // Act
            var result = adapter.GetVeeValidateRule("*.PropertyName", null);

            // Assert
            result.ShouldBe("confirmed:'PropertyName'");
        }
    }
}
