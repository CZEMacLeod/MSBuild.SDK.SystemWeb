# Autogenerating Binding Redirects

The template will, on compile, discover any required binding redirects.

You may see a build warning `Warning MSB3276 Found conflicts between different versions of the same dependent assembly. Please set the "AutoGenerateBindingRedirects" property to true in the project file. For more information, see http://go.microsoft.com/fwlink/?LinkId=294190.`, and one of three possible actions can be taken with these discovered Binding Redirections

## None
If you Would like No Action to be taken with these suggestions, set the `GeneratedBindingRedirectionsAction` to `None` in your project file.
```xml
  <PropertyGroup>
    <GeneratedBindingRedirectsAction>None</GeneratedBindingRedirectsAction>
  </PropertyGroup>
```

## Preview
You can also choose to `Preview` and generate a file `Web.BindingRedirects.config` that contains the suggested Binding Redirects.
```xml
  <PropertyGroup>
    <GeneratedBindingRedirectsAction>Preview</GeneratedBindingRedirectsAction>
  </PropertyGroup>
```

## Overwrite
Alternatively the suggested Binding Redirects can be written directly to the `web.config`.
If you want this to happen automatically, you can add the following to your project file.
```xml
  <PropertyGroup>
    <GeneratedBindingRedirectsAction>Overwrite</GeneratedBindingRedirectsAction>
  </PropertyGroup>
```

# Razor Views
MVC Views folders often contain a customized version of `web.config`, including a `system.web.webPages.razor` section and may include other settings to prevent serving files in the folder using the `BlockViewHandler`.

Since views may reference models or components from assemblies which have/require binding redirects, the redirects from the main `web.config` should normally be copied to these files too.

To facilitate this scenario, a new item group is added, and any `web.config` files in Views or Areas\*\Views folders are marked as `RazorAppConfigFiles`.

The same rules and property (`GeneratedBindingRedirectsAction`) that affects the main `web.config` will also update these files.

This applies to both the main SDK and the RazorLibrary SDK.