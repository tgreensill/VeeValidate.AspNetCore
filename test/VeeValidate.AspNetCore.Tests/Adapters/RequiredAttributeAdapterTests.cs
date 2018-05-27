using Shouldly;
using System.Collections.Generic;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RequiredAttributeAdapterTests
    {        
        [Fact]
        public void AddVeeValidateRules_adds_required_rule()
        {
            // Arrange
            var adapter = new RequiredAttributeAdapter();
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("", null, rules);

            // Assert
            rules.Keys.ShouldContain("required");
            rules["required"].ShouldBe("true");
        }
    }
}
