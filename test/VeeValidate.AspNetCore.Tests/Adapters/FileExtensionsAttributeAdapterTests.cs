using Shouldly;
using System.Collections.Generic;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class FileExtensionsAttributeAdapterTests
    {
        [Fact]
        public void AddVeeValidateRules_adds_ext_rule()
        {
            // Arrange
            var adapter = new FileExtensionsAttributeAdapter();
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("pdf,png,zip", null, rules);

            // Assert
            rules.Keys.ShouldContain("ext");
            rules["ext"].ShouldBe("['pdf','png','zip']");
        }
    }
}
