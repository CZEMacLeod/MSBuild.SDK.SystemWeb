# How to show Suggested Binding Redirects

The following may be useful if you need to see generated binding redirects.
e.g. if you want to manually add them to your `web.config`
```xml
<UsingTask TaskName="ShowBindingRedirects" TaskFactory="$(TaskFactory)" AssemblyFile="$(MSBuildToolsPath)\Microsoft.Build.Tasks.Core.dll">
    <ParameterGroup>
      <SuggestedBindingRedirects ParameterType="Microsoft.Build.Framework.ITaskItem[]" Required="true" />
    </ParameterGroup>
    <Task>
      <Using Namespace="System" />
      <Using Namespace="System.IO" />
      <Using Namespace="System.Reflection" />
      <Using Namespace="System.Xml" />
      <Using Namespace="Microsoft.Build.Framework" />
      <Code Type="Fragment" Language="cs">
        <![CDATA[
          StringBuilder sb = new StringBuilder();
          foreach(var sbr in SuggestedBindingRedirects) {
            var an = new AssemblyName(sbr.ItemSpec);
            var mvn = sbr.GetMetadata("MaxVersion");

            byte []pt = an.GetPublicKeyToken();
           
            sb.AppendLine("<assemblyBinding xmlns=\"urn:schemas-microsoft-com:asm.v1\">");
            sb.AppendLine("\t<dependentAssembly>");
            sb.Append("\t\t<assemblyIdentity name=\"");
            sb.Append(an.Name);
            sb.Append("\" publicKeyToken=\"");
            if (pt is null) {
              sb.Append("null");
            } else {
              for (int i=0;i<pt.GetLength(0);i++)
                sb.AppendFormat("{0:x2}", pt[i]);
            }
            sb.Append("\" culture=\"");
            sb.Append(an.CultureName);
            sb.AppendLine("\" />");
            sb.Append("\t\t<bindingRedirect oldVersion=\"0.0.0.0-");
            sb.Append(mvn);
            sb.Append("\" newVersion=\"");
            sb.Append(mvn);
            sb.AppendLine("\" />");
            sb.AppendLine("\t</dependentAssembly>");
            sb.AppendLine("</assemblyBinding>");
            }
         Log.LogMessage(MessageImportance.High,sb.ToString());
      ]]>
      </Code>
    </Task>
  </UsingTask>

  <Target Name="ShowBindingRedirects" AfterTargets="ResolveAssemblyReferences">
    <ShowBindingRedirects SuggestedBindingRedirects="@(SuggestedBindingRedirects)" Condition="'@(SuggestedBindingRedirects)'!=''" />
  </Target>
```