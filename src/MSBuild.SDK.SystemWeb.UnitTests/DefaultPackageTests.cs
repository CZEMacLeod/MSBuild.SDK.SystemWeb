using Microsoft.Build.Evaluation;
using TestingShared;
using Microsoft.Build.Utilities.ProjectCreation;
using Shouldly;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using System;

namespace MSBuild.SDK.SystemWeb.UnitTests
{
    public class DefaultPackageTests : MSBuildSdkTestBase
    {
        private readonly ITestOutputHelper output;

        private static readonly string ThisAssemblyDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public DefaultPackageTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData(".csproj")]
        //[InlineData(false, ".vbproj")]
        public void DefaultPacakges_WhenNoCentralManagement_VersionMetadataIsPresent(string extension)
        {
            string test0 = AppDomain.CurrentDomain.BaseDirectory;
            string test1 = Path.Combine(ThisAssemblyDirectory, @"TestableSdkComponents\Sdk");
            string test2 = Path.Combine(ThisAssemblyDirectory, @"TestHarnessInjectionSdk");

            ProjectCreator.Create()
                .Save(Path.Combine(TestRootPath, "Directory.Build.props"));

            ProjectCreator.Create()
                .Import(Path.Combine(ThisAssemblyDirectory, @"TestableSdkComponents\Sdk\MSBuild.SDK.SystemWeb.DefaultPackages.props"))
                .Save(Path.Combine(TestRootPath, "BeforeMS_NET_SDk_props.props"));

            ProjectCreator.Create()
                .Save(Path.Combine(TestRootPath, "AfterMS_NET_SDk_props.props"));


            ProjectCreator.Create()
                .Save(Path.Combine(TestRootPath, "Directory.Build.targets"));

            ProjectCreator.Create()
                .Import(Path.Combine(ThisAssemblyDirectory, @"TestableSdkComponents\Sdk\MSBuild.SDK.SystemWeb.DefaultPackages.targets"))
                .Save(Path.Combine(TestRootPath, "BeforeMS_NET_SDk_targets.targets"));

            ProjectCreator.Create()
                .Import(Path.Combine(ThisAssemblyDirectory, @"TestableSdkComponents\Sdk\MSBuild.SDK.SystemWeb.Common.DefaultPackageVersions.targets"))
                .Save(Path.Combine(TestRootPath, "AfterMS_NET_SDk_targets.targets"));

            var projVal = ProjectCreator.Templates
                .SdkCsproj(
                    path: Path.Combine(TestRootPath, $"test{extension}"),
                targetFramework: "net47",
                sdk: Path.Combine(ThisAssemblyDirectory, @"TestHarnessInjectionSdk")
                );
            projVal.TryGetItems("PackageReference", out IReadOnlyCollection<ProjectItem> PackRef);
            PackRef.ShouldNotBe(null);
            PackRef.Where(i => i.GetMetadataValue("Version") != "").Count().ShouldBe(2);
            

        }

