using Sitecore.Forms.Mvc.ViewModels;

namespace Morph.Forms.Rules.Actions.Client
{
  using Sitecore;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing field hide current field class.
  /// </summary>
  public class ChangingFieldHideCurrentField<T> : ChangingFieldRunClientAction<T> where T : ConditionalRuleContext
  {
    #region Methods

    private FieldViewModel model;

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply([NotNull]T ruleContext)
    {
      var disableValidation = new ChangingFieldToValueDisableValidation<ConditionalRuleContext>();
      disableValidation.Trigger = this.Trigger;
      disableValidation.TriggerValue = this.TriggerValue;

      disableValidation.Apply(ruleContext);

      this.model = ruleContext.Model as FieldViewModel;
      
      base.Apply(ruleContext);
    }

    /// <summary>
    /// Gets the client script.
    /// </summary>
    /// <returns>
    /// The client script.
    /// </returns>
    [NotNull]
    protected override string BuildClientScript()
    {
      if (this.model != null)
      {
        return "$(this).closest('.form-group').hide();$(this).prop('disabled', true)";
      }
      return  "$(this).parent().parent().hide();";
    }

    #endregion
  }
}