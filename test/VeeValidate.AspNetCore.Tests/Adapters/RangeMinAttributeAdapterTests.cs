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
            var result = adapter.GetVeeValidateRule("03/01/2016", metadata);

            // Assert
            result.ShouldBe("after:['01/03/2016',true]");
        }

        [Theory]
        [InlineData("2016-03-01")]
        [InlineData("03/01/2016")]
        [InlineData("Mar 01 2016")]
        public void AddValidation_adds_before_validation_rule(string date)
        {
            // Arrange
            var options = new VeeValidateOptions
            {
                Dates = new DateValidationOptions
                {
                    Format = "DD/MM/YYYY"
                }
            };
            var adapter = new RangeMaxAttributeAdapter(options);
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(DateTime));
            
            // Act
            var result = adapter.GetVeeValidateRule(date, metadata);

            // Assert
            result.ShouldBe("before:['01/03/2016',true]");
        }
    }
}
