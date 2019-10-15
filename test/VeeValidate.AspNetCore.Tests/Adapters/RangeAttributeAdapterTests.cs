using System;
using Shouldly;
using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Builders;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RangeAttributeAdapterTests
    {
        private readonly VeeValidateOptions _options = new VeeValidateOptions
        {
            DateFormatProvider = ctx => "dd/MM/yyyy"
        };

        [Fact]
        public void AddValidation_adds_min_value_and_max_value_rules()
        {
            // Arrange
            var attribute = new RangeAttribute(1, 100);
            var adapter = new RangeAttributeAdapter(attribute, _options);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModelType<int>()
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
            var adapter = new RangeAttributeAdapter(attribute, _options);

            var context = new ClientModelValidationContextBuilder()
                .WithModelType<DateTime?>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{date_format:'dd/MM/yyyy',date_between:['01/03/2016','31/03/2016',true]}");
        }

        [Theory]
        [InlineData("2016-03-01", "2016-03-31")]
        [InlineData("01/03/2016", "31/03/2016")]
        [InlineData("Mar 01 2016", "Mar 31 2016")]
        public void AddValidation_adds_after_and_before_rules(string minDate, string maxDate)
        {
            // Arrange
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-GB"); // Set culture for date string conversion

            var attribute = new RangeAttribute(typeof(DateTime), minDate, maxDate);
            var adapter = new RangeAttributeAdapter(attribute, _options);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModelType<DateTime>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{date_format:'dd/MM/yyyy',date_between:['01/03/2016','31/03/2016',true]}");
        }
    }
}
