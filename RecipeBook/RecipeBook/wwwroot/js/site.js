function replaceWindowsNewlinesWithHtmlLinebreaks(value) {
    return value.replace(/\r\n/g, "<br />").replace(/&#xD;&#xA;/g, "<br />");
}

function addLineBreaks(targetElementSelector, textToConvert) {
    $(targetElementSelector).html(replaceWindowsNewlinesWithHtmlLinebreaks(textToConvert));
} 