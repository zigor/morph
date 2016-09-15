using System;
using Morph.Forms.JsCode;
using Sitecore.Data;
using Sitecore.Forms.Mvc.ViewModels;

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
    [CanBeNull]
    protected override string PrepareScript([NotNull] Control control)
    {
      Control trigger = this.GetField(control, this.Trigger);
      if (trigger == null || control.Page == null)
      {
        return string.Empty;
      }

      var triggerControl = this.GetChildMatchingAnyId(trigger.Controls.Flatten(), trigger.ID, trigger.ID + "scope", trigger.ID + "checkbox");
      var observerControl = this.GetChildMatchingAnyId(control.Controls.Flatten(), control.ID, control.ID + "scope", control.ID + "checkbox");

      if (triggerControl == null || observerControl == null)
      {
        return string.Empty;
      }

      var code = new JsCodeBuilder()
              .AddSelectorById(observerControl.ClientID)
              .AddFind()
              .ExecuteWithElementInThis(this.BuildClientScript())
              .ToString();

      return new JsCodeBuilder()
              .AddSelectorById(triggerControl.ClientID)
              .AddFind()
              .AddOnChangeToValueExecute(this.TriggerValue, code)
              .AddDomeReady()
              .AddScope()
              .ToString();
    }

    /// <summary>
    /// Prepares the script.
    /// </summary>
    /// <param name="fieldViewModel">The field view model.</param>
    /// <returns></returns>
    [CanBeNull]
    protected override string PrepareScript(FieldViewModel fieldViewModel)
    {

      var code = new JsCodeBuilder()
              .AddSelectorByNameFromHiddenValue(fieldViewModel.FieldItemId)
              .AddFind()
              .ExecuteWithElementInThis(this.BuildClientScript())
              .ToString();

      return new JsCodeBuilder()
              .AddSelectorByNameFromHiddenValue(this.Trigger)
              .AddFind()
              .AddOnChangeToValueExecute(this.TriggerValue, code)
              .ToString();
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