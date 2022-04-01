# MSBuild.SDK.SystemWeb.Templates

[![Build Status](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_apis/build/status/CZEMacLeod.MSBuild.SDK.SystemWeb?branchName=main)](https://dev.azure.com/flexviews/MSBuild.SDKs.SystemWeb/_build/latest?definitionId=69&branchName=main)
[![NuGet package](https://img.shields.io/nuget/v/MSBuild.SDK.SystemWeb.Templates.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)
[![NuGet downloads](https://img.shields.io/nuget/dt/MSBuild.SDK.SystemWeb.Templates.svg)](https://nuget.org/packages/MSBuild.SDK.SystemWeb)

## Installation

```cmd 
dotnet new -i MSBuild.SDK.SystemWeb.Templates
```

## Updating

Optionally
```cmd
dotnet new --update-check
```
and
```cmd
dotnet new --update-apply
```
**N.B.** This applies to all installed templates.
```cmd 
dotnet new -i MSBuild.SDK.SystemWeb.Templates
```
Should update you to the latest version even if you have them already installed.

## Usage

### CLI
```cmd
dotnet new systemweb
```
or
```cmd
dotnet new systemwebfull
```

To select the VB.Net version use the flag `-lang VB`
e.g. 
```cmd
dotnet new systemweb -lang VB
```

#### Web Applications
```cmd
dotnet new systemweb
```
or
```cmd
dotnet new systemwebfull
```

To select the VB.Net version use the flag `-lang VB`
e.g. 
```cmd
dotnet new systemweb -lang VB
```

#### Razor Class Libraries
```cmd
dotnet new systemwebrazorlib
```

To select the VB.Net version use the flag `-lang VB`
e.g.
```cmd
dotnet new systemwebrazorlib -lang VB
```


### Visual Studio 2019
Alternatively use the Visual Studio Add Project dialog.
You need to have enabled the Preview feature to show [.NET CLI Templates in Visual Studio](https://devblogs.microsoft.com/dotnet/net-cli-templates-in-visual-studio/) and have Visual Studio 16.8 Preview 2 or higher.

![Visual Studio New Project Dialog](https://raw.githubusercontent.com/CZEMacLeod/MSBuild.SDK.SystemWeb/main/src/MSBuild.SDK.SystemWeb.Templates/images/create-new-project.png)

You can find the new templates easily by selecting `System.Web` from the Project Type dropdown.
