namespace Morph.Forms.Pipelines.ActionExecuting
{
  //using Morph.Forms.Controllers;
  using Morph.Forms.Filters;

  using Sitecore.Mvc.Pipelines.MvcEvents.ActionExecuting;
  using SysFormController = Sitecore.Forms.Mvc.Controllers.FormController;

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
      if (args.Context.Controller is SysFormController)
      {
        new ApplyRulesToFormViewModelAttribute().OnActionExecuting(args.Context);
      }
    }
  }
}