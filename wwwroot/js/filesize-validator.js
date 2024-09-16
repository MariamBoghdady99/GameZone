$(document).ready(function () {
    $.validator.addMethod('fileSize', function (value, element, param) {
        return this.optional(element) || element.files[0].size <= param;
    }, "File size must be less than {0}");
});