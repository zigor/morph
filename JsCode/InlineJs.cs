using Sitecore.StringExtensions;

namespace Morph.Forms.JsCode
{
  /// <summary>
  /// Js code builder
  /// </summary>
  public class InlineJs
  {
    /// <summary>
    /// The selector by identifier
    /// </summary>
    public static readonly string SelectorById = "#{0}";

    /// <summary>
    /// The selector by name from hidden value
    /// </summary>
    public static readonly string SelectorByNameFromHiddenValue = "[name=\"' + ($('input:hidden[value=\"{0}\"]').attr('name') || '').replace(/.Id$/, '.Value') + '\"]";

    /// <summary>
    /// The get value
    /// </summary>
    public static readonly string GetValue = "$('{0}').filter(function(i, e){{ return $(e).is(':radio, :checkbox') ? $(e).is(':checked') : true }}).val()";

    /// <summary>
    /// The on change to value execute code
    /// </summary>
    public static readonly string OnChangeToValueForElementExecuteCode = "$('{0}').on('change morph:change', function(){{ if(new RegExp('{1}').test($(this).filter(function(i, e){{ return $(e).is(':radio, :checkbox') ? $(e).is(':checked') : true }}).val())) {{ $.each([$('{2}')], function(){{ {3} }}) }}}}).trigger('morph:change');";

    /// <summary>
    /// The on change elements to values for element execute code
    /// </summary>
    public static readonly string OnChangeElementsToValuesForElementExecuteCode = "$('{0}, {2}').on('change morph:change',function(){{ if(new RegExp('{1}').test($('{0}').filter(function(i, e){{ return $(e).is(':radio, :checkbox') ? $(e).is(':checked') : true }}).val()) {4} new RegExp('{3}').test($('{2}').filter(function(i, e){{ return $(e).is(':radio, :checkbox') ? $(e).is(':checked') : true }}).val())) {{ $.each([$('{5}')], function(){{ {6} }}) }}}}).trigger('morph:change');";
    
    /// <summary>
    /// The safe script wrapping
    /// </summary>
    public static readonly string SafeScriptWrapping = "<script type=\"text/javascript\">(function($){{  var rules=function() {{ {0} }};rules();$(document).ajaxSuccess(function(e, xhr, o, d) {{if (o.url.indexOf('form/Index?wffm') > 0) {{ $=jQuery;rules(); }} }})}})(window.$scw || jQuery);</script>";
  }
}