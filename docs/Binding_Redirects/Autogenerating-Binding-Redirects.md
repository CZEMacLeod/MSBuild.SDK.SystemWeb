# Autogenerating Binding Redirects

The template will, on compile, generate a copy of the current `web.config` with any required binding redirects added.
This file is written to the `bin` folder with the name `AssemblyName.dll.config` where `AssemblyName` is the name of your assembly.

You can set the `AssemblyName` by adding 
```xml
<AssemblyName>MyAssemblyName</AssemblyName>
```
to your project file as normal.

If you get the error `Warning MSB3276 Found conflicts between different versions of the same dependent assembly. Please set the "AutoGenerateBindingRedirects" property to true in the project file. For more information, see http://go.microsoft.com/fwlink/?LinkId=294190. <project name here>`, then you can manually copy the 
```xml
<runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
        ...
    </assemblyBinding>
</runtime>
```
section from the generated `.dll.config` file to your web.config file, replacing the existing `assemblyBinding` node.

Alternatively, since the `.dll.config` file is based on your `web.config` file, you can just overwrite your `web.config` file with the `.dll.config` file.

If you want this to happen automatically, you can add the following to your project file.
```xml
  <PropertyGroup>
    <OverwriteAppConfigWithBindingRedirects>true</OverwriteAppConfigWithBindingRedirects>
  </PropertyGroup>
```

This enables the following target in the SDK which will overwrite your web.config from the patched version if there are any changes.

```xml
  <Target Name="UpdateConfigWithBindingRedirects" AfterTargets="AfterBuild" Condition="'$(OverwriteAppConfigWithBindingRedirects)'=='true'">
    <ItemGroup>
      <_DllConfig Remove="@(_DllConfig)" />
      <_AppConfig Remove="@(_AppConfig)" />
      <_ConfigFile Remove="@(_ConfigFileHash)" />
      <_DllConfig Include="$(OutDir)$(AssemblyName).dll.config" />
      <_AppConfig Include="web.config" />
    </ItemGroup>
    <GetFileHash Files="@(_DllConfig)">
      <Output TaskParameter="Hash" PropertyName="_DllConfigHash" />
      <Output TaskParameter="Items" ItemName="_DllConfigFileHash" />
    </GetFileHash>
    <GetFileHash Files="@(_AppConfig)">
      <Output TaskParameter="Hash" PropertyName="_AppConfigHash" />
      <Output TaskParameter="Items" ItemName="_AppConfigFileHash" />
    </GetFileHash>
    <ItemGroup>
      <_ConfigFileHash Include="@(_DllConfigFileHash)" />
      <_ConfigFileHash Include="@(_AppConfigFileHash)" />
    </ItemGroup>
    <Message Text="%(_ConfigFileHash.Identity): %(_ConfigFileHash.FileHash)" />
    <Warning Text="Replacing web.config due to changes during compile - This should clear warning MSB3276 on next compile" File="web.config" Condition="'$(_DllConfigHash)'!='$(_AppConfigHash)'" />
    <Copy SourceFiles="$(OutDir)$(AssemblyName).dll.config" DestinationFiles="web.config" Condition="'$(_DllConfigHash)'!='$(_AppConfigHash)'" />
  </Target>
```