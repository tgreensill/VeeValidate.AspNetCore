using Shouldly;
using System;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RangeAttributeAdapterTests
    {
        [Fact]
        public void AddValidation_adds_min_and_max_rules()
        {
            // Arrange
            var attribute = new RangeAttribute(2, 99);
            var adapter = new RangeAttributeAdapter(attribute, new VeeValidateOptions());

            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();
            
            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.ShouldContainKey("data-vv-as");
            context.Attributes.ShouldContainKey("v-validate");
            context.Attributes["v-validate"].ShouldBe("{max_value:99,min_value:2");            
        }

        [Fact]
        public void AddValidation_adds_before_and_after_rules_for_dates()
        {
            // Arrange
            var attribute = new RangeAttribute(typeof(DateTime), "2000-01-15", "2020-06-03");
            var adapter = new RangeAttributeAdapter(attribute, new VeeValidateOptions());

            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.ShouldContainKey("data-vv-as");
            context.Attributes.ShouldContainKey("v-validate");
            context.Attributes["v-validate"].ShouldBe("{date_format:'DD/MM/YYYY',date_between:15/01/2000,03/06/2020,true}");
        }
    }
}
