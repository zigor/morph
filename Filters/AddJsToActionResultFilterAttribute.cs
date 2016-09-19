using System.Web.Mvc;
using Morph.Forms.ActionResults;
using Morph.Forms.Configuration;
using Morph.Forms.JsCode;
using Sitecore.Forms.Mvc.Controllers;
using Sitecore.Forms.Mvc.Models;
using Sitecore.Forms.Mvc.ViewModels;
using Sitecore.StringExtensions;

namespace Morph.Forms.Filters
{
  /// <summary>
  /// Adds js to action result filter 
  /// </summary>
  /// <seealso cref="System.Web.Mvc.ActionFilterAttribute" />
  public class AddJsToActionResultFilterAttribute : ActionFilterAttribute
  {
    /// <summary>
    /// Called by the ASP.NET MVC framework before the action result executes.
    /// </summary>
    /// <param name="filterContext">The filter context.</param>
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
      base.OnActionExecuted(filterContext);

      var result = filterContext.Result as FormResult<FormModel, FormViewModel>;

      if (result == null)
      {
        return;
      }

      filterContext.Result = new JsEmbededFormActionResult(result);
    }
  }
}