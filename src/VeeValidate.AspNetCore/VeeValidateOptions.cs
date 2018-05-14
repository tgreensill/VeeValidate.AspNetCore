using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Globalization;

namespace VeeValidate.AspNetCore
{
    public class VeeValidateOptions
    {       
        public string ErrorBagName { get; set; } = "errors";
        public string ValidationInputCssClassName { get; set; } = HtmlHelper.ValidationInputCssClassName;
        public string ValidationMessageCssClassName { get; set; } = HtmlHelper.ValidationMessageCssClassName;        
        public string ValidationSummaryCssClassName { get; set; } = HtmlHelper.ValidationSummaryCssClassName;
        public DateValidationOptions Dates { get; set; } = new DateValidationOptions();
        //public bool DisableHtml5FormValidation { get; set; } = true;
        // TODO - Add action feature for adding attributes to fields that are being validated
        //      - i.e. options.Something = attributes => attributes.Add("v-errors");
        //      - i.e. options.Something = attributes => attributes.Add(":class={error:true}");
    }

    public class DateValidationOptions
    {        
        /// <summary>
        /// Date format in date-fns format, see https://date-fns.org/v2.0.0-alpha.7/docs/format
        /// </summary>
        public string Format { get; set; } = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern.ToUpper();
    }
}
