using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class UrlAttributeAdapterTests
    {
        [Fact]
        public void AddValidation_adds_validation_rule()
        {
            // Arrange     
            var adapter = new UrlAttributeAdapter();
            
            // Act
            var result = adapter.GetVeeValidateRule("", null);

            // Assert
            result.ShouldBe("url:[true,true]");
        }
    }
}
