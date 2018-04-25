using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Globalization;

namespace VeeValidate.AspNetCore
{
    public class VeeValidateOptions
    {
        // TODO: numeric
        // See: https://date-fns.org/v2.0.0-alpha.7/docs/format
        public string DateTimeFormat { get; set; } = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern.ToUpper();
        public string ErrorBagName { get; set; } = "errors";
        public string ValidationMessageCssClassName { get; set; } = HtmlHelper.ValidationMessageCssClassName;
        public string ValidationInputCssClassName { get; set; } = HtmlHelper.ValidationInputCssClassName;        
    }
}
