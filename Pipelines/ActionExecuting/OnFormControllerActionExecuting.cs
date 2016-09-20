using Sitecore.Forms.Mvc.Controllers;

namespace Morph.Forms.Pipelines.ActionExecuting
{
  using Morph.Forms.Filters;

  using Sitecore.Mvc.Pipelines.MvcEvents.ActionExecuting;

  /// <summary>
  /// On form controller action execution
  /// </summary>
  /// <seealso cref="Sitecore.Mvc.Pipelines.MvcEvents.ActionExecuting.ActionExecutingProcessor" />
  public class OnFormControllerActionExecuting : ActionExecutingProcessor
  {
    /// <summary>
    /// Processes the specified arguments.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public override void Process(ActionExecutingArgs args)
    {
      if (args.Context.Controller is FormController)
      {
        new ApplyRulesToFormViewModelAttribute().OnActionExecuting(args.Context);
      }
    }
  }
}