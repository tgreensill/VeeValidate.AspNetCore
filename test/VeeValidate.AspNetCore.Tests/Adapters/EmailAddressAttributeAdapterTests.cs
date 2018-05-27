using Shouldly;
using System.Collections.Generic;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class EmailAddressAttributeAdapterTests
    {
        [Fact]        
        public void AddVeeValidateRules_adds_email_rule()
        {
            // Arrange            
            var adapter = new EmailAddressAttributeAdapter();
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("", null, rules);

            // Assert
            rules.Keys.ShouldContain("email");
            rules["email"].ShouldBe("true");
        }
    }
}
