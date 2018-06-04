using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using NSubstitute;
using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests
{
    public class VeeValidateAttributeAdapterProviderTests
    {
        private readonly IValidationAttributeAdapterProvider _provider;

        public VeeValidateAttributeAdapterProviderTests()
        {
            _provider = new VeeValidateAttributeAdapterProvider(new VeeValidateOptions());
        }

        [Theory]
        [MemberData(nameof(GetAttributeAdapterCases))]
        public void GetAttributeAdapter_returns_adapter_for_attribute(ValidationAttribute attribute, Type expectedAdapterType)
        {
            // Arrange

            // Act
            var adapter = _provider.GetAttributeAdapter(attribute, Substitute.For<IStringLocalizer>());

            // Assert
            adapter.ShouldBeOfType(expectedAdapterType);
        }

        public static IEnumerable<object[]> GetAttributeAdapterCases =>
            new List<object[]>
            {
                new object[] { new CompareAttribute("OtherProperty"), typeof(CompareClientValidator) },
                new object[] { new CreditCardAttribute(), typeof(CreditCardClientValidator) },
                new object[] { new EmailAddressAttribute(), typeof(EmailAddressClientValidator) },
                new object[] { new FileExtensionsAttribute(), typeof(FileExtensionsClientValidator) },
                new object[] { new MaxLengthAttribute(), typeof(MaxLengthClientValidator) },
                new object[] { new MinLengthAttribute(2), typeof(MinLengthClientValidator) },
                new object[] { new RangeAttribute(1, 2), typeof(RangeClientValidator) },
                new object[] { new RegularExpressionAttribute("[a-zA-Z]"), typeof(RegularExpressionClientValidator) },
                new object[] { new RequiredAttribute(), typeof(RequiredClientValidator) },
                new object[] { new StringLengthAttribute(2), typeof(StringLengthClientValidator) },
                new object[] { new UrlAttribute(), typeof(UrlClientValidator) }
            };
    }
}
