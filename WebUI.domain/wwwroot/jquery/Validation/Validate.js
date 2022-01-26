        // First Name
        $(document).ready(function () {
            $(".firstName").focus(function () {
                var fName = $(".firstName").val();
                if (fName == "") {
                    $("#firstNameError").show();
                    $("#firstNameError").html("First Name is Empty");
                    $(".firstName").css({ "border": "2px solid red" });
                    $("#firstNameError").css({ "color": "red" });
                    $(".submit").attr("disabled", true);
                }
                else if (fName.length < 2) {
                    $("#firstNameError").show();
                    $("#firstNameError").html("First Name cannot be less than 2");
                    $(".firstName").css({ "border": "2px solid red" });
                    $("#firstNameError").css({ "color": "red" });
                    $(".submit").attr("disabled", true);
                }
                else {
                    $("#firstNameError").hide();
                    $("#firstNameError").html("");
                    $(".firstName").css({ "border": "2px solid blue" });
                    $("#firstNameError").css({ "color": "black" });
                    $(".submit").attr("disabled", false);
                }
            });
            $(".firstName").blur(function () {
                $("#firstNameError").hide();
                $(".firstName").css({ "border": "2px solid blue" });
                $(".submit").attr("disabled", false);
            });
        });


        // Last Name
        $(document).ready(function () {
            $(".lastName").focus(function () {
                var lName = $(".lastName").val();
                if (lName == "") {
                    $("#lastNameError").show();
                    $("#lastNameError").html("Last Name is Empty");
                    $(".lastName").css({ "border": "2px solid red" });
                    $("#lastNameError").css({ "color": "red" });
                    $(".submit").attr("disabled", true);
                }
                else if (lName.length < 2) {
                    $("#lastNameError").show();
                    $("#lastNameError").html("First Name cannot be less than 2");
                    $(".lastName").css({ "border": "2px solid red" });
                    $("#lastNameError").css({ "color": "red" });
                    $(".submit").attr("disabled", true);
                }
                else {
                    $("#lastNameError").hide();
                    $("#lastNameError").html("");
                    $(".lastName").css({ "border": "2px solid blue" });
                    $("#lastNameError").css({ "color": "black" });
                    $(".submit").attr("disabled", false);
                }
            });
            $(".lastName").blur(function () {
                $("#lastNameError").hide();
                $(".lastName").css({ "border": "2px solid blue" });
                $(".submit").attr("disabled", false);
            });
        });


        // Username
        $(document).ready(function () {
            $(".userName").focus(function () {
                var uName = $(".userName").val();
                if (uName == "") {
                    $("#userNameError").show();
                    $("#userNameError").html("UserName is Empty");
                    $(".userName").css({ "border": "2px solid red" });
                    $("#userNameError").css({ "color": "red" });
                    $(".submit").attr("disabled", true);
                }
                else if (uName.length < 2) {
                    $("#userNameError").show();
                    $("#userNameError").html("UserName cannot be less than 2");
                    $(".userName").css({ "border": "2px solid red" });
                    $("#userNameError").css({ "color": "red" });
                    $(".submit").attr("disabled", true);
                }
                else {
                    $("#userNameError").hide();
                    $("#userNameError").html("");
                    $(".userName").css({ "border": "2px solid blue" });
                    $("#userNameError").css({ "color": "black" });
                    $(".submit").attr("disabled", false);
                }
            });
            $(".userName").blur(function () {
                $("#userNameError").hide();
                $(".userName").css({ "border": "2px solid blue" });
                $(".submit").attr("disabled", false);
            });
        });


        // Email
        $(document).ready(function () {
            $(".email").focus(function () {
                var email = $(".email").val();
                var filter = /^([a-zA-z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z]{2,4})+$/;
                if (email == "") {
                    $("#emailError").show();
                    $("#emailError").html("Email is Empty");
                    $(".email").css({ "border": "2px solid red" });
                    $("#email").css({ "color": "red" });
                    $(".submit").attr("disabled", true);
                }
                else if (!filter.test(email)) {
                    $("#emailError").show();
                    $("#emailError").html("Invalid Email Format");
                    $(".email").css({ "border": "2px solid red" });
                    $("#emailError").css({ "color": "red" });
                    $(".submit").attr("disabled", true);
                }
                else {
                    $("#emaiError").hide();
                    $("#emailError").html("");
                    $(".email").css({ "border": "2px solid blue" });
                    $("#emailError").css({ "color": "black" });
                    $(".submit").attr("disabled", false);
                }
            });
            $(".email").blur(function () {
                $("#emailError").hide();
                $(".email").css({ "border": "2px solid blue" });
                $(".submit").attr("disabled", false);
            });
        });


        // Birthday
        // Gender
$(document).ready(function () {
    $(".gender").focus(function () {
        var gender = $(".userName").val();
        if (gender.values = "Select A Gender") {
            $("#genderError").show();
            $("#genderError").html("Select the right Gender");
            $(".gender").css({ "border": "2px solid red" });
            $("#genderError").css({ "color": "red" });
            $(".submit").attr("disabled", true);
        }
        else {
            $("#genderError").hide();
            $("#genderError").html("");
            $(".gender").css({ "border": "2px solid blue" });
            $("#genderError").css({ "color": "black" });
            $(".submit").attr("disabled", false);
        }
    });
    $(".gender").blur(function () {
        $("#genderError").hide();
        $(".gender").css({ "border": "2px solid blue" });
        $(".submit").attr("disabled", false);
    });
});

        // Account Type
