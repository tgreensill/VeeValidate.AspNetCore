using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
using VeeValidate.AspNetCore.FluentValidation.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.FluentValidation.Tests.Adapters
{
    public class EqualClientValidatorTests
    {
        private class TestObject
        {
            public string Property { get; set; }
            public string OtherProperty { get; set; }
        }

        [Fact]
        public void AddValidation_adds_confirmed_rule()
        {   
            // Arrange
            var property = PropertyRule.Create<TestObject, string>(x => x.Property);
            var otherProperty = PropertyRule.Create<TestObject, string>(x => x.OtherProperty);
            var validator = new EqualValidator(otherProperty.PropertyFunc, otherProperty.Member);
            var adapter = new EqualClientValidator(property, validator);

            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{confirmed:'OtherProperty'}");
        }
    }
}
