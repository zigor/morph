using System.Web.Mvc;

namespace Morph.Forms.Models
{
  using Sitecore.Forms.Mvc.ViewModels;

  public class RuleContextModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RuleContextModel"/> class.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <param name="form">The form.</param>
    /// <param name="modelState">State of the model.</param>
    public RuleContextModel(FieldViewModel model, FormViewModel form, ModelState modelState)
    {
      this.Model = model;
      this.Form = form;
      this.ModelState = modelState;
    }

    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    /// <value>
    /// The model.
    /// </value>
    public FieldViewModel Model { get; private set; }

    /// <summary>
    /// Gets or sets the form.
    /// </summary>
    /// <value>
    /// The form.
    /// </value>
    public FormViewModel Form { get; private set; }

    /// <summary>
    /// Gets the state of the model.
    /// </summary>
    /// <value>
    /// The state of the model.
    /// </value>
    public ModelState ModelState { get; private set; }
  }
}