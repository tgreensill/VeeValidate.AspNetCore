# VeeValidate.AspNetCore

[![Build status](https://ci.appveyor.com/api/projects/status/5fiom3ed16dtvxdo/branch/master?svg=true)](https://ci.appveyor.com/project/tgreensill/veevalidate-aspnetcore/branch/master)

This is a simple library for replacing the default jquery validation attributes that are rendered by ASP.NET with a VeeValidate v-validate attribute.
> NOTE: There is no VeeValidate rule for phone number so this has not been ported.

### Usage
To use VeeValidate in place of JQuery validation, use the AddVeeValidation() extension in the ConfigureServices method of your startup.cs file.
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddVeeValidation();
    services.AddMvc();
}
```
Reference Vue JS, VeeValidate, and a polyfill for promises if you plan on supporting older browsers:
```html
<script src="https://cdnjs.cloudflare.com/ajax/libs/vue/2.5.16/vue.js" integrity="sha256-CMMTrj5gGwOAXBeFi7kNokqowkzbeL8ydAJy39ewjkQ=" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/vee-validate/2.0.9/vee-validate.js" integrity="sha256-t+DWUFe1/1QjOFx+LxCzWqka3Vq1ZBGJJvnNSq5XHVU=" crossorigin="anonymous"></script>
<script asp-fallback-test="window.Promise"
        asp-fallback-src="https://cdnjs.cloudflare.com/ajax/libs/es6-promise/4.1.1/es6-promise.auto.js">
</script>
```

### Options

| Name                          | Description                | Default Value |
|:------------------------------|:---------------------------|:--------------|
| ErrorBagName                  | Vee Validate ErrorBag Name | "errors"      |
| FieldBagName                  | Vee Validate FieldBag Name | "fields"      |
| ReplaceValidationTagHelpers   | Replaces the asp-message-for and asp-validation-summary tag helpers | true |
| ValidationMessageCssClassName | Css class added to field validation messages | HtmlHelper.ValidationMessageCssClassName ("field-validation-error") |
| ValidationSummaryCssClassName | Css class added to the validation summary | HtmlHelper.ValidationSummaryCssClassName ("validation-summary-errors") |
| ValidationInputCssClassName   | Css class added to invalid fields | HtmlHelper.ValidationInputCssClassName ("input-validation-error") |
| Dates.Format                  | Expected date format in [date-fns](https://date-fns.org/v2.0.0-alpha.7/docs/format) format | CurrentCulture.DateTimeFormat.ShortDatePattern |

### Rules
The table below shows the default validation attributes created by .NET and the v-validate replacements:

| Default Validation Attribute                   | Vee Validate Rule             | Notes |
|:-----------------------------------------------|:------------------------------|:------|
| data-val-equalto-other="*.OtherPropertyName" | confirmed:&#123;OtherPropertyName&#125; | The OtherPropertyName must exist in the vue $data |
| data-val-creditcard="ErrorMessage"           | credit_card:true             |       |
| data-val-email="ErrorMessage"                | email:true                    |       |

In addition to the above
| Property Type | Vee Validate Rule |
|:--------------|:-----------------:|
| int           | numeric:true      |
| short         | numeric:true      |
| long          | numeric:true      |
| float         | decimal:true      |
| double        | decimal:true      |
| decimal       | decimal:true      |
| datetime      | date_format:'&#123;Options.Dates.Format&#125;'|

> NOTE: Type validation can be buggy when working with HTML5 types. The quick workaround is to set the type attribute to "text".

### Overrides
#### Html Attributes

#### IHtmlValidationAttributeAdapter

### Tag Helpers
By default, the asp-message-for and asp-validation-summary tag helpers will be overridden to only show VeeValidate errors.
If the asp-validation-summary value is set to "ModelOnly" you'll also need to include the VeeValidateSnippets.ValidationSummaryMixin (after vee-validate.js reference).

#### Examples
```html
<span asp-message-for="Property"></span>
```
```html
@Html.Raw(VeeValidateSnippets.ValidationSummaryMixin)
<div asp-validation-summary="ModelOnly"></div>
```

### Useful Resources
Vue JS - https://vuejs.org/ \
VeeValidate - https://baianat.github.io/vee-validate/