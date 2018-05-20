using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Globalization;

namespace VeeValidate.AspNetCore
{
    public class VeeValidateOptions
    {   
        public string ErrorBagName { get; set; } = "errors";
        public string FieldBagName { get; set; } = "fields";

        /// <summary>
        /// The css class to add to the field when it's in an invalid state.
        /// </summary>
        public string ValidationInputCssClassName { get; set; } = HtmlHelper.ValidationInputCssClassName;
        
        /// <summary>
        /// The css class to add to the field level validation messages.
        /// </summary>
        public string ValidationMessageCssClassName { get; set; } = HtmlHelper.ValidationMessageCssClassName;
        
        /// <summary>
        /// The css class to add to the validation summary.
        /// </summary>
        public string ValidationSummaryCssClassName { get; set; } = HtmlHelper.ValidationSummaryCssClassName;

        public DateValidationOptions Dates { get; set; } = new DateValidationOptions();
    }

    public class DateValidationOptions
    {        
        /// <summary>
        /// Expected client side date format. Must be in date-fns format, see https://date-fns.org/v2.0.0-alpha.7/docs/format
        /// </summary>
        public string Format { get; set; } = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern.ToUpper();
    }
}
