using Sitecore;
using Sitecore.StringExtensions;

namespace Morph.Forms.Pipelines.RenderRendering
{
  using System.IO;
  using Sitecore.Diagnostics;
  using Sitecore.Mvc.Pipelines.Response.RenderRendering;
  using Sitecore.Mvc.Presentation;
  using Morph.Forms.Configuration;
  using Morph.Forms.JsCode;

  /// <summary>
  /// Add scripts for mvc form renderings
  /// </summary>
  /// <seealso cref="Sitecore.Mvc.Pipelines.Response.RenderRendering.RenderRenderingProcessor" />
  public class AddScriptsForMvcFormRendering : RenderRenderingProcessor
  {
    /// <summary>
    /// Processes the specified arguments.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public override void Process([NotNull]RenderRenderingArgs args)
    {
      Assert.ArgumentNotNull(args, nameof(args));
      
      Renderer renderer = args.Rendering.Renderer;
      if (!args.Rendered || renderer == null)
      {
        return;
      }

      this.Render(args.Rendering, args.Writer);
    }

    /// <summary>
    /// Renders the specified renderer.
    /// </summary>
    /// <param name="rendering">The rendering.</param>
    /// <param name="writer">The writer.</param>
    protected virtual void Render(Rendering rendering, TextWriter writer)
    {
      var script = rendering.Properties[Constants.MvcWebFormRulesScriptKey];
      if (string.IsNullOrEmpty(script))
      {
        return;
      }
      var onStartupScript = InlineJs.SafeScriptWrapping.FormatWith(script);

      writer.Write(onStartupScript);      
    }
  }
}