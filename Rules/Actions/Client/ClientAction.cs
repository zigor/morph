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
        ruleContext.Control.PreRender += (s, e) => RegisterScript(ruleContext.Control, this.PrepareScript(ruleContext.Control));
      }

      var fieldModel = ruleContext.Model as FieldViewModel;
      if (fieldModel != null)
      {
        this.RegisterScript(fieldModel, this.PrepareScript(fieldModel));
      }
    }

    /// <summary>
    ///   Prepares the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns>
    ///   The script.
    /// </returns>
    [CanBeNull]
    protected virtual string PrepareScript([NotNull] Control control)
    {
      return string.Empty;
    }

    /// <summary>
    /// Prepares the script.
    /// </summary>
    /// <param name="fieldViewModel">The field view model.</param>
    /// <returns></returns>
    [CanBeNull]
    protected virtual string PrepareScript(FieldViewModel fieldViewModel)
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

      ScriptManager.RegisterStartupScript(control, typeof(Page), script, script, true);
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
      RenderingContext.Current.Rendering.Properties[Constants.MvcWebFormRulesScriptKey] += script;
    }

    #endregion
  }
}