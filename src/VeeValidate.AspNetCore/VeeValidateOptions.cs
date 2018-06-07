using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace VeeValidate.AspNetCore
{
    public class VeeValidateOptions
    {   
        /// <summary>
        /// VeeValidate errorbag name. Uses "errors" by default.
        /// </summary>
        public string ErrorBagName { get; set; }

        /// <summary>
        /// VeeValidate fieldbag name. Uses "fields" by default.
        /// </summary>
        public string FieldBagName { get; set; }

        /// <summary>
        /// The css class to add to the field when it's in an invalid state.
        /// Uses "input-validation-error" by default.
        /// </summary>
        public string ValidationInputCssClassName { get; set; }

        /// <summary>
        /// The css class to add to the field level validation messages.
        /// Uses "field-validation-error" by default.
        /// </summary>
        public string ValidationMessageCssClassName { get; set; }

        /// <summary>
        /// The css class to add to the validation summary.
        /// Uses "validation-summary-errors" by default.
        /// </summary>
        public string ValidationSummaryCssClassName { get; set; }
        
        /// <summary>
        /// Override the default behaviour of the asp-validation-for and asp-validation-summary tag helpers.
        /// Uses true by default.
        /// </summary>
        public bool OverrideValidationTagHelpers { get; set; }

        /// <summary>
        /// Whether to add the input css bindings to fields without client side validation.
        /// Useful if manually adding errors to the errorbag.
        /// Uses false by default.
        /// </summary>
        public bool AddValidationInputCssToFieldsWithoutValidation { get; set; }

        /// <summary>
        /// Function that returns the expected client side date format.
        /// The result must be in <see href="https://date-fns.org/v2.0.0-alpha.7/docs/format">date-fns</see> format.  
        /// Uses the ShortDatePattern from the CurrentCulture by default.
        /// </summary>
        public Func<HttpContext, string> DateFormatProvider { get; set; }

        public VeeValidateOptions()
        {
            // Set default values.
            ErrorBagName = "errors";
            FieldBagName = "fields";
            ValidationInputCssClassName = HtmlHelper.ValidationInputCssClassName;
            ValidationMessageCssClassName = HtmlHelper.ValidationMessageCssClassName;
            ValidationSummaryCssClassName = HtmlHelper.ValidationSummaryCssClassName;
            OverrideValidationTagHelpers = true;
            AddValidationInputCssToFieldsWithoutValidation = false;
            DateFormatProvider = ctx => CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ToUpper();
        }
    }
}
