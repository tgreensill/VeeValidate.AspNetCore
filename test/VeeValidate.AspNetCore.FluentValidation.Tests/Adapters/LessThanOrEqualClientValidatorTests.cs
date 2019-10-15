using System;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.Builders;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
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
        public void AddValidation_adds_date_between_rule(Type type)
        {
            // Arrange
            const string DATE_FORMAT = "dd/MM/yyyy";
            DateTime max = new DateTime(2016, 12, 31);

            var property = PropertyRule.Create<TestObject, DateTime>(x => x.LessThanOrEqualDate);
            var adapter = new LessThanOrEqualClientValidator(
                property, new LessThanOrEqualValidator(max), ctx => DATE_FORMAT);

            var context = new ClientModelValidationContextBuilder()
                .WithModelType(type)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe($"{{date_format:'{DATE_FORMAT}',date_between:['{DateTime.MinValue.ToString(DATE_FORMAT)}','31/12/2016',true]}}");
        }
    }
}
