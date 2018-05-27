using Shouldly;
using System.Collections.Generic;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RegularExpressionAttributeAdapterTests
    {
        [Theory]
        [InlineData("[a-zA-Z]")]
        [InlineData("/[a-zA-Z]/")]
        public void AddVeeValidateRules_adds_regex_rule(string value)
        {
            // Arrange
            var adapter = new RegularExpressionAttributeAdapter();
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules(value, null, rules);

            // Assert
            rules.Keys.ShouldContain("regex");
            rules["regex"].ShouldBe("/[a-zA-Z]/");
        }
    }
}
