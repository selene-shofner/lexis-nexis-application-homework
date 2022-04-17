// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function replaceWindowsNewlinesWithHtmlLinebreaks(value) {
    return value.replace(/\r\n/g, "<br />").replace(/&#xD;&#xA;/g, "<br />");

}