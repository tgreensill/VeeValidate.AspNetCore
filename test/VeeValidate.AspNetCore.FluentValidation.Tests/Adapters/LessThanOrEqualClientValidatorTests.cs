using System;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
using VeeValidate.AspNetCore.FluentValidation.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.FluentValidation.Tests.Adapters
{
    public class LessThanOrEqualClientValidatorTests
    {
        private class TestObject
        {
            public string LessThanOrEqual { get; set; }
            public DateTime LessThanOrEqualDate { get; set; }
        }

        [Fact]
        public void AddValidation_adds_max_value_rule()
        {
            // Arrange
            var property = PropertyRule.Create<TestObject, string>(x => x.LessThanOrEqual);
            var adapter = new LessThanOrEqualClientValidator(property, new LessThanOrEqualValidator(100), null);

            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{max_value:100}");
        }

        [Theory]
        [InlineData(typeof(DateTime?))]
        [InlineData(typeof(DateTime))]
        public void AddValidation_adds__before_rule(Type type)
        {
            // Arrange
            DateTime max = new DateTime(2016, 12, 31);

            var property = PropertyRule.Create<TestObject, DateTime>(x => x.LessThanOrEqualDate);
            var adapter = new LessThanOrEqualClientValidator(
                property, new LessThanOrEqualValidator(max), ctx => "DD/MM/YYYY");

            var context = new ClientModelValidationContextBuilder()
                .WithModelType(type)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{date_format:'DD/MM/YYYY',before:['31/12/2016',true]}");
        }
    }
}
