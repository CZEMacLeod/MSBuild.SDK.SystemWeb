# MSBuild.SDK.SystemWeb

[![Build Status](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_apis/build/status/CZEMacLeod.MSBuild.SDK.SystemWeb?branchName=main)](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_build/latest?definitionId=69&branchName=main)
[![NuGet package](https://img.shields.io/nuget/v/MSBuild.SDK.SystemWeb.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)
[![NuGet downloads](https://img.shields.io/nuget/dt/MSBuild.SDK.SystemWeb.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)

Based on the discussion and ideas in [Add support for ASP.NET (non-Core) projects](https://github.com/dotnet/project-system/issues/2670)

## How can I use this SDKs?

When using an MSBuild Project SDK obtained via NuGet (such as the SDKs in this repo) a specific version **must** be specified.

Either append the version (as shown in the nuget shield above) to the package name:

```xml
<Project Sdk="MSBuild.SDK.SystemWeb/4.0.69">
  ...
```

Or omit the version from the SDK attribute and specify it in the version in `global.json`, which can be useful to synchronise versions across multiple projects in a solution:

```json
{
  "msbuild-sdks": {
    "MSBuild.SDK.SystemWeb" : "4.0.69"
  }
}
```

You can also use the [templates](Templates.md) to easily create new projects.

## Properties

| Property | Default value | Description |
| - | - |
| ExcludeASPNetCompilers | false | Do not include the default packages `Microsoft.Net.Compilers.Toolset` and `Microsoft.CodeDom.Providers.DotNetCompilerPlatform` |
| MicrosoftNetCompilersToolset_Version | 3.8.0 | Version number of the package `Microsoft.Net.Compilers.Toolset` to include* |
| MicrosoftCodeDomProvidersDotNetCompilerPlatform_Version | 3.6.0 | Version number of the packge `Microsoft.CodeDom.Providers.DotNetCompilerPlatform` to include* |
| OverwriteAppConfigWithBindingRedirects | false | If set, then any [automatically generated binding redirects](Binding_Redirects/Autogenerating-Binding-Redirects.md) will be copied into your web.config file. |
| MvcBuildViews | true if Configuration is Release<br/>false otherwise | Whether to invoke the AspNetCompiler automatically after build |
| EnableWebFormsDefaultItems | Same as `EnableDefaultItems` | Whether to automatically include WebForms files as content<br><ul><li> *.asax</li><li> *.ascx</li><li> *.ashx</li><li> *.asmx</li><li> *.aspx</li><li> *.master</li><li> *.svc</li></ul> |

*Version numbers are not applied if you are using `Microsoft.Build.CentralPackageVersions` version 2.1.1 or higher. Remember to include the packages in your central package versions file.