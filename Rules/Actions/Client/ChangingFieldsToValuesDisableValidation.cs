using Morph.Forms.Models;
using Sitecore.Forms.Mvc.ViewModels;

namespace Morph.Forms.Rules.Actions.Client
{
  using System.Web.UI;
  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Form.Web.UI.Controls;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing fields to values disable validation class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingFieldsToValuesDisableValidation<T> : ChangingFieldToValueDisableValidation<T> where T : ConditionalRuleContext
  {
    #region Properties

    /// <summary>
    /// Gets or sets the field2.
    /// </summary>
    /// <value>
    /// The field2.
    /// </value>
    [CanBeNull]
    public string Trigger2 { get; set; }

    /// <summary>
    /// Gets or sets the value2.
    /// </summary>
    /// <value>
    /// The value2.
    /// </value>
    [CanBeNull]
    public string TriggerValue2 { get; set; }

    /// <summary>
    /// Gets or sets the operator.
    /// </summary>
    /// <value>
    /// The operator.
    /// </value>
    [CanBeNull]
    public string Operator { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Applies the specified rule context.
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

    protected override void DisableValidationOnSubmit(RuleContextModel model)
    {
      var operatorItem = Context.Database.GetItem(this.Operator);

      if (operatorItem == null)
      {
        return;
      }

      var match = this.Match(this.GetValue(model, this.Trigger2), this.TriggerValue2);

      if (match && operatorItem.Name == "and" || !match && operatorItem.Name != "and")
      {
        base.DisableValidationOnSubmit(model);
        return;
      }

      if (match)
      {
        base.DisableValidationOnSubmit(model);
      }
    }

    /// <summary>
    /// Called when the fields has loaded.
    /// </summary>
    /// <param name="context">The context.</param>
    protected override void DisableValidationOnSubmit([NotNull] Control context)
    {
      Assert.ArgumentNotNull(context, "context");

      var operatorItem = Context.Database.GetItem(this.Operator);
      var trigger2 = this.GetField(context, this.Trigger2) as IResult;     

      if (trigger2 == null || operatorItem == null)
      {
        return;
      }

      var match = this.Match(trigger2.Result.Value, this.TriggerValue2);
      if (match && operatorItem.Name == "and" || !match && operatorItem.Name != "and")
      {
        base.DisableValidationOnSubmit(context);
        return;
      }
      
      if (match)
      {
        this.DisableValidation(context, null);
      }
    }

    #endregion
  }
}