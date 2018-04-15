using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Globalization;

namespace VeeValidate.AspNetCore
{
    public class VeeValidateOptions
    {
        public string DateTimeFormat { get; set; } = CultureInfo.CurrentUICulture.DateTimeFormat.FullDateTimePattern;
        public string ErrorBagName { get; set; } = "errors";
        public string ValidationMessageCssClassName { get; set; } = HtmlHelper.ValidationMessageCssClassName;
        public string ValidationInputCssClassName { get; set; } = HtmlHelper.ValidationInputCssClassName;
        public bool RequireProtocolForUrls { get; set; } = true;
    }
}
