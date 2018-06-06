using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using VeeValidate.AspNetCore.ViewFeatures;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.ViewFeatures
{
    public class VueHtmlAttributeHelperTests
    {
        [Theory]
        [InlineData("classObject")]
        [InlineData("[activeClass, errorClass]")]
        [InlineData("{ active: isActive }")]
        public static void MergeClassAttribute_adds_attribute(string value)
        {
            // Arrange
            var attributes = new Dictionary<string, string>();

            // Act
            VueHtmlAttributeHelper.MergeClassAttribute(attributes, value);

            // Assert
            attributes.Keys.FirstOrDefault().ShouldBe("v-bind:class");
            attributes.Values.FirstOrDefault().ShouldBe(value);
        }

        [Theory]
        [InlineData("classObject")]
        [InlineData("[activeClass, errorClass]")]
        [InlineData("{ active: isActive }")]
        public static void MergeClassAttribute_concatenates_existing_attributes(string existingValue)
        {
            // Arrange
            var attributes = new Dictionary<string, string>
            {
                {":class", existingValue}
            };

            // Act
            VueHtmlAttributeHelper.MergeClassAttribute(attributes, "anotherClassObject");

            // Assert
            attributes.Keys.FirstOrDefault().ShouldBe(":class");
            attributes.Values.FirstOrDefault().ShouldBe($"[{existingValue.TrimStart('[').TrimEnd(']')},anotherClassObject]");
        }

        [Theory]
        [InlineData(":class")]
        [InlineData("v-bind:class")]
        public static void MergeClassAttribute_preserves_existing_attribute_name(string attributeName)
        {
            // Arrange
            var attributes = new Dictionary<string, string>
            {
                {attributeName, "classObject"}
            };

            // Act
            VueHtmlAttributeHelper.MergeClassAttribute(attributes, "anotherClassObject");

            // Assert
            attributes.Keys.FirstOrDefault().ShouldBe(attributeName);
            attributes.Values.FirstOrDefault().ShouldBe("[classObject,anotherClassObject]");
        }

        [Fact]
        public static void MergeVeeValidateAttributes_adds_attribute()
        {
            // Arrange
            var attributes = new Dictionary<string, string>();

            // Act
            VueHtmlAttributeHelper.MergeVeeValidateAttribute(attributes, "email", "true");
            VueHtmlAttributeHelper.MergeVeeValidateAttribute(attributes, "required", "true");

            // Assert
            attributes.Keys.FirstOrDefault().ShouldBe("v-validate");
            attributes.Values.FirstOrDefault().ShouldBe("{email:true,required:true}");
        }

        [Fact]
        public static void MergeVeeValidateAttributes_concatenates_existing_rules()
        {
            // Arrange
            var attributes = new Dictionary<string, string>
            {
                { "v-validate", "{required:true}" }
            };

            // Act
            VueHtmlAttributeHelper.MergeVeeValidateAttribute(attributes, "email", "true");

            // Assert
            attributes.Keys.FirstOrDefault().ShouldBe("v-validate");
            attributes.Values.FirstOrDefault().ShouldBe("{required:true,email:true}");
        }

        [Fact]
        public static void MergeVeeValidateAttributes_throws_when_validation_rules_in_string_format()
        {
            // Arrange
            var attributes = new Dictionary<string, string>
            {
                { "v-validate", "'required|email'"}
            };

            // Act

            // Assert
            Should.Throw<Exception>(() =>
                VueHtmlAttributeHelper.MergeVeeValidateAttribute(attributes, "credit_card", "true"));
        }
    }
}
