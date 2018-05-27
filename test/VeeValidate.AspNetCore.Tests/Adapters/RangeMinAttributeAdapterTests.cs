using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RangeMinAttributeAdapterTests
    {
        private readonly VeeValidateOptions _options = new VeeValidateOptions
        {
            Dates = new DateValidationOptions
            {
                Format = "DD/MM/YYYY"
            }
        };

        [Fact]
        public void AddVeeValidateRules_adds_min_value_rule()
        {
            // Arrange
            var adapter = new RangeMinAttributeAdapter(_options);
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(int));
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("10", metadata, rules);

            // Assert
            rules.Keys.ShouldContain("min_value");
            rules["min_value"].ShouldBe("'10'");
        }

        [Fact]
        public void AddVeeValidateRules_adds_after_rule_and_date_format_for_date_types()
        {
            // Arrange
            var adapter = new RangeMinAttributeAdapter(_options);
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(DateTime));
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("03/01/2016", metadata, rules);

            // Assert
            rules.Keys.ShouldContain("after");
            rules["after"].ShouldBe("['01/03/2016',true]");
            rules.Keys.ShouldContain("date_format");
            rules["date_format"].ShouldBe("'DD/MM/YYYY'");
        }
    }
}
