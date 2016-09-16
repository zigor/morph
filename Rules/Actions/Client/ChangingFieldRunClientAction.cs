using System;
using Morph.Forms.JsCode;
using Sitecore.Data;
using Sitecore.Forms.Mvc.ViewModels;
using Sitecore.StringExtensions;

namespace Morph.Forms.Rules.Actions.Client
{
  using System.Web.UI;

  using Morph.Forms.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing field run client action class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class ChangingFieldRunClientAction<T> : ClientAction<T> where T : ConditionalRuleContext
  {
    #region Properties

    /// <summary>
    /// Gets or sets the field trigger.
    /// </summary>
    /// <value>
    /// The field trigger.
    /// </value>
    [CanBeNull]
    public string Trigger { get; set; }

    /// <summary>
    /// Gets or sets the trigger value.
    /// </summary>
    /// <value>
    /// The trigger value.
    /// </value>
    [CanBeNull]
    public string TriggerValue { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply([NotNull] T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (string.IsNullOrEmpty(this.Trigger))
      {
        return;
      }

      base.Apply(ruleContext);
    }

    /// <summary>
    /// Registers the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="model">The model.</param>
    /// <returns>
    /// The script.
    /// </returns>
    [CanBeNull]
    protected override string PrepareScript([CanBeNull] Control control, [CanBeNull]FieldViewModel model)
    {
      var triggerSelector = this.GetClientElementSelector(this.GetField(control, this.Trigger), this.Trigger);
      var observerSelector = this.GetClientElementSelector(control, model?.FieldItemId);


      if (string.IsNullOrEmpty(triggerSelector) || string.IsNullOrEmpty(observerSelector))
      {
        return string.Empty;
      }

      return JsCode.InlineJs.OnChangeToValueForElementExecuteCode.FormatWith(triggerSelector, this.TriggerValue, observerSelector, this.BuildClientScript());      
    }

    /// <summary>
    /// Builds the client script.
    /// </summary>
    /// <returns>
    /// The client script.
    /// </returns>
    [CanBeNull]
    protected abstract string BuildClientScript();

    #endregion
  }
}