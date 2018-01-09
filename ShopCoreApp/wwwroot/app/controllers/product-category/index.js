var productCategoriesController = (function () {
    var initailize = function () {
        loadData();
        registerEvent();
    };

    var registerEvent = function () {
    };

    var loadData = function (isPageChanged) {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            url: '/admin/productCategories/GetAll',
            dataType: 'json',
            success: function (response) {
                if (response) {
                    var data = [];
                    $.each(response, function (i, item) {
                        data.push({
                            id: item.Id,
                            text: item.Name,
                            parentId: item.ParentId,
                            sortOrder: item.SortOrder
                        });
                    });
                    var treeArray = app.unflattern(data);
                    $('#treeProductCategories').tree({
                        data: treeArray,
                    });
                }
            },
            error: function (error) {
                console.log(error);
                app.notify('Cannot loading data', 'error');
            }
        });
    };
    return {
        initailize: initailize,
        registerEvent: registerEvent
    };
})();