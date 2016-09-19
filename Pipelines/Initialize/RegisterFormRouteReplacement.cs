namespace Morph.Forms.Pipelines.Initialize
{
  using Sitecore.Pipelines;
  using System.Web.Mvc;
  using System.Web.Routing;

  /// <summary>
  /// Set model binders
  /// </summary>
  /// <seealso cref="Sitecore.Mvc.Pipelines.Loader.InitializeRoutes" />
  public class RegisterFormRouteReplacement : Sitecore.Mvc.Pipelines.Loader.InitializeRoutes
  {
    /// <summary>
    /// Registers the routes.
    /// </summary>
    /// <param name="routes">The routes.</param>
    /// <param name="args">The arguments.</param>
    protected override void RegisterRoutes(RouteCollection routes, PipelineArgs args)
    {
      RegisterFormRouteReplacement.RegisterFormSubmitController(routes);
    }

    /// <summary>
    /// Registers the client event controller.
    /// </summary>
    /// <param name="routes">The routes.</param>
    private static void RegisterFormSubmitController(RouteCollection routes)
    {
      var routeData = (Route)routes[Sitecore.Forms.Mvc.Constants.Routes.Form];

   //   routeData.DataTokens = new RouteValueDictionary(new { Namespaces = new[] { "Morph.Forms.Controllers" } });      
    }
  }
}