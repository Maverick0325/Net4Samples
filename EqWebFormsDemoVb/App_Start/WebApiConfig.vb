﻿Imports System.Collections.Generic
Imports System.Web.Http
Imports System.Web.Http.Controllers
Imports System.Web.Http.Routing

Imports Korzh.EasyQuery.Services

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)

        Korzh.EasyQuery.AspNet.License.Key = "ZNQbiled5JoJMwzArFmSSQGSP5J77Y"
        Korzh.EasyQuery.AspNet.JSLicense.Key = "voC5XVNcovrofRwXkHNKEABNJ4VH40"

        '// Web API configuration and services
        '// Web API routes
        Dim customRouteProvider As New WebApiCustomDirectRouteProvider
        config.MapHttpAttributeRoutes(customRouteProvider)

        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

        '// Register you exportes here
        '// to make export works
        EasyQueryManager.RegisterExporter("csv", New CsvDataExporter())
        EasyQueryManager.RegisterExporter("excel-html", New ExcelHtmlDataExporter())

    End Sub
End Module

Public Class WebApiCustomDirectRouteProvider
    Inherits DefaultDirectRouteProvider
    Protected Overrides Function GetActionRouteFactories(actionDescriptor As HttpActionDescriptor) As IReadOnlyList(Of IDirectRouteFactory)

        '    '// inherit route attributes decorated on base class controller's actions
        Return actionDescriptor.GetCustomAttributes(Of IDirectRouteFactory)(True)

    End Function

End Class
