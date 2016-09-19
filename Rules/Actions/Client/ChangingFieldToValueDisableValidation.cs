namespace Morph.Forms.Rules.Actions.Client
{
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using Morph.Forms.Web.UI;
  using Sitecore.Diagnostics;
  using Sitecore.Form.Web.UI.Controls;
  using Sitecore.Forms.Core.Rules;

  using Morph.Forms.Models;
  using Sitecore;
  using Sitecore.Forms.Mvc.Interfaces;

  /// <summary>
  /// Defines the changing field to value disable validation class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ChangingFieldToValueDisableValidation<T> : FieldAction<T> where T : ConditionalRuleContext
  {
    #region Public properties

    /// <summary>
    /// Gets or sets the field.
    /// </summary>
    /// <value>
    /// The field.
    /// </value>
    [CanBeNull]
    public string Trigger { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// The value.
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

      if (ruleContext.Control != null)
      {
        ruleContext.Control.Load += (s, e) => this.DisableValidationOnSubmit(ruleContext.Control);
      }

      var model = ruleContext.Model as RuleContextModel;
      if (model != null)
      {
        this.DisableValidationOnSubmit(model);
      }
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <param name="fieldItemId">The field item identifier.</param>
    /// <returns></returns>
    protected object GetValue(RuleContextModel model, string fieldItemId)
    {
      var result = model.Form.GetField(fieldItemId) as IFieldResult;
      return result?.GetResult().Value;
    }

    /// <summary>
    /// Disables the validation on submit.
    /// </summary>
    /// <param name="model">The model.</param>
    protected virtual void DisableValidationOnSubmit([NotNull]RuleContextModel model)
    {
      Assert.ArgumentNotNull(model, nameof(model));

      if (this.Match(this.GetValue(model, this.Trigger), this.TriggerValue))
      {
        this.DisableValidation(null, model);
      }
    }

    /// <summary>
    /// Disables the validation.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="model">The model.</param>
    protected void DisableValidation([CanBeNull]Control control, [CanBeNull]RuleContextModel model)
    {
      if (control != null)
      {
        foreach (var validator in control.Controls.Flatten().OfType<BaseValidator>())
        {
          validator.Enabled = false;
        }
      }

      if (model != null)
      {
        var modelState = model.ModelState;
        modelState.Errors.Clear();
      }
    }

    /// <summary>
    /// Called when the field has loaded.
    /// </summary>
    /// <param name="context">The control.</param>
    protected virtual void DisableValidationOnSubmit([NotNull] Control context)
    {
      Assert.ArgumentNotNull(context, "context");

      var trigger = this.GetField(context, this.Trigger) as IResult;

      if (trigger == null)
      {
        return;
      }

      if (this.Match(trigger.Result.Value, this.TriggerValue))
      {
        this.DisableValidation(context, null);
      }
    }

    /// <summary>
    /// Matches the specified current value.
    /// </summary>
    /// <param name="currentValue">The current value.</param>
    /// <param name="patternValue">The pattern value.</param>
    /// <returns></returns>
    protected virtual bool Match(object currentValue, string patternValue)
    {
      var match = new Regex(patternValue ?? string.Empty).Match((currentValue ?? string.Empty).ToString());
      return match.Success;
    }

    #endregion
  }
}