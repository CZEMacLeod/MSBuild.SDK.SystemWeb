using System.Web;
using System.Web.Optimization;

namespace ExampleRazorLibraryApplication
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/lib/jquery/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/lib/jquery-validate/jquery.validate.js*").Include(
						"~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/lib/modernizr/modernizr.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/lib/bootstrap/dist/js/bootstrap.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/lib/bootstrap/dist/css/bootstrap.css",
					  "~/Content/site.css"));
		}
	}
}
