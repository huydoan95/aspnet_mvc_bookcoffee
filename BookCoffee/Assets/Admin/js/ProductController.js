var product = {
    init: function () {
        product.registerEvents();
    },
    registerEvents: function () {
        $('.ProductChangeStatus').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/Product/ChangeStatus',
                data: { id: id },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text("Đang sử dụng");
                    } else {
                        btn.text('Không sử dụng');
                    }
                }
            });
        }) //End ProductChangeStatus Click

        $('.productChangeTopHot').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/Product/ChageTopHot',
                data: { id: id },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text("Bán chạy");
                    } else {
                        btn.text('Bình thường');
                    }
                }
            });
        }) //End productChangeTopHot Click

        $('.btn-images').off('click').on('click', function (e) {
            e.preventDefault();
            $('#imageManage').modal('show');
            $('#hidProductID').val($(this).data('id'));
            product.LoadImage();
        })
        //Mở CkFinder và gán hình vào 
        $('#btnChooseImage').off('click').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                var html = '<div style="display:inline;margin:5px;">' +
                    '<img src=' + url + ' width="100"  />' +
                    '<a href="#" class="btn-removeImage"><i class="fa fa-times"></i></a>' +
                    '<input type="hidden" class="hidImage" value=' + url + '>' +
                    '</div>';
                $('.imageList').append(html);

                $('.btn-removeImage').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                })
            }
            finder.popup();
        });
        //Sự kiện save image
        $('#btnSaveImage').off('click').on('click', function () {
            var images = [];
            $.each($('.imageList .hidImage'), function (i, item) {
                images.push($(item).val());
            });

            $.ajax({
                url: '/Admin/product/saveImages',
                type: 'POST',
                dataType: 'json',
                data: {
                    id: $('#hidProductID').val(),
                    images: JSON.stringify(images)
                },
                success: function (response) {
                    $('#imageManage').modal('hide');
                    alert("Thêm ảnh thành công");
                }
            });
        });

        //Load hình lên Dialog Modal khi rê chuột qua từng hình
        $('.viewImagesDetail').off('click').on('click', function () {
            $('#hidProID').val($(this).data('id'))
            product.ImagesofEachPro();
        });


        //Kiểm tra xem trang hiện tại đang sử dụng file ProductController.js có div nào có ID là slider_client_index không
        if ($('#client_index').length) {
            product.clearjQueryCache();
            var ID;
            $.ajax({
                url: '/Product/OneProductTopHot',
                type: 'POST',
                dataType: 'Json',
                data: '{}',
                cache: false,
                success: function (response) {
                    ID = response.id;
                    //Sau khi lấy được ID của sản phẩm tophot có ngày tạo gần nhất.
                    //Lấy hình từ MoreImage của product và add lên slider
                    $.ajax({
                        url: '/product/loadImages',
                        type: 'POST',
                        dataType: 'html',
                        data: {
                            id: ID
                        },
                        cache: false,
                        success: function (response) {
                            $('#client_index').html(response);
                        },
                    });
                }
            });
            $('.box-img.ps-rv').off('click').on('click', function (e) {
                e.preventDefault();
                var ID = $(this).data('id');
                $.ajax({
                    url: '/product/loadImages',
                    type: 'POST',
                    dataType: 'html',
                    data: {
                        id: ID
                    },
                    cache: false,
                    success: function (response) {
                        $('#client_index').html(response);
                    },
                });
            });
            $('.btn.btn-default').off('click').on('click', function (e) {
                e.preventDefault();
                var ID = $(this).data('id');
                $.ajax({
                    url: '/product/loadImages',
                    type: 'POST',
                    dataType: 'html',
                    data: {
                        id: ID
                    },
                    cache: false,
                    success: function (response) {
                        $('#client_index').html(response);
                    },
                });
            });
            squareThis('.isotope', 0.67);
            squareThis('.square_images', 0.67);
            squareThis('.item bg', 0.67);

            //Ẩn Menu sản phẩm theo loại
            $('.MnuProdByCate').hide();
            //Click lên Menu loại sản phẩm
            $('.ProdItem').off('click').on('click', function (e) {
                e.preventDefault();
                var id = $(this).data('id');
                //Gọi Action
                $.ajax({
                    url: '/Product/ProductByCategory',
                    type:'POST',
                    dataType: 'html',
                    data: {
                        id: id
                    },
                    success: function (response) {
                        $('.MnuProdByCate').html(response);
                        $('.MnuProdByCate').show();
                    }
                });
            });

        }
    },

    clearjQueryCache: function () {
        for (var x in jQuery.cache) {
            delete jQuery.cache[x];
        }
    },

    //Phương thức này load hình theo mỗi sản phẩm khi click chuột lên hình
    ImagesofEachPro: function () {
        $.ajax({
            url: '/Admin/product/loadImages',
            type: 'POST',
            dataType: 'html',
            data: {
                id: $('#hidProID').val()
            },
            success: function (response) {
                $('#listImagesOfProduct').html(response);
                $('#productImages').modal('show');
            }
        });
    },


    //Load hình lên trước khi chọn hình mới
    LoadImage: function () {
        $.ajax({
            url: '/Admin/product/LoadImage',
            type: 'POST',
            dataType: 'json',
            data: {
                id: $('#hidProductID').val()
            },
            success: function (response) {
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<div style="display:inline;margin:5px;">' +
                     '<img src=' + item + ' width="100"  />' +
                     '<a href="#" class="btn-removeImage"><i class="fa fa-times"></i></a>' +
                     '<input type="hidden" class="hidImage" value=' + item + '>' +
                     '</div>';
                });
                $('.imageList').html(html);
                $('.btn-removeImage').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                })
            }
        });
    }

}
product.init();