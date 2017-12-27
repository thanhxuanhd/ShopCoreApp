var productController = (function () {
    var initailize = function () {
        loadData();
    }

    var registerEvent = function () {

    }
    var loadData = function () {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            url: '/admin/product/GetAll',
            success: function (response) {
                if (response) {
                    $.each(response, function (i, item) {
                        render += Mustache.render(template, {
                            Id: item.Id,
                            Name: item.Name,
                            CategoryName: item.ProductCategory.Name,
                            Image: item.Image == null ? '<img scr="/admin/images/user.png" width=25>' : '<img scr="' + item.Image + '" width=25>',
                            CreatedDate: app.dateFormatJson(item.DateCreated),
                            Status: app.getStatus(item.Status),
                            Price: app.formatNumber(item.Price)
                        });

                        if (render !== '') {
                            $('#tblContent').html(render);
                        }
                    });
                }
            },
            error: function (error) {
                console.log(error);
                app.notify('Cannot loading data', 'error');
                app.dateFormatJson()
            }
        });
    }

    return {
        initailize: initailize,
        registerEvent: registerEvent
    }
})();