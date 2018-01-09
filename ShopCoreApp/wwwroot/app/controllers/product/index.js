var productController = (function () {
    var initailize = function () {
        loadCategorys();
        loadData();
        registerEvent();
    };

    var registerEvent = function () {
        $('#ddlShowPage').on('change', function () {
            app.configs.pageSize = $(this).val();
            app.configs.pageIndex = 1;
            loadData(true);
        });
        $('#btnSearch').on('click', function () {
            loadData();
        });
        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                loadData();
            }
        });
    };
    var loadCategorys = function () {
        $.ajax({
            type: 'GET',
            url: '/admin/product/GetAllCategory',
            dataType: 'json',
            success: function (response) {
                if (response) {
                    var render = '<option value="">--Select category--</option>';
                    $.each(response, function (i, item) {
                        render += '<option value="' + item.Id + '">' + item.Name + '</option>';
                    });
                    $('#ddlCategorySearch').html(render);
                }
            },
            error: function (error) {
                console.log(error);
                app.notify('Cannot loading data', 'error');
            }
        });
    };
    var loadData = function (isPageChanged) {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            url: '/admin/product/GetAllPaging',
            dataType: 'json',
            data: {
                categoryId: $('#ddlCategorySearch').val(),
                keyword: $('#txtKeyword').val(),
                pageSize: app.configs.pageSize,
                page: app.configs.pageIndex
            },
            success: function (response) {
                if (response && response.Result) {
                    $.each(response.Result, function (i, item) {
                        render += Mustache.render(template, {
                            Id: item.Id,
                            Name: item.Name,
                            CategoryName: item.ProductCategory.Name,
                            Image: item.Image === null ? '<img scr="/admin/images/user.png" width=25>' : '<img scr="' + item.Image + '" width=25>',
                            CreatedDate: app.dateFormatJson(item.DateCreated),
                            Status: app.getStatus(item.Status),
                            Price: app.formatNumber(item.Price)
                        });

                        $('#lblTotalRecords').text(response.RowCount);

                        if (render !== '') {
                            $('#tblContent').html(render);
                        }

                        wrapPaging(response.RowCount, function () {
                            loadData();
                        }, isPageChanged);
                    });
                }
            },
            error: function (error) {
                console.log(error);
                app.notify('Cannot loading data', 'error');
            }
        });
    };

    var wrapPaging = function (recordCount, callBack, changePageSize) {
        var totalSize = Math.ceil(recordCount / app.configs.pageSize);

        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData('twbs-pagination');
            $('#paginationUL').unbind('page');
        }

        // Binding Event
        $('#paginationUL').twbsPagination({
            totalPages: totalSize,
            visiablePages: 7,
            first: 'First',
            prev: 'Prev',
            next: 'Next',
            last: 'Last',
            onPageClick: function (event, p) {
                app.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });


    };
    return {
        initailize: initailize,
        registerEvent: registerEvent
    };
})();