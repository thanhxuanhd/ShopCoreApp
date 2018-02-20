var app = (function () {
    var configs = {
        pageSize: 10,
        pageIndex: 1
    };
    var notify = function (message, type) {
        $.notify(message, {
            // whether to hide the notification on click
            clickToHide: true,
            // whether to auto-hide the notification
            autoHide: true,
            // if autoHide, hide after milliseconds
            autoHideDelay: 5000,
            // show the arrow pointing at the element
            arrowShow: true,
            // arrow size in pixels
            arrowSize: 5,
            // position defines the notification position though uses the defaults below
            position: '...',
            // default positions
            elementPosition: 'top left',
            globalPosition: 'top right',
            // default style
            style: 'bootstrap',
            // default class (string or [string])
            className: type,
            // show animation
            showAnimation: 'slideDown',
            // show animation duration
            showDuration: 400,
            // hide animation
            hideAnimation: 'slideUp',
            // hide animation duration
            hideDuration: 200,
            // padding between element and notification
            gap: 2
        })
    }
    var confirm = function (message, onCallBack) {
        bootbox.confirm({
            message: message,
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-default'
                }
            },
            callback: function (result) {
                if (result === true) {
                    onCallBack();
                }
            }
        });
    }
    var dateFormatJson = function (dateTime, formatDate) {
        if (!dateTime) {
            return '';
        }
        if (formatDate) {
            return moment(dateTime).format(formatDate);
        }

        return moment(dateTime).format('DD/MM/YYYY');
    };
    var dateTimeFormatJson = function (dateTime, formatDateTime) {
        if (!dateTime) {
            return '';
        }
        if (formatDate) {
            return moment(dateTime).format(formatDateTime);
        }
        return moment(dateTime).format('DD/MM/YYYY hh:mm:ss');
    }
    var startLoading = function () {
        if ($('.dv-loading').length > 0) {
            $('.dv-loading').removeClass('hide');
        }
    }
    var stopLoading = function () {
        if ($('.dv-loading').length > 0) {
            $('.dv-loading').addClass('hide');
        }
    }
    var getStatus = function (status) {
        if (status == 1)
            return '<span class="badge bg-green">Kích hoạt</span>';
        else
            return '<span class="badge bg-red">Khóa</span>';
    }
    var formatNumber = function (number, precision) {
        if (!isFinite(number)) {
            return number.toString();
        }

        var a = number.toFixed(precision).split('.');
        a[0] = a[0].replace(/\d(?=(\d{3})+$)/g, '$&,');
        return a.join('.');
    }
    var unflattern = function (arr) {
        var map = {};
        var roots = [];
        for (var i = 0; i < arr.length; i += 1) {
            var node = arr[i];
            node.children = [];
            map[node.id] = i; // use map to look-up the parents
            if (node.parentId !== null) {
                arr[map[node.parentId]].children.push(node);
            } else {
                roots.push(node);
            }
        }
        return roots;
    }

    var postAjax = function (e, xhr, options) {
        $(document).ajaxSend(function (e, xhr, options) {
            if (options.type.toUpperCase() == "POST" || options.type.toUpperCase() == "PUT") {
                var token = $('form').find("input[name='__RequestVerificationToken']").val();
                xhr.setRequestHeader("RequestVerificationToken", token);
            }
        });
    }

    $(document).ajaxSend(function (e, xhr, options) {
        if (options.type.toUpperCase() == "POST" || options.type.toUpperCase() == "PUT") {
            var token = $('form').find("input[name='__RequestVerificationToken']").val();
            xhr.setRequestHeader("RequestVerificationToken", token);
        }
    });

    return {
        configs: configs,
        notify: notify,
        confirm: confirm,
        dateFormatJson: dateFormatJson,
        dateTimeFormatJson: dateTimeFormatJson,
        startLoading: startLoading,
        stopLoading: stopLoading,
        getStatus: getStatus,
        formatNumber: formatNumber,
        unflattern: unflattern,
        postAjax: postAjax
    }
})();
