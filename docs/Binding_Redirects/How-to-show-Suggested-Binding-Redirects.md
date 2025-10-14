# How to show Suggested Binding Redirects

The following may be useful if you need to see generated binding redirects.
e.g. if you want to manually add them to your `web.config`

Add this into your csproj
```xml
  <PropertyGroup>
    <GeneratedBindingRedirectsAction>Preview</GeneratedBindingRedirectsAction>
  </PropertyGroup>
```

And then look for a `Web.BindingRedirects.config` file in your project's Solution Explorer.

# Razor Views
If you set `GeneratedBindingRedirectsAction` to `Preview` then any MVC Views related `web.config` files, such as `View\web.config` or `Areas\*\Views\web.config`, or any file marked as `RazorAppConfigFiles` will also create a preview file next to it named `web.BindingRedirects.config`.

