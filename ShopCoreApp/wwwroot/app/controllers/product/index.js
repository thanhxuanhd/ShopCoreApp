var productController = (function () {
    var initailize = function () {
        loadData();
    }

    var registerEvent = function () {
        $('#ddlShowPage').on('change', function () {
            app.configs.pageSize = $(this).val();
            app.configs.pageIndex = 1;
            loadData(true);
        });
    }
    var loadData = function (isPageChanged) {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            url: '/admin/product/GetAllPaging',
            data: {
                categoryId: null,
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
                            Image: item.Image == null ? '<img scr="/admin/images/user.png" width=25>' : '<img scr="' + item.Image + '" width=25>',
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
                        }, isPageChanged)
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


    }
    return {
        initailize: initailize,
        registerEvent: registerEvent
    }
})();