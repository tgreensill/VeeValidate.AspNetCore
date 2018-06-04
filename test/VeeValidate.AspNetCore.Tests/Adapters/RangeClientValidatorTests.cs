using System;
using Shouldly;
using Xunit;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Tests.Builders;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RangeClientValidatorTests
    {
        private readonly VeeValidateOptions _options = new VeeValidateOptions
        {
            Dates = new DateValidationOptions
            {
                Format = "DD/MM/YYYY"
            }
        };

        [Fact]
        public void AddValidation_adds_min_value_and_max_value_rules()
        {
            // Arrange
            var attribute = new RangeAttribute(1, 100);
            var adapter = new RangeClientValidator(attribute, _options);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{max_value:100,min_value:1}");
        }

        [Fact]
        public void AddValidation_adds_after_and_before_rules_for_nullables()
        {
            // Arrange
            var attribute = new RangeAttribute(typeof(DateTime?), "2016-03-01", "2016-03-31");
            var adapter = new RangeClientValidator(attribute, _options);

            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{date_format:'DD/MM/YYYY',after:['01/03/2016',true],before:['31/03/2016',true]}");
        }

        [Theory]
        [InlineData("2016-03-01", "2016-03-31")]
        [InlineData("01/03/2016", "31/03/2016")]
        [InlineData("Mar 01 2016", "Mar 31 2016")]
        public void AddValidation_adds_after_and_before_rules(string minDate, string maxDate)
        {
            // Arrange
            var attribute = new RangeAttribute(typeof(DateTime), minDate, maxDate);
            var adapter = new RangeClientValidator(attribute, _options);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{date_format:'DD/MM/YYYY',after:['01/03/2016',true],before:['31/03/2016',true]}");
        }
    }
}
