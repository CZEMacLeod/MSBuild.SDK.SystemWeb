# MSBuild.SDK.SystemWeb

[![Build Status](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_apis/build/status/CZEMacLeod.MSBuild.SDK.SystemWeb?branchName=main)](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_build/latest?definitionId=69&branchName=main)
[![NuGet package](https://img.shields.io/nuget/v/MSBuild.SDK.SystemWeb.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)
[![NuGet downloads](https://img.shields.io/nuget/dt/MSBuild.SDK.SystemWeb.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)

Based on the discussion and ideas in [Add support for ASP.NET (non-Core) projects](https://github.com/dotnet/project-system/issues/2670)

## How can I use this SDKs?

When using an MSBuild Project SDK obtained via NuGet (such as the SDKs in this repo) a specific version **must** be specified.

Either append the version (as shown in the nuget shield above) to the package name:

```xml
<Project Sdk="MSBuild.SDK.SystemWeb/4.0.xx">
  ...
```

Or omit the version from the SDK attribute and specify it in the version in `global.json`, which can be useful to synchronise versions across multiple projects in a solution:

```json
{
  "msbuild-sdks": {
    "MSBuild.SDK.SystemWeb" : "4.0.xx"
  }
}
```

You can also use the [templates](Templates.md) to easily create new projects.

Where `xx` is the latest release available on [nuget.org](https://nuget.org/packages/MSBuild.SDK.SystemWeb)

## Properties

### Common Properties

| Property | Default value | Description |
| -------- | ------------- | ----------- |
| `ExcludeSDKDefaultPackages` | false | Do not include the default packages `Microsoft.Net.Compilers.Toolset` and `Microsoft.CodeDom.Providers.DotNetCompilerPlatform` |
| `ApplySDKDefaultPackageVersions` | true* | Apply default version numbers to packages unless using a Central Package Management system |
| `GeneratedBindingRedirectsAction` | None | Set the desired default behavior of what to do with SuggestedBindingRedirects if not yet set.<br>See [automatically generated binding redirects](Binding_Redirects/Autogenerating-Binding-Redirects.md)<br><ul><li>`None` - Do nothing except show the warning</li><li>`Preview` - Creates new Web.BindingRedirects.config file showing proposed changes</li><li>`Overwrite` - Updates the $(AppConfig) aka web.config in the project root</li></ul> |

*Version numbers are not applied if you are using `Microsoft.Build.CentralPackageVersions` version 2.1.1 or higher. Remember to include the packages in your central package versions file.

### Specific Properties

| Property | Default value | Description |
| -------- | ------------- | ----------- |
| `MvcBuildViews` | true if Configuration is Release<br/>false otherwise | Whether to invoke the AspNetCompiler automatically after build |
| `EnableWebFormsDefaultItems` | Same as `EnableDefaultItems` | Whether to automatically include WebForms files as content<br><ul><li> *.asax</li><li> *.ascx</li><li> *.ashx</li><li> *.asmx</li><li> *.aspx</li><li> *.master</li><li> *.svc</li></ul> |
| `SetRoslynToolPath` | true if `RoslynToolPath` is empty | Sets `RoslynToolPath` to the version included in `Microsoft.Net.Compilers.Toolset` to allow compiling .aspx files etc. using a newer langversion** |

** You will have to adjust your web.config to change the `system.codedom/compilers/compiler` `compilerOptions` attribute to set the langversion - it won't automatically use the `LangVersion` property from your project file.

### Deprecated Properties
| Property | Default value | Description |
| -------- | ------------- | ----------- |
| `ExcludeASPNetCompilers` | false | Use `ExcludeSDKDefaultPackages` instead |
| `OverwriteAppConfigWithBindingRedirects` | false | If set, will set `GeneratedBindingRedirectsAction` to `Overwrite` then any  will be copied into your web.config file. |

## Automatic Default Packages

### Common Packages

| Package | Default Version | Property |
| ------- | --------------- | -------- |
| `Microsoft.Net.Compilers.Toolset` | 4.5.0 | `MicrosoftNetCompilersToolset_Version` |
| `Microsoft.CodeDom.Providers.DotNetCompilerPlatform` | 3.6.0 | `MicrosoftCodeDomProvidersDotNetCompilerPlatform_Version` |
