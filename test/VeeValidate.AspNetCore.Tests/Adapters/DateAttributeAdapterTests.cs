using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class DateAttributeAdapterTests
    {
        private readonly VeeValidateOptions _options = new VeeValidateOptions
        {
            Dates = new DateValidationOptions
            {
                Format = "DD/MM/YYYY"
            }
        };

        [Fact]
        public void AddValidation_adds_validation_rule()
        {
            // Arrange
            var adapter = new DateAttributeAdapter(_options);

            // Act
            var result = adapter.GetVeeValidateRule("", null);

            // Assert
            result.ShouldBe("date_format:'DD/MM/YYYY'");
        }
    }
}
