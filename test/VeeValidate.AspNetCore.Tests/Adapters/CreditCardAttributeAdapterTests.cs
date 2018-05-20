using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class CreditCardAttributeAdapterTests
    {
        [Fact]        
        public void AddValidation_adds_validation_rule()
        {
            // Arrange            
            var adapter = new CreditCardAttributeAdapter();
            
            // Act
            var result = adapter.GetVeeValidateRule("", null);

            // Assert
            result.ShouldBe("credit_card:true");
        }
    }
}
