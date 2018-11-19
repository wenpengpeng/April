(function () {
    /**
     * 点击登陆
     */
    $("#login").click(function () {
        var returnUrl = $("#returnUrl").val();
        var userName = $("#userName").val();
        var password = $("#password").val();
        $.ajax({
            type: "POST",
            url: "/Account/Login",
            dataType: "json",
            data: {
                usernameOrPhoneNumber: userName,
                password: password,
                returnUrl:returnUrl
            },
            success: function (result) {
                if (result.successed) {
                    location.href = returnUrl;
                } else {
                    alert(result.message);
                }
            },
            error: function(result) {
                alert(result.message);
            }
        });
    });
})();