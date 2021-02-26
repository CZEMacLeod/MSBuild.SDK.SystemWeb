Imports System.Web.Optimization

Public Module BundleConfig
    ' For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                    "~/lib/jquery/jquery.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/lib/jquery-validate/jquery.validate.js").Include(
                    "~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"))

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/lib/modernizr/modernizr.js"))

        bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
                  "~/lib/bootstrap/dist/js/bootstrap.js"))

        bundles.Add(New StyleBundle("~/Content/css").Include(
                  "~/lib/bootstrap/dist/css/bootstrap.css",
                  "~/Content/site.css"))
    End Sub
End Module

