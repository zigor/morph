using System.Web.UI;
using Sitecore.StringExtensions;

namespace Morph.Forms.Rules.Actions.Client
{
  using System.Web.UI.WebControls;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;
  using Sitecore.Forms.Mvc.ViewModels;

  using Morph.Forms.JsCode;

  /// <summary>
  /// Defines the hide element inputs class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class DisableElement<T> : ClientAction<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply([NotNull] T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (ruleContext.Control != null)
      {
        var webControl = ruleContext.Control as WebControl;
        if (webControl != null)
        {
          webControl.Enabled = false;
        }
        return;
      }

      base.Apply(ruleContext);
    }

    /// <summary>
    /// Prepares the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="fieldViewModel">The field view model.</param>
    /// <returns></returns>
    protected override string PrepareScript([CanBeNull]Control control, [CanBeNull]FieldViewModel fieldViewModel)
    {
      var selector = this.GetClientElementSelector(null, fieldViewModel?.FieldItemId);

      if (control != null || string.IsNullOrEmpty(selector))
      {
        return null;
      }

      return "$('{0}').prop('disabled', 'true');".FormatWith(selector);
    }

    #endregion
  }
}