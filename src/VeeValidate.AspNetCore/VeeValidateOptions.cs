using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Globalization;

namespace VeeValidate.AspNetCore
{
    public class VeeValidateOptions
    {        
        // TODO - Change to an action to allow globalization        
        public string ErrorBagName { get; set; } = "errors";
        public string ValidationMessageCssClassName { get; set; } = HtmlHelper.ValidationMessageCssClassName;
        public string ValidationInputCssClassName { get; set; } = HtmlHelper.ValidationInputCssClassName;
        public string ValidationSummaryCssClassName { get; set; } = HtmlHelper.ValidationSummaryCssClassName;
        public DateValidationOptions Dates { get; set; } = new DateValidationOptions();
        public UrlValidationOptions Urls { get; set; } = new UrlValidationOptions();
    }

    public class UrlValidationOptions
    {
        public bool RequireUrlProtocol { get; set; } = true;
    }

    public class DateValidationOptions
    {        
        /// <summary>
        /// Date format in date-fns format, see https://date-fns.org/v2.0.0-alpha.7/docs/format
        /// </summary>
        public string DateFormat { get; set; } = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern.ToUpper();
    }
}
