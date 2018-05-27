using Shouldly;
using System.Collections.Generic;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class UrlAttributeAdapterTests
    {
        [Fact]
        public void AddVeeValidateRules_adds_url_rule()
        {
            // Arrange     
            var adapter = new UrlAttributeAdapter();
            var rules = new Dictionary<string, string>();
            
            // Act
            adapter.AddVeeValidateRules("", null, rules);

            // Assert
            rules.Keys.ShouldContain("url");
            rules["url"].ShouldBe("[true,true]");            
        }
    }
}
