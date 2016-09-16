using Morph.Forms.JsCode;
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
  /// Defines the changing trigger event for field class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingTriggerEventForField<T> : ChangingFieldRunClientAction<T> where T : ConditionalRuleContext
  {
    /// <summary>
    /// The context
    /// </summary>
    private T context;

    #region Properties

    /// <summary>
    /// Gets or sets the event.
    /// </summary>
    /// <value>
    /// The event.
    /// </value>
    [CanBeNull]
    public string Event { get; set; }

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
      this.context = ruleContext;
      this.TriggerValue = ".*";
      base.Apply(ruleContext);
    }


    /// <summary>
    /// Builds the client script.
    /// </summary>
    /// <returns>
    /// The client script.
    /// </returns>
    protected override string BuildClientScript()
    {
      var selector = this.GetClientElementSelector(this.context?.Control, ((FieldViewModel) this.context?.Model)?.FieldItemId);

      return "$('{0}').trigger('{1}');".FormatWith(selector, this.Event);
    }

    #endregion
  }
}