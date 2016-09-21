# Morph

The project contains a set of actions for Web Forms for Marketers rule engine [(Rule Set Editor)](https://doc.sitecore.net/web_forms_for_marketers/81/using_web_forms/creating_web_forms/create_a_field_rule_in_the_rule_set_editor) to customize forms fields.

## Client Side Field Actions
The actions enable selection-dependent inputs for the Sitecore Web Forms for Marketers module.

Selection-dependent input requires users to enter additional information related to an initial selection before they can complete a form. In almost all cases, it let make web forms simpler and more responsive to user actions and personalize forms depending on target audiences. 

-	Changing `specific` field to `value` hide element
  
  The selected element is hidden when the specified field matches to the specified value.

-	changing `specific` field to `value` show element

  The selected element is shown when the specified field matches to the specified value.

-	changing `specific` field to `value` clear field value

  The selected element value is cleared when the specified field matches to the specified value.

-	changing `specific` field to `value` copy specific field value

  The selected element value is taken from another field when the specified field matches to the specified value.

-	changing `specific` field to `value` redirect visitor to `specific` page

  The visitor is redirected to the specified page when the specified field matches to the specified value.

-	changing value trigger `event` for `specific` field

  Triggers the specified event for the specified field when value of the selected field is changed. Possible event values – [HTML DOM events](http://www.w3schools.com/jsref/dom_obj_event.asp)
 
-	changing `specific` field to `value` run `client side script`

  Runs client side script when the specified field matches to the specified value.

-	changing `specific` field to `value` `operator` `specific` field to `value` hide element

  The selected element is hidden when the specified fields matches to the specified values.

-	changing `specific` field to `value` `operator` `specific` field to `value` show element

  The selected element is shown when the specified fields matches to the specified values.

-	changing `specific` field to `value` `operator` `specific` field to `value` run `client side script`

  Runs client side script when the specified fields matches to the specified values.

**Note**: prefer to use condition “where true (action always execute)” for all client side actions.

## Server Side Actions
The set of actions extends Web Forms for Marketers fields behavior if the conditions are met.

- disable element
  
  Disables the selected field
  
- set read only element
  
  Makes the selected field readonly

- use the default value from cache `specific` record
  
  Reads the value of the specific cache record and makes it the current field value

- use the default value from session `specific` record
  
  Reads the value of the specific session record and makes it the current field value

- use the default value from visit `specific` field
  
  Reads the value of the specific visit field and makes it the current field value. Allowed visit field name: areacode, businessname, city, country, dns, isp, latitude, longitude, metrocode, postalcode, region, url.
 
- use the default value from `specific` field matching `regex` pattern

  Reads the value of the specific form field, extracts a match according to the specified regex and makes it the current field value

## Compatibility
The extension for the Web Forms for Marketers module supports both ASP.NET Web Forms and MVC engines

## Examples
The example of client side actions usage is described in the [document] (https://github.com/zigor/morph/raw/master/doc/Selection-dependent%20inputs.docx)

