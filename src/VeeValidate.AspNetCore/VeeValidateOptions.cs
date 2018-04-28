using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Globalization;

namespace VeeValidate.AspNetCore
{
    public class VeeValidateOptions
    {
        // TODO: numeric
        
        // TODO - Change to an action to allow globalization
        /// <summary>
        /// Date format in date-fns format, see https://date-fns.org/v2.0.0-alpha.7/docs/format
        /// </summary>
        public string DateFormat { get; set; } = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern.ToUpper();
        public string ErrorBagName { get; set; } = "errors";
        public string ValidationMessageCssClassName { get; set; } = HtmlHelper.ValidationMessageCssClassName;
        public string ValidationInputCssClassName { get; set; } = HtmlHelper.ValidationInputCssClassName;
        public string ValidationSummaryCssClassName { get; set; } = HtmlHelper.ValidationSummaryCssClassName;
        public bool RequireUrlProtocol { get; set; } = true;
    }
}
