using Morph.Forms.JsCode;
using Sitecore.Forms.Mvc.ViewModels;

namespace Morph.Forms.Rules.Actions.Client
{
  using System.Web.UI;

  using Morph.Forms.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing trigger event for field class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingTriggerEventForField<T> : ClientAction<T> where T : ConditionalRuleContext
  {
    #region Properties

    /// <summary>
    /// Gets or sets the event.
    /// </summary>
    /// <value>
    /// The event.
    /// </value>
    [CanBeNull]
    public string Event { get; set; }

    /// <summary>
    /// Gets or sets the trigger.
    /// </summary>
    /// <value>
    /// The trigger.
    /// </value>
    [CanBeNull]
    public string Trigger { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply([NotNull] T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (string.IsNullOrEmpty(this.Trigger) || string.IsNullOrEmpty(this.Event))
      {
        return;
      }

      base.Apply(ruleContext);
    }

    /// <summary>
    /// Prepares the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns>
    /// The script.
    /// </returns>
    [NotNull]
    protected override string PrepareScript([NotNull] Control control)
    {
      Control trigger = this.GetField(control, this.Trigger);
      if (trigger == null || control.Page == null)
      {
        return string.Empty;
      }

      var triggerControl = this.GetChildMatchingAnyId(trigger.Controls.Flatten(), trigger.ID, trigger.ID + "scope");
      var observerControl = this.GetChildMatchingAnyId(control.Controls.Flatten(), control.ID, control.ID + "scope");

      if (triggerControl == null || observerControl == null)
      {
        return string.Empty;
      }

      return JsCodeSnippets.OnChangeTrigger(
        JsCodeSnippets.SelectorById(triggerControl.ClientID),
        JsCodeSnippets.SelectorById(observerControl.ClientID),
        this.Event);
    }

    /// <summary>
    /// Prepares the script.
    /// </summary>
    /// <param name="fieldViewModel">The field view model.</param>
    /// <returns></returns>
    [NotNull]
    protected override string PrepareScript([NotNull] FieldViewModel fieldViewModel)
    {
      return JsCodeSnippets.OnChangeTrigger(
        JsCodeSnippets.SelectorByNameFromHiddenValue(this.Trigger),
        JsCodeSnippets.SelectorByNameFromHiddenValue(fieldViewModel.FieldItemId),
        this.Event);
    }

    #endregion
  }
}