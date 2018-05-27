using System;
using Shouldly;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VeeValidate.AspNetCore.Adapters;
using Xunit;
using System.Collections.Generic;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RangeMaxAttributeAdapterTests
    {
        private readonly VeeValidateOptions _options = new VeeValidateOptions
        {
            Dates = new DateValidationOptions
            {
                Format = "DD/MM/YYYY"
            }
        };

        [Fact]
        public void AddVeeValidateRules_adds_max_value_rule()
        {
            // Arrange
            var adapter = new RangeMaxAttributeAdapter(_options);
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(int));
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("10", metadata, rules);

            // Assert
            rules.Keys.ShouldContain("max_value");
            rules["max_value"].ShouldBe("'10'");
        }

        [Theory]
        [InlineData("2016-03-01")]
        [InlineData("03/01/2016")]
        [InlineData("Mar 01 2016")]
        public void AddVeeValidateRules_adds_before_and_date_format_rule_for_date_types(string date)
        {
            // Arrange
            var adapter = new RangeMaxAttributeAdapter(_options);
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(DateTime));
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules(date, metadata, rules);

            // Assert
            rules.Keys.ShouldContain("before");
            rules["before"].ShouldBe("['01/03/2016',true]");
            rules.Keys.ShouldContain("date_format");
            rules["date_format"].ShouldBe("'DD/MM/YYYY'");
        }
    }
}
