# Diagnostics (message IDs)

Messages emitted by `MSBuild.SDK.SystemWeb` use the `SYSWEB` prefix followed by a number.
This page is the central registry of those messages.

| ID | Severity | Meaning |
|----|----------|---------|
| `SYSWEB000` | Error | Invalid value for the `MissingWebApplicationsTargetPathAction` property (expected `Ignore`, `Skip`, `Warn` or `Error`). |
| `SYSWEB001` | Error / Warning / message | `Microsoft.WebApplication.targets` could not be resolved at `$(WebApplicationsTargetPath)`. Configurable via the `MissingWebApplicationsTargetPathAction` property — see below. |

## `MissingWebApplicationsTargetPathAction`

`Microsoft.WebApplication.targets` ships with Visual Studio / the Visual Studio Build Tools, not with
the .NET SDK. When building with the `dotnet` CLI (Core MSBuild), `$(MSBuildExtensionsPath32)` points at
the .NET SDK rather than a Visual Studio install, so the default `$(WebApplicationsTargetPath)` may not
exist and the build would otherwise fail with a cryptic `MSB4019`.

The `MissingWebApplicationsTargetPathAction` property controls what happens when that file cannot be found:

| Value | Behaviour |
|-------|-----------|
| `Ignore` | Do nothing. |
| `Skip` | Emit a high-importance informational message (`SYSWEB001`). |
| `Warn` | Emit a warning (`SYSWEB001`). |
| `Error` | Emit an error and fail the build (`SYSWEB001`). **Default.** |

> With `Ignore`, `Skip` or `Warn` the build continues **without** importing `Microsoft.WebApplication.targets`.
> For a real web project, later targets that depend on it may still fail. These options are intended for
> scenarios where the web targets are genuinely not needed.

### How to resolve a missing `Microsoft.WebApplication.targets`

- Install Visual Studio, or the Visual Studio Build Tools, with the **web development build tools** workload.
- Build from a **Visual Studio Developer command prompt** (which sets the correct environment).
- Set `VSToolsPath` (or `WebApplicationsTargetPath`) explicitly, e.g. computed via
  [vswhere](https://github.com/microsoft/vswhere) in a CI pre-build step:

```xml
<!-- e.g. in Directory.Build.props, or pass via -p: on the command line -->
<PropertyGroup>
  <VSToolsPath>C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Microsoft\VisualStudio\v17.0</VSToolsPath>
</PropertyGroup>
```

To opt out of the failure instead:

```xml
<PropertyGroup>
  <MissingWebApplicationsTargetPathAction>Warn</MissingWebApplicationsTargetPathAction>
</PropertyGroup>
```
