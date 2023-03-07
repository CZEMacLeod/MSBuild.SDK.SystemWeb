# MSBuild.SDK.SystemWeb

[![Build Status](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_apis/build/status/CZEMacLeod.MSBuild.SDK.SystemWeb?branchName=main)](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_build/latest?definitionId=69&branchName=main)
[![Docs](https://img.shields.io/badge/Documentation-docs-lightgrey?logo=github)](https://czemacleod.github.io/MSBuild.SDK.SystemWeb/)
[![pages-build-deployment](https://github.com/CZEMacLeod/MSBuild.SDK.SystemWeb/actions/workflows/pages/pages-build-deployment/badge.svg)](https://github.com/CZEMacLeod/MSBuild.SDK.SystemWeb/actions/workflows/pages/pages-build-deployment)

This MSBuild SDK is designed to allow for the easy creation and use of SDK (shortform) projects targeting ASP.NET 4.x using System.Web.

## What's available

### [MSBuild.SDK.SystemWeb](src/MSBuild.SDK.SystemWeb)

[![NuGet package](https://img.shields.io/nuget/v/MSBuild.SDK.SystemWeb.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)
[![NuGet downloads](https://img.shields.io/nuget/dt/MSBuild.SDK.SystemWeb.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)

This is the basic SDK that enables Visual Studio to work with an ASP.Net 4.x based project using a short form project file.

### [MSBuild.SDK.SystemWeb.Templates](src/MSBuild.SDK.SystemWeb.Templates)

[![NuGet package](https://img.shields.io/nuget/v/MSBuild.SDK.SystemWeb.Templates.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb.Templates)
[![NuGet downloads](https://img.shields.io/nuget/dt/MSBuild.SDK.SystemWeb.Templates.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb.Templates)

This is a set of templates that allow for the easy creation of projects based on the MSBuild.SDK.SystemWeb project SDK type.

### [MSBuild.SDK.SystemWeb.RazorLibrary](src/MSBuild.SDK.SystemWeb.RazorLibrary)

[![NuGet package](https://img.shields.io/nuget/v/MSBuild.SDK.SystemWeb.RazorLibrary.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb.Templates)
[![NuGet downloads](https://img.shields.io/nuget/dt/MSBuild.SDK.SystemWeb.RazorLibrary.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb.Templates)

This is an SDK that allows Visual Studio to work with an ASP.Net 4.x / MVC 5 based Razor Library.
This makes it easy to use the [RazorGenerator](https://github.com/RazorGenerator/RazorGenerator) system with an SDK type project.

## Read The Docs

### [Documentation](https://czemacleod.github.io/MSBuild.SDK.SystemWeb/)

## How can I use these SDKs?

When using an MSBuild Project SDK obtained via NuGet (such as the SDKs in this repo) a specific version **must** be specified.

Either append the version to the package name:

```xml
<Project Sdk="MSBuild.SDK.SystemWeb/4.0.82">
  ...
```

Or omit the version from the SDK attribute and specify it in the version in `global.json`, which can be useful to synchronise versions across multiple projects in a solution:

```json
{
  "msbuild-sdks": {
    "MSBuild.SDK.SystemWeb" : "4.0.82"
  }
}
```

Since MSBuild 15.6, SDKs are downloaded as NuGet packages automatically. Earlier versions of MSBuild 15 required SDKs to be installed. 

For more information, [read the documentation](https://docs.microsoft.com/visualstudio/msbuild/how-to-use-project-sdk).

## What are MSBuild SDKS?
MSBuild 15.0 introduced new project XML for .NET Core that we refer to as SDK-style.  These SDK-style projects looks like:

```xml
<Project Sdk="Microsoft.NET.SystemWeb">
  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
  </PropertyGroup>
</Project>
```

At evaluation time, MSBuild adds implicit imports at the top and bottom of the project like this:

```xml
<Project>
  <Import Project="Sdk.props" Sdk="Microsoft.NET.SystemWeb" />

  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
  </PropertyGroup>

  <Import Project="Sdk.targets" Sdk="Microsoft.NET.SystemWeb" />
</Project>
```
