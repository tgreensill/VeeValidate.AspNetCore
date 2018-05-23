using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

namespace VeeValidate.AspNetCore.Tests
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
            attributes.Keys.FirstOrDefault().ShouldBe(":class");
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

        [Theory]
        [InlineData("required:true")]
        [InlineData("required:true", "email:true")]
        public static void VeeValidateAttribute_adds_attribute(params string[] rules)
        {
            // Arrange
            var attributes = new Dictionary<string, string>();
            
            // Act
            VueHtmlAttributeHelper.MergeVeeValidateAttributes(attributes, rules.ToList());

            // Assert
            attributes.Keys.FirstOrDefault().ShouldBe("v-validate");
            attributes.Values.FirstOrDefault().ShouldBe("{" + string.Join(",", rules) + "}");
        }

        [Fact]
        public static void VeeValidateAttribute_concatenates_existing_rules()
        {
            // Arrange
            var attributes = new Dictionary<string, string>
            {
                {"v-validate", "required:true"}
            };

            // Act
            VueHtmlAttributeHelper.MergeVeeValidateAttributes(attributes, new List<string> { "email:true" });

            // Assert
            attributes.Keys.FirstOrDefault().ShouldBe("v-validate");
            attributes.Values.FirstOrDefault().ShouldBe("{required:true,email:true}");
        }

        [Fact]
        public static void VeeValidateAttribute_throws_when_validation_rules_in_string_format()
        {
            // Arrange
            var attributes = new Dictionary<string, string>
            {
                { "v-validate", "'required|email'"}
            };

            // Act
            
            // Assert
            Should.Throw<Exception>(() =>
                VueHtmlAttributeHelper.MergeVeeValidateAttributes(attributes, new List<string>{ "credit_card:true" }));
        }
    }
}
