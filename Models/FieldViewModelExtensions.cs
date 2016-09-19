namespace Morph.Forms.Models
{
  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Form.Core.Configuration;
  using Sitecore.Forms.Core.Data;
  using Sitecore.Forms.Mvc.ViewModels;

  public static class FieldViewModelExtensions
  {
    /// <summary>
    /// Gets the conditions.
    /// </summary>
    /// <param name="fieldViewModel">The field view model.</param>
    /// <returns></returns>
    [CanBeNull]
    public static string GetConditions([NotNull]this FieldViewModel fieldViewModel)
    {
      Assert.ArgumentNotNull(fieldViewModel, nameof(fieldViewModel));

      var item = StaticSettings.ContextDatabase?.GetItem(fieldViewModel.FieldItemId);
      if (item == null)
      {
        return null;
      }

      return new FieldItem(item).Conditions;
    }
  }
}