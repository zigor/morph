using Sitecore.StringExtensions;

namespace Morph.Forms.JsCode
{
  /// <summary>
  ///   js code snippets
  /// </summary>
  public static class JsCodeSnippets
  {
    /// <summary>
    /// Selectors the name of the by.
    /// </summary>
    /// <param name="elementNameValue">The element name value.</param>
    /// <returns></returns>
    public static string SelectorByName(string elementNameValue)
    {
      return "[name =\"{0}\"]".FormatWith(elementNameValue);
    }

    /// <summary>
    /// Selectors the by identifier.
    /// </summary>
    /// <returns></returns>
    public static string SelectorById(string elementId)
    {
      return "#{0}".FormatWith(elementId);
    }

    /// <summary>
    /// Selectors the by hidden value.
    /// </summary>
    /// <param name="hiddenElementValue">The hidden element value.</param>
    /// <returns></returns>
    public static string SelectorByHiddenValue(string hiddenElementValue)
    {
      return "input:hidden[value=\"{0}\"]".FormatWith(hiddenElementValue);
    }

    /// <summary>
    /// Selectors the by name from hidden value.
    /// </summary>
    /// <param name="hiddenElementValue">The hidden element value.</param>
    /// <returns></returns>
    public static string SelectorByNameFromHiddenValue(string hiddenElementValue)
    {
      return SelectorByName("$('{0}').val().replace(/.Id+$/, '.Value')".FormatWith(SelectorByHiddenValue(hiddenElementValue)));
    }

    /// <summary>
    /// On the change event executes js code.
    /// </summary>
    /// <param name="triggerId">The trigger identifier.</param>
    /// <param name="triggerName">Name of the trigger.</param>
    /// <param name="triggerValuePattern">The trigger value pattern.</param>
    /// <param name="observerId">The observer identifier.</param>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    public static string OnChangeExecte(string triggerId, string triggerName, string triggerValuePattern, string observerId, string code)
    {
      return "$scw('{0}').change(function(){{ (function d($, p) {{ var el = $('{1}'); if (new RegExp('{2}').test($(el.filter(':checked')[0] || ($(el[0]).is(':checkbox') ? $() : el[0])).val() || '')) {{ $scw.each([$scw('{3}')], function(){{{4}}})}}}}).apply(this, [$scw]) }}).triggerHandler('change');"
        .FormatWith(triggerId, triggerName, triggerValuePattern, observerId, code);
    }

    /// <summary>
    /// On the change event triggers an event.
    /// </summary>
    /// <param name="triggerId">The trigger identifier.</param>
    /// <param name="observerId">The observer identifier.</param>
    /// <param name="eventName">Name of the event.</param>
    /// <returns></returns>
    public static string OnChangeTrigger(string triggerId, string observerId, string eventName)
    {
      return "$scw('{0}').change(function() {{$scw('{1}').trigger('{2}', [$scw(this).val()])}})"
        .FormatWith(triggerId, observerId, eventName);
    }
  }
}