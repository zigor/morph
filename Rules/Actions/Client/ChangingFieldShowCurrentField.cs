using Sitecore.Forms.Mvc.ViewModels;

namespace Morph.Forms.Rules.Actions.Client
{
  using Sitecore;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing field show current field class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingFieldShowCurrentField<T> : ChangingFieldRunClientAction<T> where T : ConditionalRuleContext
  {
    /// <summary>
    /// The model
    /// </summary>
    private FieldViewModel model;

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      this.model = ruleContext.Model as FieldViewModel;

      base.Apply(ruleContext);
    }

    #region Methods

    /// <summary>
    /// Builds the client script.
    /// </summary>
    /// <returns>
    /// The client script.
    /// </returns>
    [NotNull]
    protected override string BuildClientScript()
    {
      if (this.model != null)
      {
        return "$(this).closest('.form-group').show();$(this).prop('disabled', 'false')";
      }
      return "$(this).parent().parent().show();";
    }

    #endregion
  }
}