/*
 *  Project:      MyStoryBuddy
 *  Description:  Show user stories directly in the page.
 *  Author:       Scott Brooks
 */
// Example pulled from: http://jqueryboilerplate.com/
// Link to some other references for jquery plugins: http://stackoverflow.com/questions/10638948/extremely-simple-jquery-plugin-tutorial
// the semi-colon before function invocation is a safety net against concatenated
// scripts and/or other plugins which may not be closed properly.
; (function ($, window, document, undefined) {

    // undefined is used here as the undefined global variable in ECMAScript 3 is
    // mutable (ie. it can be changed by someone else). undefined isn't really being
    // passed in so we can ensure the value of it is truly undefined. In ES5, undefined
    // can no longer be modified.

    // window and document are passed through as local variable rather than global
    // as this (slightly) quickens the resolution process and can be more efficiently
    // minified (especially when both are regularly referenced in your plugin).

    // Create the defaults once
    var pluginName = "myStoryBuddy",
        defaults = {
            propertyName: "value"
        };

    // The actual plugin constructor
    function Plugin(element, options) {
        this.element = element;
        //console.log('Plugin');
        //console.dir(element);
        // jQuery has an extend method which merges the contents of two or
        // more objects, storing the result in the first object. The first object
        // is generally empty as we don't want to alter the default options for
        // future instances of the plugin
        this.options = $.extend({}, defaults, options);

        this._defaults = defaults;
        this._name = pluginName;

        this.init();
    }

    Plugin.prototype = {

        init: function () {
            var ul = document.createElement("ul");
            ul.setAttribute("id", "msbAssert");
            //ul.appendChild(document.createElement("msbAssert"));
            // document.getElementById("story").appendChild(ul);
            this.element.appendChild(ul);

            var body = "";
            body += "<div id='msb_main' class='container'>";
            body += "  <div class='row'>";
            body += "    <div id='siteNotes' class='span12'>";
            body += "      <div class='text-center'>";
            body += "        <span class='pull-left'><b>Notes</b> Add</span>";
            body += "        (Everything below this line will not be displaed on the live site)";
            body += "      </div>";
            body += "      <br />";
            body += "      <div id='msbContent' />";
            body += "    </div>";
            body += "  </div>";
            body += "</div>";
            $(this.element).append(body);
            $('#msbContent').load('/Story/list', '#msbContent');
        },
    };

    // A really lightweight plugin wrapper around the constructor,
    // preventing against multiple instantiations
    $.fn[pluginName] = function (options) {
        return this.each(function () {
            if (!$.data(this, "plugin_" + pluginName)) {
                $.data(this, "plugin_" + pluginName, new Plugin(this, options));
            }
        });
    };

})(jQuery, window, document);