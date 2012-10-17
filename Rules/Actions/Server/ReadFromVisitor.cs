namespace Morph.Forms.Rules.Actions.Server
{
  using Sitecore;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.DataAccess;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Core.Rules;

  /// <summary>
  /// Defines the read from visitor class.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class ReadFromVisitor<T> : ReadValue<T> where T : ConditionalRuleContext
  {
    #region Methods

    /// <summary>
    /// Applies the specified rule context.
    /// </summary>
    /// <param name="ruleContext">The rule context.</param>
    public override void Apply([NotNull]T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");

      if (Tracker.Visitor == null)
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
    [CanBeNull]
    protected override object GetValue()
    {      
      Tracker.Visitor.Load(new VisitorLoadOptions { Options = VisitorOptions.Visitor });

      var row = Tracker.Visitor.DataSet.Visitors.FindByVisitorId(Tracker.Visitor.VisitorId);
      return row[this.Name];
    }

    #endregion
  }
}