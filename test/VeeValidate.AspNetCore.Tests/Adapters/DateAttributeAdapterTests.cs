using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class DateAttributeAdapterTests
    {
        [Fact]
        public void AddVeeValidateRules_adds_date_rule()
        {
            // Arrange
            var options = new VeeValidateOptions
            {
                Dates = new DateValidationOptions
                {
                    Format = "DD/MM/YYYY"
                }
            };
            var adapter = new DateAttributeAdapter(options);
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(DateTime));
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("", null, rules);

            // Assert
            rules.Keys.ShouldContain("date_format");
            rules["date_format"].ShouldBe("'DD/MM/YYYY'");
        }
    }
}
