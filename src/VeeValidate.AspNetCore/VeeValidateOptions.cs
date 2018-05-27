using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        /// If set to true, the asp-message-for and asp-validation-summary tag helper behaviour will be overridden to only show VeeValidate errors.
        /// Uses true by default
        /// </summary>
        public bool UseVeeValidateHtmlGenerator { get; set; }

        public DateValidationOptions Dates { get; set; } 

        public VeeValidateOptions()
        {
            // TODO - Flag for specifying whether to camel case property names??
            
            // Set default values.
            ErrorBagName = "errors";
            FieldBagName = "fields";
            ValidationInputCssClassName = HtmlHelper.ValidationInputCssClassName;
            ValidationMessageCssClassName = HtmlHelper.ValidationMessageCssClassName;
            ValidationSummaryCssClassName = HtmlHelper.ValidationSummaryCssClassName;
            UseVeeValidateHtmlGenerator = true;
            Dates = new DateValidationOptions();
        }
    }

    public class DateValidationOptions
    {
        /// <summary>
        /// Expected client side date format.
        /// Must be in <see href="https://date-fns.org/v2.0.0-alpha.7/docs/format">date-fns</see> format.  
        /// Uses the  ShortDatePattern from the CurrentCulture by default.
        /// </summary>
        public string Format { get; set; } = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ToUpper();
    }
}
