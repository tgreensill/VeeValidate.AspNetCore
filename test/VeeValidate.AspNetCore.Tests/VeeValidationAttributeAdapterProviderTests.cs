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
    public class VeeValidationAttributeAdapterProviderTests
    {
        private readonly IValidationAttributeAdapterProvider _provider;

        public VeeValidationAttributeAdapterProviderTests()
        {
            _provider = new VeeValidationAttributeAdapterProvider(new VeeValidateOptions());
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
                new object[] { new RequiredAttribute(), typeof(RequiredAttributeAdapter) },
                new object[] { new CompareAttribute("test"), typeof(CompareAttributeAdapter) },
                new object[] { new RangeAttribute(1, 2), typeof(RangeAttributeAdapter) },
                new object[] { new EmailAddressAttribute(), typeof(DataTypeAttributeAdapter) },
                new object[] { new CreditCardAttribute(), typeof(DataTypeAttributeAdapter) }
            };
    }
}
