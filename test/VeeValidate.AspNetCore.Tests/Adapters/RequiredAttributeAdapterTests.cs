using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RequiredAttributeAdapterTests
    {        
        [Fact]
        public void AddValidation_adds_validation_rule()
        {
            // Arrange
            var adapter = new RequiredAttributeAdapter();
            
            // Act
            var result = adapter.GetVeeValidateRule("true", null);

            // Assert            
            result.ShouldBe("required:true");
        }
    }
}