        [Theory]
        [InlineData(".csproj")]
        //[InlineData(false, ".vbproj")]
        public void DefaultPacakges_WhenCentralPackageVersionSdkDoesNOTHaveVersion_VersionMetadataDefaultsAreProvidedBySdk(string extension)
        {
            ProjectCreator.Create()
                .Import(Path.Combine(ThisAssemblyDirectory, @"TestableSdkComponents\Sdk\MSBuild.SDK.SystemWeb.DefaultPackages.props"))
                .Save(Path.Combine(TestRootPath, "BeforeMS_NET_SDk_props.props"));

            ProjectCreator.Create()
                .Save(Path.Combine(TestRootPath, "Directory.Build.props"));

            ProjectCreator.Create()
                .Save(Path.Combine(TestRootPath, "AfterMS_NET_SDk_props.props"));


            ProjectCreator.Create()
                .Import(Path.Combine(ThisAssemblyDirectory, @"TestableSdkComponents\Sdk\MSBuild.SDK.SystemWeb.DefaultPackages.targets"))
                .ItemGroup().ItemInclude("PackageReferencesAddedBySdkAsAddedWithoutVersionMetadata", "@(PackageReference)")
                .Save(Path.Combine(TestRootPath, "BeforeMS_NET_SDk_targets.targets"));

            ProjectCreator.Create()
                .Save(Path.Combine(TestRootPath, "Packages.props"));

            ProjectCreator.Create()
                .ImportSdk(name: "Microsoft.Build.CentralPackageVersions", version: "2.1.3", project: "Sdk.props")
                .ImportSdk(name: "Microsoft.Build.CentralPackageVersions", version: "2.1.3", project: "Sdk.targets")
                .Save(Path.Combine(TestRootPath, "Directory.Build.targets"));

            ProjectCreator.Create()
                .Import(Path.Combine(ThisAssemblyDirectory, @"TestableSdkComponents\Sdk\MSBuild.SDK.SystemWeb.Common.DefaultPackageVersions.targets"))
                .ItemGroup().ItemInclude("PackageReferencesAfterSdkUpdatedVersionMetadata", "@(PackageReference)")
                .ItemGroup().ItemInclude("SystemWebSdkProvidedPackageVersionDefault_Audit", "@(SystemWebSdkProvidedPackageVersionDefault)")
                .Save(Path.Combine(TestRootPath, "AfterMS_NET_SDk_targets.targets"));

            var projVal = ProjectCreator.Templates
                .SdkCsproj(
                    path: Path.Combine(TestRootPath, $"test{extension}"),
                targetFramework: "net47",
                sdk: Path.Combine(ThisAssemblyDirectory, @"TestHarnessInjectionSdk")
                );



            projVal.TryGetItems("PackageReferencesAddedBySdkAsAddedWithoutVersionMetadata", out IReadOnlyCollection<ProjectItem> PackageReferencesAddedBySdkAsAddedWithoutVersionMetadata);
            PackageReferencesAddedBySdkAsAddedWithoutVersionMetadata.ShouldNotBe(null);
            PackageReferencesAddedBySdkAsAddedWithoutVersionMetadata.Count().ShouldBe(2);
            PackageReferencesAddedBySdkAsAddedWithoutVersionMetadata.Where(i => i.GetMetadataValue("Version") == "").Count().ShouldBe(2);

            projVal.TryGetItems("PackageReferencesAfterSdkUpdatedVersionMetadata", out IReadOnlyCollection<ProjectItem> PackageReferencesAfterSdkUpdatedVersionMetadata);
            PackageReferencesAfterSdkUpdatedVersionMetadata.ShouldNotBe(null);
            PackageReferencesAfterSdkUpdatedVersionMetadata.Count().ShouldBe(2);
            PackageReferencesAfterSdkUpdatedVersionMetadata.Where(i => i.GetMetadataValue("Version") != "").Count().ShouldBe(2);

            projVal.TryGetItems("SystemWebSdkProvidedPackageVersionDefault_Audit", out IReadOnlyCollection<ProjectItem> SystemWebSdkProvidedPackageVersionDefault_Audit);
            SystemWebSdkProvidedPackageVersionDefault_Audit.ShouldNotBe(null);
            SystemWebSdkProvidedPackageVersionDefault_Audit.Count().ShouldBe(2);

            projVal.TryGetPropertyValue("ExcludeASPNetCompilers", out string Property_ExcludeASPNetCompilers);
            Property_ExcludeASPNetCompilers.ShouldNotBe("False");
            //SystemWebSdkProvidedPackageVersionDefault
            projVal.TryGetPropertyValue("UsingMicrosoftCentralPackageVersionsSdk", out string Property_UsingMicrosoftCentralPackageVersionsSdk);
            projVal.TryGetPropertyValue("EnableCentralPackageVersions", out string Property_EnableCentralPackageVersions);
            projVal.TryGetPropertyValue("ManagePackageVersionsCentrally", out string Property_ManagePackageVersionsCentrally);


        }


