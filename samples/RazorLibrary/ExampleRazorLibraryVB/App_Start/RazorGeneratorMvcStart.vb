Imports System.Web.WebPages
Imports RazorGenerator.Mvc

<Assembly: WebActivatorEx.PostApplicationStartMethod(
    GetType(RazorSystemWeb.RazorGeneratorMvcStart),
    NameOf(RazorSystemWeb.RazorGeneratorMvcStart.Start)
    )>

Namespace RazorSystemWeb
    Public NotInheritable Class RazorGeneratorMvcStart
        Private Sub New()

        End Sub

        Public Shared Sub Start()
            Dim engine = New PrecompiledMvcEngine(GetType(RazorGeneratorMvcStart).Assembly) With {
                .UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
            }

            ViewEngines.Engines.Insert(0, engine)

            '' StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine)
        End Sub
    End Class
End Namespace
