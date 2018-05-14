# VeeValidate.AspNetCore

Replaces the jquery validation attributes that are rendered by the taghelpers and html extension methods with the vee-validate equivalents.
There is no vee-validate rule for phone number so this has not been ported.

### Configuration
Startup.cs
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddVeeValidation();
    services.AddMvc();
}
```

### Options

| Name       | Description | Default Value |
|:-----------|:------------|:--------------|
|ErrorBagName|             | "errors"      |
|ValidationMessageCssClassName|             | ""      |
|ValidationSummaryCssClassName|             | ""      |
|ValidationInputCssClassName|             | ""      |
|Dates.Format|             | ""      |
|Urls.RequireProtocol|             | true      |

### Examples
```html
<span asp-message-for="Property"></span>
```

### NOTES
Input types should be text for validation to work correctly. If type="number" then numeric validation won't work. Need to check other types.