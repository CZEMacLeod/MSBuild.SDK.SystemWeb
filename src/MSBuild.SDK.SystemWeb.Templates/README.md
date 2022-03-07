# MSBuild.SDK.SystemWeb.Templates

[![Build Status](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_apis/build/status/CZEMacLeod.MSBuild.SDK.SystemWeb?branchName=main)](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_build/latest?definitionId=69&branchName=main)
[![NuGet package](https://img.shields.io/nuget/v/MSBuild.SDK.SystemWeb.Templates.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb.Templates)
[![NuGet downloads](https://img.shields.io/nuget/dt/MSBuild.SDK.SystemWeb.Templates.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb.Templates)

## Installation

`dotnet new -i MSBuild.SDK.SystemWeb.Templates`

## Usage

### CLI
`dotnet new systemweb`
or
`dotnet new systemwebfull`

To select the VB.Net version use the flag `-lang VB`
e.g. `dotnet new systemweb -lang VB`

### Visual Studio

Alternatively use the Visual Studio Add Project dialog.
#### 2019
You need to have enabled the Preview feature to show [.NET CLI Templates in Visual Studio](https://devblogs.microsoft.com/dotnet/net-cli-templates-in-visual-studio/) and have Visual Studio 16.8 Preview 2 or higher.

#### 2022
The ability to use templates is built into VS2022.

![Visual Studio New Project Dialog](https://raw.githubusercontent.com/CZEMacLeod/MSBuild.SDK.SystemWeb/main/src/MSBuild.SDK.SystemWeb.Templates/images/create-new-project.png)

You can find the new templates easily by selecting System.Web from the Project Type dropdown.

## Documentation

For more information see 

[![Source](https://img.shields.io/badge/github-source-lightgrey?logo=github)](https://github.com/CZEMacLeod/MSBuild.SDK.SystemWeb)
[![Docs](https://img.shields.io/badge/github_pages-docs-lightgrey?logo=github)](https://czemacleod.github.io/MSBuild.SDK.SystemWeb/)
