# MSBuild.SDK.SystemWeb

[![Build Status](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_apis/build/status/CZEMacLeod.MSBuild.SDK.SystemWeb?branchName=main)](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_build/latest?definitionId=69&branchName=main)
[![Source](https://img.shields.io/badge/github-source-lightgrey?logo=github)](https://github.com/CZEMacLeod/MSBuild.SDK.SystemWeb)

This MSBuild SDK is designed to allow for the easy creation and use of SDK (shortform) projects targeting ASP.NET 4.x using System.Web.

## What's available

### [MSBuild.SDK.SystemWeb](SDK.md)

[![NuGet package](https://img.shields.io/nuget/v/MSBuild.SDK.SystemWeb.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)
[![NuGet downloads](https://img.shields.io/nuget/dt/MSBuild.SDK.SystemWeb.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)

This is the basic SDK that enables Visual Studio 2019 to work with an ASP.Net 4.x based project using a short form project file.

### [MSBuild.SDK.SystemWeb.Templates](Templates.md)

[![NuGet package](https://img.shields.io/nuget/v/MSBuild.SDK.SystemWeb.Templates.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)
[![NuGet downloads](https://img.shields.io/nuget/dt/MSBuild.SDK.SystemWeb.Templates.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)

This is a set of templates that allow for the easy creation of projects based on the MSBuild.SDK.SystemWeb project SDK type.

## How can I use these SDKs?

When using an MSBuild Project SDK obtained via NuGet (such as the SDKs in this repo) a specific version **must** be specified.

Either append the version to the package name:

```xml
<Project Sdk="MSBuild.SDK.SystemWeb/4.0.33">
  ...
```

Or omit the version from the SDK attribute and specify it in the version in `global.json`, which can be useful to synchronise versions across multiple projects in a solution:

```json
{
  "msbuild-sdks": {
    "MSBuild.SDK.SystemWeb" : "4.0.33"
  }
}
```

Since MSBuild 15.6, SDKs are downloaded as NuGet packages automatically. Earlier versions of MSBuild 15 required SDKs to be installed. 

For more information, [read the documentation](https://docs.microsoft.com/visualstudio/msbuild/how-to-use-project-sdk).

## What are MSBuild SDKS?
MSBuild 15.0 introduced new project XML for .NET Core that we refer to as SDK-style.  These SDK-style projects looks like:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
  </PropertyGroup>
</Project>
```

At evaluation time, MSBuild adds implicit imports at the top and bottom of the project like this:

```xml
<Project Sdk="MSBuild.SDK.SystemWeb">
  <Import Project="Sdk.props" Sdk="Microsoft.NET.Sdk" />

  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
  </PropertyGroup>

  <Import Project="Sdk.targets" Sdk="Microsoft.NET.Sdk" />
</Project>
```

# Useful Information

## Binding Redirects
- [How to show Suggested Binding Redirects](Binding_Redirects/How-to-show-Suggested-Binding-Redirects.md)
- [Autogenerating Binding Redirects](Binding_Redirects/Autogenerating-Binding-Redirects.md)

## Known Limitations
- [![GitHub issues by-label](https://img.shields.io/github/issues/CZEMacLeod/MSBuild.SDK.SystemWeb/known%20limitation?label=known%20limitations)](https://github.com/CZEMacLeod/MSBuild.SDK.SystemWeb/issues?q=is%3Aissue+label%3A%22known+limitation%22+is%3Aopen)
- [![GitHub issues by-label](https://img.shields.io/github/issues-closed/CZEMacLeod/MSBuild.SDK.SystemWeb/known%20limitation?label=known%20limitations)](https://github.com/CZEMacLeod/MSBuild.SDK.SystemWeb/issues?q=is%3Aissue+label%3A%22known+limitation%22+is%3Aclosed)
- [Projects don't work with dotnet CLI tooling](https://github.com/CZEMacLeod/MSBuild.SDK.SystemWeb/issues/1)
- [Docker Containers](https://github.com/CZEMacLeod/MSBuild.SDK.SystemWeb/issues/9)
- [WebForms](https://github.com/CZEMacLeod/MSBuild.SDK.SystemWeb/issues/11)
- [VS Publish Command](https://github.com/CZEMacLeod/MSBuild.SDK.SystemWeb/issues/12)

## Templates
- [How to install and use the templates](Templates.md)