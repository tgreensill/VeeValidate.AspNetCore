using Shouldly;
using System.Collections.Generic;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class CreditCardAttributeAdapterTests
    {
        [Fact]        
        public void AddVeeValidateRules_adds_credit_card_rule()
        {
            // Arrange            
            var adapter = new CreditCardAttributeAdapter();
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("", null, rules);

            // Assert
            rules.Keys.ShouldContain("credit_card");
            rules["credit_card"].ShouldBe("true");
        }
    }
}
