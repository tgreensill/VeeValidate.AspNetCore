﻿using System;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.Builders;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.FluentValidation.Tests.Adapters
{
    public class GreaterThanOrEqualClientValidatorTests
    {
        private class TestObject
        {
            public string GreaterThanOrEqual { get; set; }
            public DateTime GreaterThanOrEqualDate { get; set; }
        }

        [Fact]
        public void AddValidation_adds_min_value_rule()
        {
            // Arrange
            var property = PropertyRule.Create<TestObject, string>(x => x.GreaterThanOrEqual);
            var adapter = new GreaterThanOrEqualClientValidator(property, new GreaterThanOrEqualValidator(5), null);

            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{min_value:5}");
        }

        [Theory]
        [InlineData(typeof(DateTime?))]
        [InlineData(typeof(DateTime))]
        public void AddValidation_adds_date_between_rule(Type type)
        {
            // Arrange
            const string DATE_FORMAT = "dd/MM/yyyy";
            DateTime max = new DateTime(2016, 3, 1);

            var property = PropertyRule.Create<TestObject, DateTime>(x => x.GreaterThanOrEqualDate);
            var adapter = new GreaterThanOrEqualClientValidator(
                property, new GreaterThanOrEqualValidator(max), ctx => DATE_FORMAT);

            var context = new ClientModelValidationContextBuilder()
                .WithModelType(type)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe($"{{date_format:'{DATE_FORMAT}',date_between:['01/03/2016','{DateTime.MaxValue.ToString(DATE_FORMAT)}',true]}}");
        }
    }
}
