using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shouldly;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Extensions
{
    public class TagBuilderExtensionTests
    {
        [Theory]
        [InlineData("classObject")]
        [InlineData("[activeClass, errorClass]")]
        [InlineData("{ active: isActive }")]
        public static void MergeVeeBindAttribute_adds_attribute(string value)
        {
            // Arrange
            var builder = new TagBuilder("input");

            // Act
            builder.MergeVeeBindAttribute("class", value);
            
            // Assert
            builder.Attributes.First().Key.ShouldBe(":class");
            builder.Attributes.First().Value.ShouldBe(value);
        }

        [Theory]
        [InlineData("classObject")]
        [InlineData("[activeClass, errorClass]")]
        [InlineData("{ active: isActive }")]
        public static void MergeVeeBindAttribute_concatenates_existing_attributes(string existingValue)
        {
            // Arrange
            var builder = new TagBuilder("input");
            builder.Attributes.Add(":class", existingValue);

            // Act
            builder.MergeVeeBindAttribute("class", "anotherClassObject");

            // Assert
            builder.Attributes.First().Key.ShouldBe(":class");
            builder.Attributes.First().Value.ShouldBe($"[{existingValue.TrimStart('[').TrimEnd(']')},anotherClassObject]");
        }

        [Theory]
        [InlineData(":class")]
        [InlineData("v-bind:class")]
        public static void MergeVeeBindAttribute_preserves_existing_attribute_name(string attributeName)
        {
            // Arrange
            var builder = new TagBuilder("input");
            builder.Attributes.Add(attributeName, "classObject");

            // Act
            builder.MergeVeeBindAttribute("class", "anotherClassObject");

            // Assert
            builder.Attributes.First().Key.ShouldBe(attributeName);
            builder.Attributes.First().Value.ShouldBe("[classObject,anotherClassObject]");
        }

        // Can merge when exists

        // Can handle different key formats

        // Can handle different value formats
    }
}
