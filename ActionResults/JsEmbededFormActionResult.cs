namespace Morph.Forms.ActionResults
{
  using System;
  using System.Web.Mvc;

  using Morph.Forms.Configuration;
  using Morph.Forms.JsCode;
  using Sitecore.Forms.Mvc.Controllers;
  using Sitecore.Forms.Mvc.Models;
  using Sitecore.Forms.Mvc.ViewModels;
  using Sitecore.StringExtensions;

  /// <summary>
  ///   composite aciton result
  /// </summary>
  /// <seealso cref="System.Web.Mvc.ActionResult" />
  internal class JsEmbededFormActionResult : ActionResult
  {
    /// <summary>
    /// Gets or sets the result.
    /// </summary>
    /// <value>
    /// The result.
    /// </value>
    public FormResult<FormModel, FormViewModel> Result { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsEmbededFormActionResult" /> class.
    /// </summary>
    /// <param name="result">The result.</param>
    public JsEmbededFormActionResult(FormResult<FormModel, FormViewModel> result)
    {
      this.Result = result;
    }

    /// <summary>
    /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.
    /// </summary>
    /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
    /// <exception cref="NotImplementedException"></exception>
    public override void ExecuteResult(ControllerContext context)
    {
      this.Result.ExecuteResult(context);

      var script = Sitecore.Context.Items[Constants.MvcWebFormRulesScriptKey] as string;
      if (!string.IsNullOrEmpty(script))
      {
        var content = new ContentResult { Content = InlineJs.SafeScriptWrapping.FormatWith(script) };
        content.ExecuteResult(context);
      }
    }
  }
}