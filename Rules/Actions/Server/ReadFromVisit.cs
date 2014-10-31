namespace Morph.Forms.Rules.Actions.Server
{
  using System.Globalization;

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

      if (!Tracker.IsActive || Tracker.Current.Interaction == null)
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
      switch (this.Name.ToLower(CultureInfo.InvariantCulture))
      {
        case "areacode":
          return Tracker.Current.Interaction.GeoData.AreaCode;
        case "businessname":
          return Tracker.Current.Interaction.GeoData.BusinessName;
        case "city":
          return Tracker.Current.Interaction.GeoData.City;
        case "country":
          return Tracker.Current.Interaction.GeoData.Country;
        case "dns":
          return Tracker.Current.Interaction.GeoData.Dns;
        case "isp":
          return Tracker.Current.Interaction.GeoData.Isp;
        case "latitude":
          return Tracker.Current.Interaction.GeoData.Latitude;
        case "longitude":
          return Tracker.Current.Interaction.GeoData.Longitude;
        case "metrocode":
          return Tracker.Current.Interaction.GeoData.MetroCode;
        case "postalcode":
          return Tracker.Current.Interaction.GeoData.PostalCode;
        case "region":
          return Tracker.Current.Interaction.GeoData.Region;
        case "url":
          return Tracker.Current.Interaction.GeoData.Url;
      }
      return string.Empty;
    }

    #endregion
  }
}