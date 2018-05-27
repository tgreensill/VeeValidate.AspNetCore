using Shouldly;
using System.Collections.Generic;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class CompareAttributeAdapterTests
    {
        [Fact]
        public void AddVeeValidateRules_adds_confirmed_rule()
        {
            // Arrange
            var adapter = new CompareAttributeAdapter();
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("*.PropertyName", null, rules);

            // Assert
            rules.Keys.ShouldContain("confirmed");
            rules["confirmed"].ShouldBe("'PropertyName'");
        }
    }
}
