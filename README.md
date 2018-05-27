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

| Name                          | Description                                   | Default Value |
|:------------------------------|:----------------------------------------------|:--------------|
| ErrorBagName                  | Vee Validate ErrorBag Name.                   | "errors"      |
| FieldBagName                  | Vee Validate FieldBag Name.                   | "fields"      |
| UseVeeValidateHtmlGenerator   | The VeeValidateHtmlGenerator will only show VeeValidate messages the asp-message-for and asp-validation-summary tag helpers. | true |
| ValidationMessageCssClassName | Css class added to field validation messages. | HtmlHelper.ValidationMessageCssClassName ("field-validation-error") |
| ValidationSummaryCssClassName | Css class added to the validation summary.    | HtmlHelper.ValidationSummaryCssClassName ("validation-summary-errors") |
| ValidationInputCssClassName   | Css class added to invalid fields.            | HtmlHelper.ValidationInputCssClassName ("input-validation-error") |
| Dates.Format                  | Expected date format in [date-fns](https://date-fns.org/v2.0.0-alpha.7/docs/format) format. | CurrentCulture.DateTimeFormat.ShortDatePattern |

### Rules
The table below shows the default validation attributes created by .NET and the v-validate replacements:

| Default Validation Attribute                 | Vee Validate Rule             |
|:---------------------------------------------|:------------------------------|
| data-val-equalto-other="*.OtherPropertyName" | confirmed:'OtherPropertyName' |
| data-val-creditcard="ErrorMessage"           | credit_card:true              |
| data-val-email="ErrorMessage"                | email:true                    |
| data-val-fileextensions-extensions="png,gif" | ext:['png','gif']             |
| data-val-maxlength-max="6"                   | max:6                         |
| data-val-length-max="6"                      | max:6                         |
| data-val-minlength-min="2"                   | min:2                         |
| data-val-length-min="2"                      | min:2                         |
| data-val-range-max="20"                      | max_value:'20'                |
| data-val-range-max="01/01/2018"              | before:['01/01/2018',true]    |
| data-val-range-min="2"                       | min_value:'20'                |
| data-val-range-min="01/01/2018"              | after:['01/01/2018',true]     |
| data-val-regex-pattern="[a-zA-Z]"            | regex:/[a-zA-Z]/              |
| data-val-required="true"                     | required:true                 |
| data-val-url="ErrorMessage"                  | url:[true,true]               |
| data-val-number="ErrorMessage"               | numeric:true                  |
| data-val-number="ErrorMessage"               | decimal:true                  |
| data-val-date="ErrorMessage"                 | date_format:'&#123;Options.Dates.Format&#125;' |

> NOTE: The data-val-number and data-val-date attributes are only added if the input type is text. /
> The data-val-number rule will be converted to the 'decimal' rule if the field type is double, float, or decimal. The 'numeric' rule will be used for all other field types. /
> The date_format rule will be added regardless of whether the data-val-date attribute is present IF there are any date related rules on the field, i.e. 'before' or 'after'.

### Overrides
#### Html Attributes
Any rules specified in HTML will take priority over the generated rules. 
>NOTE: Rules need to be declared in object format, i.e. &#123;required:true&#125; if there are any generated rules.

#### IHtmlValidationAttributeAdapter
Existing attribute conversion can be overridden by implementing a custom IHtmlValidationAttributeAdapter with the same key as the provider you want to override (the jQuery validation attribute name).
The provider will need to be registered before adding the Vee-Validation services.
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<IHtmlValidationAttributeAdapter, CustomAttributeAdapter>();
    services.AddVeeValidation();
    services.AddMvc();
}
```

### Tag Helpers
By default, the asp-message-for and asp-validation-summary tag helpers will be overridden to only show VeeValidate errors.

#### Examples
```html
<span asp-message-for="Property"></span>
<div asp-validation-summary="All"></div>
```
If the asp-validation-summary value is set to "ModelOnly" you'll also need to include the VeeValidateSnippets.ValidationSummaryMixin (after vee-validate.js reference).
```html
@Html.Raw(VeeValidateSnippets.ValidationSummaryMixin)
<div asp-validation-summary="ModelOnly"></div>
```

### Useful Resources
Vue JS - https://vuejs.org/ \
VeeValidate - https://baianat.github.io/vee-validate/