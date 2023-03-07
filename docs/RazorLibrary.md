# MSBuild.SDK.SystemWeb.RazorLibrary

[![Build Status](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_apis/build/status/CZEMacLeod.MSBuild.SDK.SystemWeb?branchName=main)](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_build/latest?definitionId=69&branchName=main)
[![NuGet package](https://img.shields.io/nuget/v/MSBuild.SDK.SystemWeb.RazorLibrary.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb.RazorLibrary)
[![NuGet downloads](https://img.shields.io/nuget/dt/MSBuild.SDK.SystemWeb.RazorLibrary.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb.RazorLibrary)

This project complements the MSBuild.SDK.SystemWeb SDK (based on the discussion and ideas in [Add support for ASP.NET (non-Core) projects](https://github.com/dotnet/project-system/issues/2670)) by adding an SDK project type for Razor Libraries.
This uses the [RazorGenerator](https://github.com/RazorGenerator/RazorGenerator) project to provide compile time support for MVC5 views with-in a library.

## How can I use this SDKs?

When using an MSBuild Project SDK obtained via NuGet (such as the SDKs in this repo) a specific version **must** be specified.

Either append the version to the package name:

```xml
<Project Sdk="MSBuild.SDK.SystemWeb.RazorLibrary/4.0.87">
  ...
```

Or omit the version from the SDK attribute and specify it in the version in `global.json`, which can be useful to synchronise versions across multiple projects in a solution:

```json
{
  "msbuild-sdks": {
    "MSBuild.SDK.SystemWeb.RazorLibrary" : "4.0.87"
  }
}
```

You can also use the [templates](../MSBuild.SDK.SystemWeb.Templates) to easily create new projects.

## Documentation

For more information see 

[![Source](https://img.shields.io/badge/github-source-lightgrey?logo=github)](https://github.com/CZEMacLeod/MSBuild.SDK.SystemWeb)
[![Docs](https://img.shields.io/badge/github_pages-docs-lightgrey?logo=github)](https://czemacleod.github.io/MSBuild.SDK.SystemWeb/)

## Properties

### Common Properties

| Property | Default value | Description |
| -------- | ------------- | ----------- |
| `ExcludeSDKDefaultPackages` | false | Do not include the default packages `Microsoft.Net.Compilers.Toolset` and `Microsoft.CodeDom.Providers.DotNetCompilerPlatform` |
| `ApplySDKDefaultPackageVersions` | true* | Apply default version numbers to packages unless using a Central Package Management system |

*Version numbers are not applied if you are using `Microsoft.Build.CentralPackageVersions` version 2.1.1 or higher. Remember to include the packages in your central package versions file.


### Specific Properties

| Property | Default value | Description |
| -------- | ------------- | ----------- |
| `MvcBuildViews` | true if Configuration is Release<br/>false otherwise | Whether to invoke the AspNetCompiler automatically after build |
| `EnableWebFormsDefaultItems` | Same as `EnableDefaultItems` | Whether to automatically include WebForms files as content<br><ul><li> *.asax</li><li> *.ascx</li><li> *.ashx</li><li> *.asmx</li><li> *.aspx</li><li> *.master</li><li> *.svc</li></ul> |
| `OverwriteAppConfigWithBindingRedirects` | false | If set, then any [automatically generated binding redirects](Binding_Redirects/Autogenerating-Binding-Redirects.md) will be copied into your web.config and `RazorAppConfigFiles` files. |

### Deprecated Properties
| Property | Default value | Description |
| -------- | ------------- | ----------- |
| `ExcludeDefaultRazorPackages` | false | Use `ExcludeSDKDefaultPackages` instead  |

## Automatic Default Packages

### Common Packages

| Package | Default Version | Property |
| ------- | --------------- | -------- |
| `Microsoft.Net.Compilers.Toolset` | 4.5.0 | `MicrosoftNetCompilersToolset_Version` |
| `Microsoft.CodeDom.Providers.DotNetCompilerPlatform` | 3.6.0 | `MicrosoftCodeDomProvidersDotNetCompilerPlatform_Version` |

### Specific Packages

| Package | Default Version | Property |
| ------- | --------------- | -------- |
| `RazorGenerator.MsBuild` | 2.5.0 | `RazorGeneratorMSBuild_Version` |
| `RazorGenerator.MVC` | 2.4.9 | `RazorGeneratorMVC_Version` |
| `Microsoft.AspNet.Mvc` | 5.2.9 | `MicrosoftAspNetMvc_Version` |

## Items

| Item Name | Default | Description |
| --------- | ------- | ----------- |
| `RazorAppConfigFiles` | `Views/web.config`<br>`Areas/**/web.config` | List of config files that will be updated with binding redirects |
