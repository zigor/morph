using System.Web.Mvc;
using System.Web.Routing;
using Morph.Forms.Configuration;
using Morph.Forms.Models;
using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Helpers;
using Sitecore.Forms.Mvc.ViewModels;

namespace Morph.Forms.Filters
{
  /// <summary>
  ///   Applyes rules to form view model
  /// </summary>
  /// <seealso cref="System.Web.Mvc.ActionFilterAttribute" />
  public class ApplyRulesToFormViewModelAttribute : ActionFilterAttribute
  {
    /// <summary>
    ///   Called by the ASP.NET MVC framework before the action method executes.
    /// </summary>
    /// <param name="filterContext">The filter context.</param>
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      base.OnActionExecuting(filterContext);

      if (filterContext.HttpContext.Request.HttpMethod != "POST")
      {
        return;
      }

      var model = filterContext.ActionParameters["formViewModel"] as FormViewModel;

      if (model != null)
      {
        ReadFromViewData(model, filterContext.Controller.ViewData);
      }
    }

    /// <summary>
    /// Reads from view data.
    /// </summary>
    /// <param name="formViewModel">The field view model.</param>
    /// <param name="viewData">The view data.</param>
    private static void ReadFromViewData(FormViewModel formViewModel, ViewDataDictionary viewData)
    {
      Assert.ArgumentNotNull(formViewModel, nameof(formViewModel));
      Assert.ArgumentNotNull(viewData, nameof(viewData));

      for (int i = 0; i < formViewModel.Sections.Count; ++i)
      {
        var section = formViewModel.Sections[i];
        for (int j = 0; j < section.Fields.Count; ++j)
        {
          var field = section.Fields[j];

          string viewDataKey = $"{formViewModel.ClientId}.Sections[{i}].Fields[{j}].Value";
          if (viewData.ModelState.ContainsKey(viewDataKey))
          {           
            var conditions = field.GetConditions();
            if (!string.IsNullOrEmpty(conditions))
            {
              RulesManager.RunRules(field.GetConditions(), new RuleContextModel(field, formViewModel, viewData.ModelState[viewDataKey]));
            }
          }
        }
      }
    }
  }
}