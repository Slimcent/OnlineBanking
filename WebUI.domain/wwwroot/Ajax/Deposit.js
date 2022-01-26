$(document).ready(function () {
    $(".btnDeposit").click(function (e) {
        e.preventDefault();
        var mydata = $("form").serialize();
        $.ajax({
            type: "POST",
            //url: '@Url.Action("Deposit", "Transaction")',
            url: "/Transaction/Deposit",
            data: mydata,
            //data: {
            //    Amount: $("#amount").val()
            //},
            //success: function (res) {
            //    if (res.success) {
            //        alert("Successful");
            //    }
            //    else {
            //        alert("Unsuccessful");
            //    }
            //},
        });
    });
});