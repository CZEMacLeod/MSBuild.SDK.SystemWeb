# MSBuild.SDK.SystemWeb.Templates

## Installation

`dotnet new -i MSBuild.SDK.SystemWeb.Templates --nuget-source https://nuget.pkg.github.com/czemacleod/index.json`

## Usage

### CLI
`dotnet new systemweb`
or
`dotnet new systemwebfull`

To select the VB.Net version use the flag `-lang VB`
e.g. `dotnet new systemweb -land VB`

### Visual Studio 2019
Alternatively use the Visual Studio Add Project dialog.
You need to have enabled the Preview feature to show [.NET CLI Templates in Visual Studio](https://devblogs.microsoft.com/dotnet/net-cli-templates-in-visual-studio/) and have Visual Studio 16.8 Preview 2 or higher.

![Visual Studio New Project Dialog](images/create-new-project.png)

You can find the new templates easily by selecting System.Web from the Project Type dropdown.