        [Theory]
        [InlineData(".csproj")]
        //[InlineData(false, ".vbproj")]
        public void DefaultPacakges_WhenCentralPackageManagementSdkDoesNOTHaveVersion_VersionMetadataDefaultsAreProvidedBySdk(string extension)
        {
            ProjectCreator.Create()
                .Import(Path.Combine(ThisAssemblyDirectory, @"TestableSdkComponents\Sdk\MSBuild.SDK.SystemWeb.DefaultPackages.props"))
                .Save(Path.Combine(TestRootPath, "BeforeMS_NET_SDk_props.props"));

            ProjectCreator.Create()
                .Save(Path.Combine(TestRootPath, "Directory.Build.props"));

            ProjectCreator.Create()
                .Save(Path.Combine(TestRootPath, "AfterMS_NET_SDk_props.props"));


            ProjectCreator.Create()
                .Import(Path.Combine(ThisAssemblyDirectory, @"TestableSdkComponents\Sdk\MSBuild.SDK.SystemWeb.DefaultPackages.targets"))
                .ItemGroup().ItemInclude("PackageReferencesAddedBySdkAsAddedWithoutVersionMetadata", "@(PackageReference)")
                .Save(Path.Combine(TestRootPath, "BeforeMS_NET_SDk_targets.targets"));

            ProjectCreator.Create()
                .PropertyGroup().Property("ManagePackageVersionsCentrally","true")
                .Save(Path.Combine(TestRootPath, "Directory.Packages.props"));

            ProjectCreator.Create()
                .Save(Path.Combine(TestRootPath, "Directory.Build.targets"));

            ProjectCreator.Create()
                .Import(Path.Combine(ThisAssemblyDirectory, @"TestableSdkComponents\Sdk\MSBuild.SDK.SystemWeb.Common.DefaultPackageVersions.targets"))
                .ItemGroup().ItemInclude("PackageVersionAfterSdkUpdatedVersionMetadata", "@(PackageVersion)")
                .ItemGroup().ItemInclude("SystemWebSdkProvidedPackageVersionDefault_Audit", "@(SystemWebSdkProvidedPackageVersionDefault)")
                .Save(Path.Combine(TestRootPath, "AfterMS_NET_SDk_targets.targets"));

            var projVal = ProjectCreator.Templates
                .SdkCsproj(
                    path: Path.Combine(TestRootPath, $"test{extension}"),
                targetFramework: "net47",
                sdk: Path.Combine(ThisAssemblyDirectory, @"TestHarnessInjectionSdk")
                );



            projVal.TryGetItems("PackageReferencesAddedBySdkAsAddedWithoutVersionMetadata", out IReadOnlyCollection<ProjectItem> PackageReferencesAddedBySdkAsAddedWithoutVersionMetadata);
            PackageReferencesAddedBySdkAsAddedWithoutVersionMetadata.ShouldNotBe(null);
            PackageReferencesAddedBySdkAsAddedWithoutVersionMetadata.Count().ShouldBe(2);
            PackageReferencesAddedBySdkAsAddedWithoutVersionMetadata.Where(i => i.GetMetadataValue("Version") == "").Count().ShouldBe(2);

            projVal.TryGetItems("PackageVersionAfterSdkUpdatedVersionMetadata", out IReadOnlyCollection<ProjectItem> PackageVersionAfterSdkUpdatedVersionMetadata);
            PackageVersionAfterSdkUpdatedVersionMetadata.ShouldNotBe(null);
            PackageVersionAfterSdkUpdatedVersionMetadata.Count().ShouldBe(2);
            PackageVersionAfterSdkUpdatedVersionMetadata.Where(i => i.GetMetadataValue("Version") != "").Count().ShouldBe(2);

            projVal.TryGetItems("SystemWebSdkProvidedPackageVersionDefault_Audit", out IReadOnlyCollection<ProjectItem> SystemWebSdkProvidedPackageVersionDefault_Audit);
            SystemWebSdkProvidedPackageVersionDefault_Audit.ShouldNotBe(null);
            SystemWebSdkProvidedPackageVersionDefault_Audit.Count().ShouldBe(2);

            projVal.TryGetPropertyValue("ExcludeASPNetCompilers", out string Property_ExcludeASPNetCompilers);
            Property_ExcludeASPNetCompilers.ShouldNotBe("False");
            //SystemWebSdkProvidedPackageVersionDefault
            projVal.TryGetPropertyValue("UsingMicrosoftCentralPackageVersionsSdk", out string Property_UsingMicrosoftCentralPackageVersionsSdk);
            projVal.TryGetPropertyValue("EnableCentralPackageVersions", out string Property_EnableCentralPackageVersions);
            projVal.TryGetPropertyValue("ManagePackageVersionsCentrally", out string Property_ManagePackageVersionsCentrally);
        }
    }
}
