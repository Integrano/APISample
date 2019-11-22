$(function () {
    $.validator.addMethod("dategreaterthan", function (value, element, params) {
        return Date.parse(value) > Date.parse($(params).val());
    });

    $.validator.unobtrusive.adapters.add("dategreaterthan", ["otherpropertyname"], function (options) {
        options.rules["dategreaterthan"] = "#" + options.params.otherpropertyname;
        options.messages["dategreaterthan"] = options.message;
    });

    // on submit payment form submit hide the submit button
    // to prevent user from double submit the form
    $("#submit-form").submit(function () {
        if ($(this).valid()) {
            $('input[type="submit"], input[type="button"], button').hide();
        }
    });
});