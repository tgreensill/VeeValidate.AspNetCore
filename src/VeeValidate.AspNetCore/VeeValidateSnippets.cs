using Microsoft.AspNetCore.Html;

namespace VeeValidate.AspNetCore
{
    public class VeeValidateSnippets
    {
        private readonly VeeValidateOptions _options;

        public VeeValidateSnippets(VeeValidateOptions options)
        {
            _options = options;
        }

        public IHtmlContent ValidationSummaryMixin => new HtmlString(
            @"<script>
            Vue.mixin({
                computed: {
                    validationSummaryErrors: function() {
                        var self = this;
                        return self." + _options.ErrorBagName + @".items.filter(function(e) {
                            return Object.keys(self." + _options.FieldBagName + @").indexOf(e.field) < 0;
                        }).map(function(o) {
                            return o.msg;
                        });
                    }
                }
            })
            </script>"
        );
    }
}
