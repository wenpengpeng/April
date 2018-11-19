var april = april || {};
(function ($) {
    var showMessage = function (type, message, title) {
        if (!title) {
            title = message;
            message = undefined;
        }
        return swal({
            title: title,
            type: type,
            text: message,
            showCancelButton: false,
            closeOnConfirm: false
        });
    };
    april.info = function(message, title) {
        return showMessage("info", message, title);
    };
    april.error = function(message, title) {
        return showMessage("error", message, title);
    };
    april.warn = function(message, title) {
        return showMessage("warning", message, title);
    };
    april.confirm = function(message, title, successCallBack, errorCallBack) {
        if (!title) {
            title = message;
            message = undefined;
        }
        return swal({
                title: title,
                text: message,
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                closeOnConfirm: false,
                closeOnCancel: false
            },
            function(isConfirm) {
                if (isConfirm) {
                    if (successCallBack) {
                        successCallBack();
                    }
                } else {
                    if (errorCallBack) {
                        errorCallBack();
                    }
                }
            });
    };
    april.notify = april.notify || {};
    toastr.options = {
        closeButton: false,
        debug: false,
        progressBar: false,
        positionClass: "toast-bottom-right",
        onclick: null,
        showDuration: "300",
        hideDuration: "500",
        timeOut: "5000",
        extendedTimeOut: "1000",
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: "fadeIn",
        hideMethod: "fadeOut"
    };
    april.notify.info = function (message, title) {
        return toastr.info(message, title);
    };
    april.notify.success = function (message, title) {
        return toastr.success(message, title);
    };
    april.notify.error = function (message, title) {
        return toastr.error(message, title);
    };
    april.notify.warn = function (message, title) {
        return toastr.warning(message, title);
    };
})(jQuery);