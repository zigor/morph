using System.Web;
using Sitecore;

namespace Morph.Forms.Rules.Actions.Client
{
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;
  using Sitecore.Forms.Mvc.ViewModels;
  using Sitecore.Mvc.Presentation;
  using Sitecore.StringExtensions;
  using System.Web.UI;
  using Morph.Forms.Configuration;
  using Morph.Forms.JsCode;
  using Morph.Forms.Web.UI;

  /// <summary>
  ///   Defines the client action class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class ClientAction<T> : FieldAction<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    ///   Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply([NotNull] T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, nameof(ruleContext));

      if (((ruleContext.Control == null) || !ruleContext.Control.Visible) && (ruleContext.Model == null))
      {
        return;
      }

      if (ruleContext.Control != null)
      {
        ruleContext.Control.PreRender += (s, e) => RegisterScript(ruleContext.Control, this.PrepareScript(ruleContext.Control, null));
      }

      var fieldModel = ruleContext.Model as FieldViewModel;
      if (fieldModel != null)
      {
        this.RegisterScript(fieldModel, this.PrepareScript(null, fieldModel));
      }
    }

    /// <summary>
    /// Prepares the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="model">The model.</param>
    /// <returns>
    /// The script.
    /// </returns>
    [CanBeNull]
    protected virtual string PrepareScript([CanBeNull] Control control, [CanBeNull]FieldViewModel model)
    {
      return string.Empty;
    }

    /// <summary>
    ///   Registers the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="script">The script.</param>
    private static void RegisterScript([NotNull] Control control, [CanBeNull] string script)
    {
      Assert.ArgumentNotNull(control, nameof(control));

      if (string.IsNullOrEmpty(script))
      {
        return;
      }

      ScriptManager.RegisterStartupScript(control, typeof(Page), script, InlineJs.SafeScriptWrapping.FormatWith(script), false);
    }

    /// <summary>
    /// Gets the client element selector.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="fieldId">The field identifier.</param>
    /// <returns></returns>
    [CanBeNull]
    protected string GetClientElementSelector([CanBeNull]Control control = null, [CanBeNull]string fieldId = null)
    {
      if (control?.Page != null)
      {
        var clientControl = this.GetChildMatchingAnyId(control.Controls.Flatten(), control.ID, control.ID + "scope", control.ID + "checkbox");
        return InlineJs.SelectorById.FormatWith(clientControl.ClientID);
      }
      if (!string.IsNullOrEmpty(fieldId))
      {
        return InlineJs.SelectorByNameFromHiddenValue.FormatWith(fieldId);
      }
      return null;
    }

    /// <summary>
    /// Registers the script.
    /// </summary>
    /// <param name="fieldViewModel">The field view model.</param>
    /// <param name="script">The script.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    private void RegisterScript([NotNull]FieldViewModel fieldViewModel, [CanBeNull]string script)
    {
      Assert.ArgumentNotNull(fieldViewModel, nameof(fieldViewModel));

      if (string.IsNullOrEmpty(script))
      {
        return;
      }

      Sitecore.Context.Items[Constants.MvcWebFormRulesScriptKey] += script;
    }

    #endregion
  }
}