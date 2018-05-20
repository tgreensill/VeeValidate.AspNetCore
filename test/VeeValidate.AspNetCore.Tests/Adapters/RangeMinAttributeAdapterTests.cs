using System;
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
        public void AddValidation_adds_min_value_validation_rule()
        {
            // Arrange
            var adapter = new RangeMinAttributeAdapter(_options);
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(int));

            // Act
            var result = adapter.GetVeeValidateRule("10", metadata);

            // Assert
            result.ShouldBe("min_value:'10'");
        }

        [Fact]
        public void AddValidation_adds_after_validation_rule()
        {
            // Arrange
            var adapter = new RangeMinAttributeAdapter(_options);
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(DateTime));

            // Act
            var result = adapter.GetVeeValidateRule("01/03/2016", metadata);

            // Assert
            result.ShouldBe("after:['01/03/2016',true]");
        }
    }
}
