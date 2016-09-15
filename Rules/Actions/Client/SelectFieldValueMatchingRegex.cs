using Morph.Forms.JsCode;

namespace Morph.Forms.Rules.Actions.Client
{
  using System;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Web.UI;

  using Morph.Forms.Web.UI;
  using Sitecore.Forms.Mvc.ViewModels;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Form.Web.UI.Controls;
  using Sitecore.Forms.Core.Rules;
  using SysListControl = System.Web.UI.WebControls.ListControl;

  /// <summary>
  /// Defines the select field value matching regex class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <seealso cref="Morph.Forms.Rules.Actions.Client.ClientAction{T}" />
  public class SelectFieldValueMatchingRegex<T> : ClientAction<T> where T : ConditionalRuleContext
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

      if (string.IsNullOrEmpty(this.FieldId) || 
          string.IsNullOrEmpty(this.Pattern))
      {   
        return;
      }

      if (ruleContext.Control != null)
      {
        ruleContext.Control.Load += (s, e) => this.CopyMatchingValue(ruleContext.Control);
        return;
      }

      base.Apply(ruleContext);
    }

    /// <summary>
    /// Prepares the script.
    /// </summary>
    /// <param name="fieldViewModel">The field view model.</param>
    /// <returns></returns>
    protected override string PrepareScript(FieldViewModel fieldViewModel)
    {
      var value = new JsCodeBuilder()
              .AddSelectorByNameFromHiddenValue(this.FieldId)
              .AddFind()
              .AddGetValue()
              .AddMatchRegexp(this.Pattern)
              .ToString();

      return new JsCodeBuilder()
              .AddSelectorByNameFromHiddenValue(fieldViewModel.FieldItemId)
              .AddFind()
              .AddSetValue(value)
              .ToString();
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
      string value = match.Success ? match.Value.Trim() : string.Empty;

      target.DefaultValue = this.ConvertToFieldSpecificValue(target, value);
    }

    /// <summary>
    /// Converts to field specific value.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    protected virtual string ConvertToFieldSpecificValue(IResult target, string value)
    {
      var field = target as ListControl;

      if (field != null)
      {
        var item = field.Items.FindByValue(value) ?? field.Items.FindByText(value);

        if (item != null)
        {
          var list = field.Controls.Flatten().OfType<SysListControl>().FirstOrDefault();
          if (list != null)
          {
            list.ClearSelection();
            list.SelectedValue = item.Value;
          }

          return item.Value;
        }
      }

      return value;
    }

    #endregion
  }
}