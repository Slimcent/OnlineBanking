$(document).ready(function () {
    val1 = "5";
    val2 = "2";

    $.ajax({
            type: "POST",
            url: "/Home/Add",
            data: {
                num1: val1,
                num2: val2
            },
        success: function (msg) {
            console.log(msg);
        },
        error: function (req, status, error) {
            console.log(msg);
        }
    });
});