using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class DateAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        private readonly string _dateFormat;

        public DateAttributeAdapter(VeeValidateOptions options)
        {
            _dateFormat = options.Dates.Format;
        }

        public string[] Keys => new [] { "data-val-date" };
        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            return $"date_format:'{_dateFormat}'";
        }
    }
}
