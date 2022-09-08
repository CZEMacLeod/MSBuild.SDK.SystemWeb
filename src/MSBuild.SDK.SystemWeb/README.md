# MSBuild.SDK.SystemWeb

[![Build Status](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_apis/build/status/CZEMacLeod.MSBuild.SDK.SystemWeb?branchName=main)](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_build/latest?definitionId=69&branchName=main)
[![NuGet package](https://img.shields.io/nuget/v/MSBuild.SDK.SystemWeb.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)
[![NuGet downloads](https://img.shields.io/nuget/dt/MSBuild.SDK.SystemWeb.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)

Based on the discussion and ideas in [Add support for ASP.NET (non-Core) projects](https://github.com/dotnet/project-system/issues/2670)

## How can I use this SDKs?

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

You can also use the [templates](../MSBuild.SDK.SystemWeb.Templates) to easily create new projects.

## Documentation

For more information see 

[![Source](https://img.shields.io/badge/github-source-lightgrey?logo=github)](https://github.com/CZEMacLeod/MSBuild.SDK.SystemWeb)
[![Docs](https://img.shields.io/badge/github_pages-docs-lightgrey?logo=github)](https://czemacleod.github.io/MSBuild.SDK.SystemWeb/)
