using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class NumberAttributeAdapterTests
    {
        [Theory]
        [InlineData(typeof(short))]
        [InlineData(typeof(int))]
        [InlineData(typeof(long))]
        public void AddVeeValidateRules_adds_numeric_rule(Type modelType)
        {
            // Arrange
            var adapter = new NumberAttributeAdapter();
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(modelType);
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("true", metadata, rules);

            // Assert
            rules.Keys.ShouldContain("numeric");
            rules["numeric"].ShouldBe("true");
        }

        [Theory]
        [InlineData(typeof(double))]
        [InlineData(typeof(float))]
        [InlineData(typeof(decimal))]
        public void AddVeeValidateRules_adds_decimal_rule(Type modelType)
        {
            // Arrange
            var adapter = new NumberAttributeAdapter();
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(modelType);
            var rules = new Dictionary<string, string>();

            // Act
            adapter.AddVeeValidateRules("true", metadata, rules);

            // Assert
            rules.Keys.ShouldContain("decimal");
            rules["decimal"].ShouldBe("true");
        }
    }
}
