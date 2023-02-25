using Microsoft.Build.Evaluation;
using TestingShared;
using Microsoft.Build.Utilities.ProjectCreation;
using Shouldly;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace MSBuild.SDK.SystemWeb.UnitTests
{
    public class DefaultPackageTests : MSBuildSdkTestBase
    {
        private readonly ITestOutputHelper output;

        private static readonly string ThisAssemblyDirectory = Path.GetDirectoryName(typeof(UnitTest1).Assembly.Location);

        public DefaultPackageTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData(".csproj")]
        //[InlineData(false, ".vbproj")]
        public void DefaultPacakges_WhenNoCentralManagement_ArePresent(string extension)
        {
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
        public void DefaultPacakges_WhenCentralManagement_ArePresent(string extension)
        {
            ProjectCreator.Create()
                .Save(Path.Combine(TestRootPath, "Directory.Build.props"));

            ProjectCreator.Create()
                .Import(Path.Combine(ThisAssemblyDirectory, @"TestableSdkComponents\Sdk\MSBuild.SDK.SystemWeb.DefaultPackages.props"))
                .Save(Path.Combine(TestRootPath, "BeforeMS_NET_SDk_props.props"));

            ProjectCreator.Create()
                .Save(Path.Combine(TestRootPath, "AfterMS_NET_SDk_props.props"));


            ProjectCreator.Create()
                .ImportSdk(name: "Microsoft.Build.CentralPackageVersions", version:"2.1.3",project:"Sdk.props")
                .ImportSdk(name: "Microsoft.Build.CentralPackageVersions", version: "2.1.3", project: "Sdk.targets")
                .Save(Path.Combine(TestRootPath, "Directory.Build.targets"));

            ProjectCreator.Create()
                .Import(Path.Combine(ThisAssemblyDirectory, @"TestableSdkComponents\Sdk\MSBuild.SDK.SystemWeb.DefaultPackages.targets"))
                .ItemGroup().ItemInclude("PackageReferenceBefore", "@(PackageReference)")
                .Save(Path.Combine(TestRootPath, "BeforeMS_NET_SDk_targets.targets"));

            ProjectCreator.Create()
                .Save(Path.Combine(TestRootPath, "AfterMS_NET_SDk_targets.targets"));

            var projVal = ProjectCreator.Templates
                .SdkCsproj(
                    path: Path.Combine(TestRootPath, $"test{extension}"),
                targetFramework: "net47",
                sdk: Path.Combine(ThisAssemblyDirectory, @"TestHarnessInjectionSdk")
                );
            projVal.TryGetItems("PackageReference", out IReadOnlyCollection<ProjectItem> PackRef);
            PackRef.ShouldNotBe(null);

            projVal.TryGetItems("PackageReferenceBefore", out IReadOnlyCollection<ProjectItem> PackRefAutdit);
            PackRefAutdit.Where(i => i.GetMetadataValue("Version") == "").Count().ShouldBe(2);




        }
    }
}
