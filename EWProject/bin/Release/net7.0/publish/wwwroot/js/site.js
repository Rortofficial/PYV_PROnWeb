// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function formartNumber(id) {
    try {
        var tmpNumber = $("#" + id).val();
        console.log(tmpNumber);
        if (isNumber(tmpNumber) == false) {
            $("#" + id).val(tmpNumber);
            return;
        } else {
            $("#" + id).val(parseFloat(tmpNumber, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,"));
        }
    } catch (e) {
        alert("Incorect Number!");
    }
}
function ConvertFormartNumberToNumber(value) {
    return parseFloat(value.replaceAll(",", ""));
}

function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}