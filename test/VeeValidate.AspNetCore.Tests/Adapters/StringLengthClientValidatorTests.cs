﻿using System.ComponentModel.DataAnnotations;
using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class StringLengthClientValidatorTests
    {
        [Fact]
        public void AddValidation_adds_max_rule()
        {
            // Arrange
            var attribute = new StringLengthAttribute(10);
            var adapter = new StringLengthClientValidator(attribute);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{max:10}");
        }

        [Fact]
        public void AddValidation_adds_max_and_min_rules()
        {
            // Arrange
            var attribute = new StringLengthAttribute(10) { MinimumLength = 1 };
            var adapter = new StringLengthClientValidator(attribute);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{max:10,min:1}");
        }
    }
}
