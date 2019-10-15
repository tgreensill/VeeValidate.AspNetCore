# VeeValidate.AspNetCore

[![Build status](https://ci.appveyor.com/api/projects/status/5fiom3ed16dtvxdo/branch/master?svg=true)](https://ci.appveyor.com/project/tgreensill/veevalidate-aspnetcore/branch/master)

This is a simple library for replacing jquery validation as the client side validation library, with VeeValidate.

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
Setup VeeValidate:
```html
<script>
    Vue.use(VeeValidate);     
</script>
```
### Options

| Name                          | Type                      | Description                                   | Default Value |
|:------------------------------|:--------------------------|:----------------------------------------------|:--------------|
| ErrorBagName                  | String                    | Vee Validate ErrorBag Name.                   | "errors"      |
| FieldBagName                  | String                    | Vee Validate FieldBag Name.                   | "fields"      |
| ValidationMessageCssClassName | String                    | Css class added to field validation messages. | HtmlHelper.ValidationMessageCssClassName ("field-validation-error") |
| ValidationSummaryCssClassName | String                    | Css class added to the validation summary.    | HtmlHelper.ValidationSummaryCssClassName ("validation-summary-errors") |
| ValidationInputCssClassName   | String                    | Css class added to invalid fields.            | HtmlHelper.ValidationInputCssClassName ("input-validation-error") |
| OverrideValidationTagHelpers  | Boolean                   | If true, overrides the behaviour of the asp-validation-for and asp-validation-summary tag helpers to work with VeeValidate | true |
| AddValidationInputCssToFieldsWithoutValidation | Boolean  | If true, and OverrideValidationTagHelpers is true, the validation css binding will be added to a field that doesn't have validation. Set to true if you intend on manually adding errors to the error bag. | false |
| DatesFormatProvider           | Func<HttpContext, string> | Function returning the expected date format in [date-fns](https://date-fns.org/v2.0.0-alpha.7/docs/format) format. | ctx => CurrentCulture.DateTimeFormat.ShortDatePattern.ToUpper() |

### .NET Data Annotation Validation
The table below shows the VeeValidate rules generated for each of the .NET validation attributes:

| Validation Attribute  | VeeValidate Rules         |
|:----------------------|:--------------------------|
| [Compare]             | confirmed                 |
| [CreditCard]          | credit_card               |
| [EmailAddress]        | email                     |
| [FileExtensions]      | ext                       |
| [MaxLength]           | max                       |
| [MinLength]           | min                       |
| [Phone]               | -                         |
| [Range]               | min_value, max_value      |
| [Range] (DateTime)    | date_format, date_between |
| [RegularExpression]   | regex                     |
| [Required]            | required                  |
| [StringLength]        | min, max                  |
| [Url]                 | url                       |

> NOTE: VeeValidate does not have a rule for phone numbers so there is no client validation implemented for this yet.

### Fluent Validation
To use VeeValidate in place of JQuery validation, use the UseVeeValidate() extension on the FluentValidationMvcConfiguration class in the ConfigureServices method of your startup.cs file.\
Example:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddVeeValidation();
    services.AddMvc()
            .AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
                config.ConfigureClientsideValidation(options =>
                    options.UseVeeValidate()
                );
            });
}
```
The date formatter needs to be passed into both extensions when specified.\
Example:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    string DateFormatProvider(HttpContext ctx) => "dd/MM/yyyy";

    services.AddVeeValidation(options =>
    {        
        options.DateFormatProvider = DateFormatProvider;
    });
    services.AddMvc()
        .AddFluentValidation(config =>
        {
            config.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
            config.ConfigureClientsideValidation(options =>
                options.UseVeeValidate(DateFormatProvider)
            );
        });

}
```

### Fluent Validation Rules
The table below shows the VeeValidate rules generated for each of the FluentValidation rules:

| Validation Rule               | VeeValidate Rules         |
|:------------------------------|:--------------------------|
| EmailAddress                  | email                     |
| Equal                         | confirmed                 |
| GreaterThanOrEqualTo          | min_value                 |
| GreaterThanOrEqualTo (DateTime) | date_format, date_between |
| InclusiveBetween              | min_value, max_value      |
| InclusiveBetween (DateTime)   | date_format, date_between |
| Length                        | min, max                  |
| LessThanOrEqualTo             | max_value                 |
| LessThanOrEqualTo (DateTime)  | date_format, date_between |
| Matches                       | regex                     |
| MaximumLength                 | max                       |
| MinimumLength                 | min                       |
| NotEmpty                      | confirmed                 |
| NotNull                       | credit_card               |

### Overriding the generated rules
#### Html Attributes
Any rules specified in HTML will take priority over the generated rules. 
Rules need to be declared in object format if there are any generated rules for the field.\
Example:
```html
<input asp-for="RequiredField" v-validate="{required:isRequired}" />
```

### Tag Helpers
By default, the asp-message-for and asp-validation-summary tag helpers will be overridden to only show VeeValidate errors.
This can be disabled by setting the OverrideValidationTagHelpers option to false.\
Example:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddVeeValidation(options =>
    {        
        options.OverrideValidationTagHelpers = false; 
    });
    services.AddMvc();
}
```

Caveat: The asp-validation-summary with "ModelOnly" (and OverrideValidationTagHelpers is true) will not work correctly out of the box. 
To get this working you can include the VeeValidateSnippets.ValidationSummaryMixin (after vee-validate.js reference).\
Example:
```html
@inject VeeValidateSnippets VeeValidateSnippets
@Html.Raw(VeeValidateSnippets.ValidationSummaryMixin)
<div asp-validation-summary="ModelOnly"></div>
```

### Useful Resources
Vue JS - https://vuejs.org/ \
VeeValidate - http://vee-validate.logaretm.com/v2/