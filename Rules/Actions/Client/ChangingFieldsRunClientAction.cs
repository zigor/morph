using System.Linq;
using System.Web.UI;
using Morph.Forms.JsCode;
using Morph.Forms.Web.UI;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Rules;
using Sitecore.Forms.Mvc.ViewModels;
using Sitecore.StringExtensions;

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
    /// Prepares the script.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="model">The model.</param>
    /// <returns>
    /// The script.
    /// </returns>
    [NotNull]
    protected override string PrepareScript([CanBeNull] Control control, [CanBeNull]FieldViewModel model)
    {
      var triggerSelector = this.GetClientElementSelector(this.GetField(control, this.Trigger), this.Trigger);
      var trigger2Selector = this.GetClientElementSelector(this.GetField(control, this.Trigger2), this.Trigger2);
      var observerSelector = this.GetClientElementSelector(control, model?.FieldItemId);

      var operatorItem = Context.Database.GetItem(this.Operator);

      if (new [] { triggerSelector, trigger2Selector, observerSelector }.Any(string.IsNullOrEmpty) || operatorItem == null)
      {
        return string.Empty;
      }

      return JsCode.InlineJs.OnChangeElementsToValuesForElementExecuteCode
        .FormatWith(triggerSelector, this.TriggerValue, trigger2Selector, this.TriggerValue2, operatorItem.Name == "and" ? "&&" : "||", observerSelector, this.BuildClientScript());
    }

    #endregion
  }
}