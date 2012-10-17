namespace Morph.Forms.Rules.Actions.Server
{
  using System.Text.RegularExpressions;
  using System.Web.UI;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Form.Web.UI.Controls;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the select field value matching regex class.
  /// </summary>
  public class SelectFieldValueMatchingRegex<T> : FieldAction<T> where T : ConditionalRuleContext
  {
    #region Public properties

    /// <summary>
    /// Gets or sets the field.
    /// </summary>
    /// <value>
    /// The field.
    /// </value>
    [CanBeNull]
    public string FieldId { get; set; }

    /// <summary>
    /// Gets or sets the regex.
    /// </summary>
    /// <value>
    /// The regex.
    /// </value>
    [CanBeNull]
    public string Pattern { get; set; }

    #endregion

    #region Public methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply([NotNull]T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (ruleContext.Control == null || 
          string.IsNullOrEmpty(this.FieldId) || 
          string.IsNullOrEmpty(this.Pattern))
      {
        return;
      }

      ruleContext.Control.Load += (s, e) => this.CopyMatchingValue(ruleContext.Control);     
    }

    /// <summary>
    /// Copies the matching value.
    /// </summary>
    /// <param name="control">The control.</param>
    protected virtual void CopyMatchingValue([NotNull]Control control)
    {
      Assert.ArgumentNotNull(control, "control");

      var holder = (IResult)this.GetField(control, this.FieldId);
      var target = (IResult)control;

      if (holder == null)
      {
        return;
      }

      var match = Regex.Match(holder.Result.Value.ToString(), this.Pattern);
      
      target.DefaultValue = match.Success ? match.Value.Trim() : string.Empty;
    }

    #endregion
  }
}