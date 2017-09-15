using Sitecore.StringExtensions;

namespace Morph.Forms.Rules.Actions.Client
{
  using System;
  using System.Linq;
  using System.Web.UI;
  using System.Web.UI.WebControls;
  using Morph.Forms.JsCode;
  using Morph.Forms.Web.UI;
  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;
  using Sitecore.Forms.Mvc.ViewModels;

  /// <summary>
  ///   Defines the set read only class.
  /// </summary>
  /// <typeparam name="T">
  /// </typeparam>
  public class SetReadOnly<T> : ClientAction<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    ///   Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">
    ///   The rule context.
    /// </param>
    public override void Apply([NotNull] T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (ruleContext.Control != null)
      {
        ruleContext.Control.Load += this.OnControlLoaded;
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
    /// <exception cref="System.NotImplementedException"></exception>
    protected override string PrepareScript([CanBeNull]Control control, [CanBeNull]FieldViewModel fieldViewModel)
    {
      var selector = this.GetClientElementSelector(null, fieldViewModel?.FieldItemId);

      if (control != null || string.IsNullOrEmpty(selector))
      {
        return null;
      }

      return "$('{0}').prop('readonly', true);".FormatWith(selector);
    }

    /// <summary>
    ///   Called when the control has loaded.
    /// </summary>
    /// <param name="sender">The sender control.</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnControlLoaded([NotNull] object sender, [CanBeNull] EventArgs e)
    {
      Assert.ArgumentNotNull(sender, "sender");

      var controls = ((Control) sender).Controls.Flatten().OfType<TextBox>();
      foreach (var textBox in controls)
      {
        textBox.ReadOnly = true;
      }
    }

    #endregion
  }
}