namespace Morph.Forms.Rules.Actions.Server
{
  using Sitecore.Analytics;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the read from visit class.
  /// </summary>
  public class ReadFromVisit<T>: ReadValue<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (Tracker.CurrentVisit == null)
      {
        return;
      }

      base.Apply(ruleContext);
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <returns>
    /// The value.
    /// </returns>
    protected override object GetValue()
    {
      return Tracker.CurrentVisit[this.Name];
    }

    #endregion
  }
}