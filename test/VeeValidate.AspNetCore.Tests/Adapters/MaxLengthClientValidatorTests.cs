﻿using Shouldly;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class MaxLengthClientValidatorTests
    {
        [Fact]
        public void AddValidation_adds_max_rule()
        {
            // Arrange
            var attribute = new MaxLengthAttribute(10);
            var adapter = new MaxLengthClientValidator(attribute);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{max:10}");
        }
    }
}
