var productCategoriesController = (function () {
    var initailize = function () {
        loadData();
        registerEvent();
    };

    var registerEvent = function () {
        $('#btnCreate').off('click').on('click', function (e) {
            initTreeDropDownCategory();
            clearAddEditProductCategoryForm();
            $('#modalAddEdit').modal('show');
        });

        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtNameProductCategory: { required: true },
                txtOrderProductCategory: { number: true },
                txtHomeOrderProductCategory: { number: true }
            }
        });

        $('body').on('click', '#btnEdit', function (e) {
            e.preventDefault();
            var productId = $('#hidIdM').val();
            $.ajax({
                type: "GET",
                url: '/Admin/productCategories/GetById',
                dataType: 'json',
                data: { id: productId },
                beforeSent: function (e) {
                    app.startLoading();
                },
                success: function (response) {
                    var data = response;
                    $('#hidIdM').val(data.Id);

                    initTreeDropDownCategory(data.categoryId);
                    $('#txtNameProductCategory').val(data.Name);
                    $('#txtDescriptionProductCategory').val(data.Description);
                    $('#txtOrderProductCategory').val(data.SortOrder);
                    $('#txtHomeOrderProductCategory').val(data.HomeOrder);
                    $('#txtSeoPageTitleProductCategory').val(data.SeoPageTitle);
                    $('#txtSeoKeywordsProductCategory').val(data.SeoKeywords);
                    $('#txtSeoDescriptionProductCategory').val(data.SeoDescription);
                    $('#txtSeoAliasProductCategory').val(data.SeoAlias);
                    $('#txtImageProductCategory').val(data.Image);
                    $('#ckStatusProductCategory').prop('checked', data.Status === 1);
                    $('#ckShowHomeProductCategory').prop('checked', data.HomeFlag);
                    $('#modalAddEdit').modal('show');
                    app.stopLoading();
                },
                error: function (error) {
                    app.notify('Error', 'error');
                }
            });
        });

        $('#btnSave').on('click', function (e) {
            e.preventDefault();
            if ($('#frmMaintainance').valid()) {
                var productCategory = {
                    id: parseInt($('#hidIdM').val()),
                    name: $('#txtNameProductCategory').val(),
                    parentId: $('#ddlCategoryIdProductCategory').combotree('getValue'),
                    description: $('#txtDescriptionProductCategory').val(),
                    image: $('#txtImageProductCategory').val(),
                    sortOrder: parseInt($('#txtOrderProductCategory').val()),
                    homeOrder: $('#txtHomeOrderProductCategory').val(),
                    seoKeywords: $('#txtSeoKeywordsProductCategory').val(),
                    seoDescription: $('#txtSeoDescriptionProductCategory').val(),
                    seoPageTitle: $('#txtSeoPageTitleProductCategory').val(),
                    seoAlias: $('#txtSeoAliasProductCategory').val(),
                    status: $('#ckStatusProductCategory').prop('checked') === true ? 1 : 0,
                    showHome: $('#ckShowHomeProductCategory').prop('checked')
                };
                $.ajax({
                    method: 'POST',
                    url: '/admin/productCategories/SaveProductCategory',
                    dataType: 'JSON',
                    data: productCategory,
                    beforeSend: function () {
                        app.startLoading();
                    },
                    success: function (reponse) {
                        app.notify('Success', 'success');
                        loadData(true);
                        clearAddEditProductCategoryForm();
                        app.stopLoading();
                        $('#modalAddEdit').modal('hide');

                    },
                    error: function (error) {
                        app.notify('Error', 'error');
                        app.stopLoading();
                    }
                });
            }
            return false;
        });

        $('#btnCancel').on('click', function (e) {
            clearAddEditProductCategoryForm();
            $('#modalAddEdit').modal('hide');
        });

        $('body').on('click', '#btnDelete', function (e) {
            e.preventDefault();
            var productCategory = $('#hidIdM');
            app.confirm('Are you sure to delete?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/ProductCategories/DeleteProductCategory",
                    data: { id: that },
                    dataType: "json",
                    beforeSend: function () {
                        app.startLoading();
                    },
                    success: function (response) {
                        app.notify('Deleted success', 'success');
                        app.stopLoading();
                        loadData();
                    },
                    error: function (status) {
                        app.notify('Has an error in deleting progress', 'error');
                        app.stopLoading();
                    }
                });
            });
        });

        $('#btnSelectImgProductCategory').on('click', function () {
            $('#fileInputImage').click();
        });

        $("#fileInputImage").on('change', function () {
            var fileUpload = $(this).get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            $.ajax({
                type: "POST",
                url: "/Admin/Upload/UploadImage",
                contentType: false,
                processData: false,
                data: data,
                success: function (path) {
                    $('#txtImageProductCategory').val(path);
                    app.notify('Upload image succesful!', 'success');
                },
                error: function () {
                    app.notify('There was error uploading files!', 'error');
                }
            });
        });
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
                        onContextMenu: function (e, node) {
                            e.preventDefault();
                            // Select the node
                            $('#tt').tree('select', node.target);
                            $('#hidIdM').val(node.id);
                            // Display context menu
                            $('#contextMenu').menu('show', {
                                left: e.pageX,
                                top: e.pageY
                            });
                        },
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

    var initTreeDropDownCategory = function (categoryId) {
        $.ajax({
            method: 'get',
            url: '/admin/productCategories/GetAll',
            dataType: 'json',
            async: false,
            success: function (response) {
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
                $('#ddlCategoryIdProductCategory').combotree({
                    data: treeArray
                });
                if (categoryId) {
                    $('#ddlCategoryIdProductCategory').combotree('setValue', categoryId);
                }
            },
            error: function (error) {
                app.notify('Error', 'error');
            }
        });
    };

    var clearAddEditProductCategoryForm = function () {
        $('#hidIdM').val(0);
        $('#txtNameProductCategory').val('');
        $('#txtDescriptionProductCategory').val('');
        $('#txtAliasProductCategory').val('');
        $('#txtOrderProductCategory').val('');
        $('#txtHomeOrderProductCategory').val('');
        $('#txtSeoPageTitleProductCategory').val('');
        $('#txtSeoKeywordsProductCategory').val('');
        $('#txtSeoDescriptionProductCategory').val('');
        $('#txtImageProductCategory').val('');
        $('#txtSeoAliasProductCategory').val('');
        $('#ckStatusProductCategory').prop('checked', true);
        $('#ckShowHomeroductCategory').prop('checked', false);
    };

    return {
        initailize: initailize,
        registerEvent: registerEvent
    };
})();