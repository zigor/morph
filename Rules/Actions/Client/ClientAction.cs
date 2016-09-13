using System;
using System.Web;
using System.Web.UI;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Rules;
using Sitecore.Forms.Mvc.Html;
using Sitecore.Forms.Mvc.ViewModels;
using Sitecore.Mvc.Presentation;
using Sitecore.StringExtensions;

namespace Morph.Forms.Rules.Actions.Client
{
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
    protected abstract string PrepareScript([NotNull] Control control);

    /// <summary>
    /// Prepares the script.
    /// </summary>
    /// <param name="fieldViewModel">The field view model.</param>
    /// <returns></returns>
    [CanBeNull]
    protected abstract string PrepareScript(FieldViewModel fieldViewModel);

    /// <summary>
    ///   Registers the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="script">The script.</param>
    private static void RegisterScript([NotNull] Control control, [CanBeNull] string script)
    {
      Assert.ArgumentNotNull(control, nameof(control));

      string ready = FormatScript(script);
      if (string.IsNullOrEmpty(ready))
      {
        return;
      }

      ScriptManager.RegisterStartupScript(control, typeof(Page), ready, ready, true);
    }

    /// <summary>
    /// Formats the script.
    /// </summary>
    /// <param name="script">The script.</param>
    /// <returns></returns>
    [CanBeNull]
    private static string FormatScript([CanBeNull]string script)
    {
      if (string.IsNullOrEmpty(script))
      {
        return null;
      }

      return "$scw(document).ready(function(){{{0}}});".FormatWith(script);
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

      string ready = FormatScript(script);
      if (string.IsNullOrEmpty(ready))
      {
        return;
      }

      RenderingContext.Current.PageContext.HtmlHelper.ViewContext.Writer.Write("<script type=\"tex/javascript\">" + ready + "</script>");
    }

    #endregion
  }
}