using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests
{
    public class VeeAttributeAdapterProviderTests
    {
        private readonly IValidationAttributeAdapterProvider _provider;

        public VeeAttributeAdapterProviderTests()
        {
            _provider = new VeeAttributeAdapterProvider(new VeeValidateOptions());
        }

        [Theory]
        [MemberData(nameof(GetAttributeAdapterCases))]
        public void GetAttributeAdapter_returns_adapter_for_attribute(ValidationAttribute attribute, Type type)
        {
            // Arrange

            // Act
            var adapter = _provider.GetAttributeAdapter(attribute, Substitute.For<IStringLocalizer>());

            // Assert
            adapter.ShouldBeOfType(type);            
        }

        public static IEnumerable<object[]> GetAttributeAdapterCases =>
            new List<object[]>
            {
                new object[] { new RequiredAttribute(), typeof(RequiredClientValidator) },
                new object[] { new CompareAttribute("test"), typeof(CompareClientValidator) },
                new object[] { new RangeAttribute(1, 2), typeof(RangeClientValidator) },
                new object[] { new EmailAddressAttribute(), typeof(DataTypeClientValidator) },
                new object[] { new CreditCardAttribute(), typeof(DataTypeClientValidator) }
            };
    }
}
