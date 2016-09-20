using Morph.Forms.Filters;
using Sitecore.Forms.Mvc.Controllers;
using Sitecore.Mvc.Pipelines.MvcEvents.ActionExecuted;

namespace Morph.Forms.Pipelines.ActionExecuted
{
  public class OnFormControllerActionExecuted : ActionExecutedProcessor
  {
    /// <summary>
    ///   Processes the specified arguments.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public override void Process(ActionExecutedArgs args)
    {
      if (args.Context.Controller is FormController)
      {
        new AddJsToActionResultFilterAttribute().OnActionExecuted(args.Context);
      }
    }
  }
}