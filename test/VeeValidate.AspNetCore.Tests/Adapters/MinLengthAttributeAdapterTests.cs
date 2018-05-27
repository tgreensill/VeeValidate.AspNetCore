using Shouldly;
using System.Collections.Generic;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class MinLengthAttributeAdapterTests
    {
        [Fact]
        public void AddVeeValidateRules_adds_min_rule()
        {
            // Arrange
            var adapter = new MinLengthAttributeAdapter();
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("10", null, rules);

            // Assert
            rules.Keys.ShouldContain("min");
            rules["min"].ShouldBe("10");
        }
    }
}
