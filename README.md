# Morph
## Selection-dependent input for Web Forms For Marketers

Selection-dependent inputs require users to enter additional information related to an initial selection before they can complete a form. In almost all cases, it let make web forms simpler and more responsive to user actions and personalize forms depending on target audiences. 

Selection-dependent input requires forms to be dynamic on the client side.  By default, the [Web Forms for Marketers](http://sdn.sitecore.net/Products/Web Forms for Marketers/Web Forms for Marketers 2,-d-,3.aspx) module does not provide this behavior but it provides the field rules engine [(5.2 Configuring Web Form Fields)](http://sdn.sitecore.net/upload/sdn5/products/web_forms2/web forms for marketers v2_3 user guide-usletter.pdf) to customize forms fields.

## Client Side Field Actions
Selection-dependent input is implemented as a set of Web Forms for Marketers field actions. 

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

## Compatibility
The extensions for the Web Forms for Marketers module supports both ASP.NET Web Forms and MVC engines
