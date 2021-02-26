# MSBuild.SDK.SystemWeb

This MSBuild SDK is designed to allow for the easy creation and use of SDK (shortform) projects targeting ASP.NET 4.x using System.Web.

## What SDKs are available

### MSBuild.SDK.SystemWeb

This is the basic SDK that enables Visual Studio 2019 to work with an ASP.Net 4.x based project using a short form project file.

## How can I use these SDKs?

When using an MSBuild Project SDK obtained via NuGet (such as the SDKs in this repo) a specific version **must** be specified.

Either append the version to the package name:

```xml
<Project Sdk="MSBuild.SDK.SystemWeb/1.0.0">
  ...
```

Or omit the version from the SDK attribute and specify it in the version in `global.json`, which can be useful to synchronise versions across multiple projects in a solution:

```json
{
  "msbuild-sdks": {
    "MSBuild.SDK.SystemWeb" : "1.0.0"
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
    <TargetFramework>net46</TargetFramework>
  </PropertyGroup>
</Project>
```

At evaluation time, MSBuild adds implicit imports at the top and bottom of the project like this:

```xml
<Project Sdk="MSBuild.SDK.SystemWeb">
  <Import Project="Sdk.props" Sdk="Microsoft.NET.Sdk" />

  <PropertyGroup>
    <TargetFramework>net46</TargetFramework>
  </PropertyGroup>

  <Import Project="Sdk.targets" Sdk="Microsoft.NET.Sdk" />
</Project>
```
