var loginController = (function () {
    var initialize = function () {

    };

    var registerEvent = function () {
        $('#btnSubmit').on('click', function () {
            var userName = $('#txtUserName').val();
            var password = $('#txtPassword').val();
            loginController.login(userName, password, function (response) {

            }, function (error) {

            });
        });
    };

    var login = function (user, pass, successCallBack, errorCallback) {
        $.ajax({
            type: 'POST',
            data: {
                username: user,
                password: pass
            },
            dataType: 'json',
            url: '/admin/login/authen',
            success: successCallBack,
            errorCallback: errorCallback
        });
    };
    return {
        initialize: initialize,
        login: login,
        registerEvent: registerEvent
    };
})();



