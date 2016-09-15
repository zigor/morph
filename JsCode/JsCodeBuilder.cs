using Sitecore.StringExtensions;

namespace Morph.Forms.JsCode
{
  using System.Text;

  /// <summary>
  /// Js code builder
  /// </summary>
  public class JsCodeBuilder
  {
    /// <summary>
    /// The js code
    /// </summary>
    private readonly StringBuilder jsCode;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsCodeBuilder"/> class.
    /// </summary>
    public JsCodeBuilder()
    {
      this.jsCode = new StringBuilder();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsCodeBuilder" /> class.
    /// </summary>
    /// <param name="jsScript">The js script.</param>
    public JsCodeBuilder(string jsScript) : this()
    {
      this.jsCode.Append(jsScript);
    }
    
    /// <summary>
    /// Adds the selector by identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public JsCodeBuilder AddSelectorById(string id)
    {
      this.jsCode.AppendFormat("#{0}", id);
      return this;
    }

    /// <summary>
    /// Selectors the name of the by.
    /// </summary>
    /// <param name="elementNameValue">The element name value.</param>
    /// <returns></returns>
    public JsCodeBuilder AddSelectorByName(string elementNameValue)
    {
      this.jsCode.AppendFormat("[name =\"{0}\"]", elementNameValue);
      return this;
    }

    /// <summary>
    /// Adds the selector by name from hidden value.
    /// </summary>
    /// <param name="elementNameValue">The element name value.</param>
    /// <returns></returns>
    public JsCodeBuilder AddSelectorByNameFromHiddenValue(string elementNameValue)
    {     
      this.jsCode.AppendFormat("[name=\"' + $('input:hidden[value=\"{0}\"]').attr('name').replace(/.Id$/, '.Value') + '\"]", elementNameValue);
      return this;
    }
    
    /// <summary>
    /// Finds this instance.
    /// </summary>
    /// <returns></returns>
    public JsCodeBuilder AddFind()
    {
      this.jsCode.Insert(0, "$('");
      this.jsCode.Append("')");

      return this;
    }

    /// <summary>
    /// Adds the read only.
    /// </summary>
    /// <returns></returns>
    public JsCodeBuilder AddMakeElementReadOnly()
    {
      this.jsCode.Append(".prop('readonly', true);");
      return this;
    }

    /// <summary>
    ///   Adds the scoupe.
    /// </summary>
    /// <returns></returns>
    public JsCodeBuilder AddScope()
    {
      this.jsCode.Insert(0, "(function($){");
      this.jsCode.Append("})(window.$scw || jQuery);");
      return this;
    }

    /// <summary>
    /// Adds the dome ready.
    /// </summary>
    /// <returns></returns>
    public JsCodeBuilder AddDomeReady()
    {
      this.jsCode.Insert(0, "$(function(){var rules = function(){ ");
      this.jsCode.Append("}; rules();$(document).ajaxSuccess(function(e, xhr, o, d) {if (o.url.indexOf('form/Index?wffm') > 0) { $=jQuery;rules();}})});");
      return this;
    }

    /// <summary>
    /// Adds the script tag.
    /// </summary>
    /// <returns></returns>
    public JsCodeBuilder AddScriptTag()
    {
      this.jsCode.Insert(0, "<script type=\"text/javascript\">");
      this.jsCode.Append("</script>");
      return this;
    }

    /// <summary>
    /// Adds the get value.
    /// </summary>
    /// <returns></returns>
    public JsCodeBuilder AddGetValue()
    {
      this.jsCode.Append(".filter(function(i, e){return $(e).is(':radio, :checkbox') ? $(e).is(':checked') : true}).val()");
      return this;
    }

    /// <summary>
    /// Adds the set value.
    /// </summary>
    /// <returns></returns>
    public JsCodeBuilder AddSetValue(string value)
    {
      this.jsCode.AppendFormat(".val({0});", value);
      return this;
    }

    /// <summary>
    /// Adds the match regexp.
    /// </summary>
    /// <param name="pattern">The pattern.</param>
    /// <returns></returns>
    public JsCodeBuilder AddMatchRegexp(string pattern)
    {
      this.jsCode.AppendFormat(".match('{0}')", pattern);
      return this;
    }

    /// <summary>
    /// Adds the make element disabled.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public JsCodeBuilder AddMakeElementDisabled()
    {
      this.jsCode.Append(".prop('disabled', true);");
      return this;
    }

    /// <summary>
    /// Adds the change this context.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    public JsCodeBuilder ExecuteWithElementInThis(string code)
    {
      this.jsCode.Insert(0, "$.each([");

      this.jsCode.AppendFormat("], function(){{ {0} }})", code);
      return this;
    }

    /// <summary>
    /// Adds the on change to value execute.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="execute">The execute.</param>
    public JsCodeBuilder AddOnChangeToValueExecute(string value, string execute)
    {
      this.jsCode.AppendFormat(".change(function(){{ if( new RegExp('{0}').test($(this).filter(function(i, e){{ return $(e).is(':radio, :checkbox') ? $(e).is(':checked') : true }}).val())) {{ {1} }}}}).trigger('change');", value, execute);
      return this;
    }

    /// <summary>
    /// Adds the trigger event.
    /// </summary>
    /// <param name="eventName">Name of the event.</param>
    /// <returns></returns>
    public JsCodeBuilder AddTriggerEvent(string eventName)
    {
      this.jsCode.AppendFormat(".trigger({0});", eventName);
      return this;
    }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return this.jsCode.ToString();
    }
  }
}