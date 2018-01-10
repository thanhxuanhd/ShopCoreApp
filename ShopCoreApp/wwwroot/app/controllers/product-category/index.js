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
                    treeArray.sort(function (a, b) {
                        return a.sortOrder - b.sortOrder;
                    });
                    $('#treeProductCategories').tree({
                        data: treeArray,
                        dnd: true,
                        onDrop: function (target, source, point) {

                            var targetNode = $(this).tree('getNode', target);
                            if (point === 'append') {
                                var children = [];
                                $.each(targetNode.children, function (i, item) {
                                    children.push({
                                        key: item.id,
                                        value: i
                                    });
                                });
                                debugger;
                                // Update to Database
                                $.ajax({
                                    url: '/Admin/productCategories/UpdateParentId',
                                    type: 'post',
                                    dataType: 'json',
                                    data: {
                                        sourceId: source.id,
                                        targetId: targetNode.id,
                                        items: children
                                    },
                                    success: function (response) {
                                        loadData();
                                    }
                                });
                            } else if (point === 'top' || point === 'bottom') {
                                $.ajax({
                                    url: '/Admin/productCategories/ReOrder',
                                    type: 'post',
                                    dataType: 'json',
                                    data: {
                                        sourceId: source.id,
                                        targetId: targetNode.id
                                    },
                                    success: function (response) {
                                        loadData();
                                    }
                                });
                            }

                        }
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