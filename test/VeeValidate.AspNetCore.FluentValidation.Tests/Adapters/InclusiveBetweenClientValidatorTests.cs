using System;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.Builders;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.FluentValidation.Tests.Adapters
{
    public class InclusiveBetweenClientValidatorTests
    {
        private class TestObject
        {
            public string InclusiveBetween { get; set; }
            public DateTime InclusiveBetweenDate { get; set; }
        }

        [Fact]
        public void AddValidation_adds_min_value_and_max_value_rules()
        {
            // Arrange
            var property = PropertyRule.Create<TestObject, string>(x => x.InclusiveBetween);
            var adapter = new InclusiveBetweenClientValidator(
                property, new InclusiveBetweenValidator(5, 50), null);

            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{min_value:5,max_value:50}");
        }
        
        [Theory]
        [InlineData(typeof(DateTime?))]
        [InlineData(typeof(DateTime))]
        public void AddValidation_adds_after_and_before_rules(Type type)
        {
            // Arrange
            DateTime from = new DateTime(2016, 3, 1);
            DateTime to = new DateTime(2016, 3, 31);

            var property = PropertyRule.Create<TestObject, DateTime>(x => x.InclusiveBetweenDate);
            var adapter = new InclusiveBetweenClientValidator(
                property, new InclusiveBetweenValidator(from, to), ctx => "DD/MM/YYYY");

            var context = new ClientModelValidationContextBuilder()
                .WithModelType(type)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{date_format:'DD/MM/YYYY',after:['01/03/2016',true],before:['31/03/2016',true]}");
        }
    }
}
