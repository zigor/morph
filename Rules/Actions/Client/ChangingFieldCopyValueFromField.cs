namespace Morph.Forms.Rules.Actions.Client
{
  using Morph.Forms.Web.UI;
  using Morph.Forms.JsCode;
  using Sitecore.StringExtensions;

  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the changing field copy value from field class.
  /// </summary>
  public sealed class ChangingFieldCopyValueFromField<T> : ChangingFieldRunClientAction<T> where T : ConditionalRuleContext
  {
    /// <summary>
    /// The context
    /// </summary>
    private T context;

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply([NotNull]T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (string.IsNullOrEmpty(this.Holder))
      {
        return;
      }

      this.context = ruleContext;


      base.Apply(ruleContext);
    }

    /// <summary>
    /// Gets or sets the holder.
    /// </summary>
    /// <value>
    /// The holder.
    /// </value>
    [CanBeNull]
    public string Holder { get; set; }

    /// <summary>
    /// Builds the client script.
    /// </summary>
    /// <returns>
    /// The client script.
    /// </returns>
    [NotNull]
    protected override string BuildClientScript()
    {
      var holderSelector = this.GetClientElementSelector(this.GetField(this.context.Control, this.Holder), this.Holder);
      
      if (string.IsNullOrEmpty(holderSelector))
      {
        return string.Empty;
      }

      return "$(this).val({0});".FormatWith(InlineJs.GetValue.FormatWith(holderSelector));
    }
  }
}