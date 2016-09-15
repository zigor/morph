using System.Web.UI;
using Morph.Forms.JsCode;
using Morph.Forms.Web.UI;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Rules;
using Sitecore.Forms.Mvc.ViewModels;

namespace Morph.Forms.Rules.Actions.Client
{
  /// <summary>
  ///   Defines the changing fields run script class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class ChangingFieldsRunClientAction<T> : ChangingFieldRunClientAction<T>
    where T : ConditionalRuleContext
  {
    #region Properties

    /// <summary>
    ///   Gets or sets the trigger2.
    /// </summary>
    /// <value>
    ///   The trigger2.
    /// </value>
    [CanBeNull]
    public string Trigger2 { get; set; }

    /// <summary>
    ///   Gets or sets the trigger value2.
    /// </summary>
    /// <value>
    ///   The trigger value2.
    /// </value>
    [CanBeNull]
    public string TriggerValue2 { get; set; }

    /// <summary>
    ///   Gets or sets the operator.
    /// </summary>
    /// <value>
    ///   The operator.
    /// </value>
    [CanBeNull]
    public string Operator { get; set; }

    #endregion

    #region Methods

    /// <summary>
    ///   Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply([NotNull] T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (string.IsNullOrEmpty(this.Trigger2) || string.IsNullOrEmpty(this.Operator))
      {
        return;
      }

      base.Apply(ruleContext);
    }

    /// <summary>
    ///   Prepares the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns>
    ///   The script.
    /// </returns>
    [NotNull]
    protected override string PrepareScript([NotNull] Control control)
    {
      var trigger = this.GetField(control, this.Trigger);
      var trigger2 = this.GetField(control, this.Trigger2);
      if ((trigger == null) || (trigger2 == null) || (control.Page == null) || (Context.Database == null))
      {
        return string.Empty;
      }

      var triggerControl = this.GetChildMatchingAnyId(trigger.Controls.Flatten(), trigger.ID, trigger.ID + "scope");
      var triggerControl2 = this.GetChildMatchingAnyId(trigger2.Controls.Flatten(), trigger2.ID, trigger2.ID + "scope");
      var observeControl = this.GetChildMatchingAnyId(control.Controls.Flatten(), control.ID, control.ID + "scope");

      var operatorItem = Context.Database.GetItem(this.Operator);

      if ((triggerControl == null) || (triggerControl2 == null) || (observeControl == null) || (operatorItem == null))
      {
        return string.Empty;
      }

      var triggerCondition = new JsCodeBuilder()
        .AddSelectorById(triggerControl.ClientID)
        .AddFind()
        .AddGetValue()
        .AddTestRegexp(this.TriggerValue);

      var triggerCondition2 = new JsCodeBuilder()
        .AddSelectorById(triggerControl2.ClientID)
        .AddFind()
        .AddGetValue()
        .AddTestRegexp(this.TriggerValue2);

      var contextElement = new JsCodeBuilder()
        .AddSelectorById(observeControl.ClientID)
        .AddFind();

      return new JsCodeBuilder()
        .AddSelectorById(triggerControl.ClientID, triggerControl.ClientID)
        .AddFind()
        .AddOnChangeExecute(this.GetConditionWithExecuteCode(triggerCondition, triggerCondition2, contextElement))
        .ToString();
    }

    /// <summary>
    ///   Prepares the script.
    /// </summary>
    /// <param name="fieldViewModel">The field view model.</param>
    /// <returns></returns>
    protected override string PrepareScript(FieldViewModel fieldViewModel)
    {
      var triggerCondition = new JsCodeBuilder()
        .AddSelectorByNameFromHiddenValue(this.Trigger)
        .AddFind()
        .AddGetValue()
        .AddTestRegexp(this.TriggerValue);

      var triggerCondition2 = new JsCodeBuilder()
        .AddSelectorByNameFromHiddenValue(this.Trigger2)
        .AddFind()
        .AddGetValue()
        .AddTestRegexp(this.TriggerValue2);
      
      var contextElement = new JsCodeBuilder()
        .AddSelectorByNameFromHiddenValue(fieldViewModel.FieldItemId)
        .AddFind();

      return new JsCodeBuilder()
        .AddSelectorByNameFromHiddenValue(this.Trigger, this.Trigger2)
        .AddFind()
        .AddOnChangeExecute(this.GetConditionWithExecuteCode(triggerCondition, triggerCondition2, contextElement))
        .ToString();
    }

    /// <summary>
    /// Gets the condition with execute code.
    /// </summary>
    /// <param name="triggerCondition">The trigger condition.</param>
    /// <param name="triggerCondition2">The trigger condition2.</param>
    /// <param name="contextElement">The context element.</param>
    /// <returns></returns>
    private string GetConditionWithExecuteCode(JsCodeBuilder triggerCondition, JsCodeBuilder triggerCondition2, JsCodeBuilder contextElement)
    {
      var operatorItem = Context.Database.GetItem(this.Operator);
      var code = string.Join(string.Empty, "if (", triggerCondition, " ", operatorItem.Name == "and" ? "&&" : "||", " ", triggerCondition2, "){", contextElement.ExecuteWithElementInThis(this.BuildClientScript()), "}");
      return code;
    }

    #endregion
  }
}