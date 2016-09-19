namespace Morph.Forms.Models
{
  using System.Linq;
  using Sitecore.Diagnostics;
  using Sitecore.Forms.Mvc.ViewModels;

  /// <summary>
  /// Field view model extensions
  /// </summary>
  internal static class FormViewModelExtensions
  {
    /// <summary>
    /// Gets the field.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <param name="fieldItemId">The field item identifier.</param>
    /// <returns></returns>
    public static FieldViewModel GetField(this FormViewModel model, string fieldItemId)
    {
      Assert.ArgumentNotNull(model, nameof(model));
      return model.Sections.SelectMany(s => s.Fields).FirstOrDefault(f => f.FieldItemId == fieldItemId);
    }
  }
}