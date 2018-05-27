using Shouldly;
using System.Collections.Generic;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class MaxLengthAttributeAdapterTests
    {
        [Fact]
        public void AddVeeValidateRules_adds_max_rule()
        {
            // Arrange
            var adapter = new MaxLengthAttributeAdapter();
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("50", null, rules);

            // Assert
            rules.Keys.ShouldContain("max");
            rules["max"].ShouldBe("50");
        }
    }
}
