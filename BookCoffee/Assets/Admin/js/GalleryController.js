var gallery = {
    init: function () {
        gallery.registerEvents();
    },
    registerEvents: function () {
        $('.GalleryChangeStatus').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/Gallery/ChangeStatus',
                data: { id: id },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text("Sử dụng");
                    } else {
                        btn.text('Không sử dụng');
                    }
                }
            });
        })//End ChangeImageStatus

        $('.GalleryChangeTopHot').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/Gallery/GalleryChangeTopHot',
                data: { id: id },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    console.log(response);
                    if (response.status == 0) {
                        alert("Tạm thời không chuyển Album này lên TopHot được, vì đang có 1 Album TopHot");
                        btn.text("No");
                    } else if (response.status == 1) {
                        btn.text('Yes');
                    } else if (response.status == 2) {
                        btn.text('No');
                    }
                }
            });
        }) //End GalleryChangeTopHot


        $('.btn-MoreImage').off('click').on('click', function (e) {
            e.preventDefault();
            $('#imageManage').modal('show');
            $('#hidProductID').val($(this).data('id'));

        })//End btn-MoreImage

        //Upload hình cho album không gian quán
        //document.getElementById('imageUploadId').onsubmit = function () {
        //    var formdata = new FormData();
        //    var fileInput = document.getElementById('fileInputType');
        //    for (i = 0; i < fileInput.files.length; i++) {
        //        formdata.append(fileInput.files[i].name, fileInput.files[i]);
        //    }
        //    var id = $('#hidProductID').val();
        //    formdata.append("id", id);
        //    var xhr = new XMLHttpRequest();
        //    xhr.open('POST', '/Admin/Gallery/UploadImageMethod');
        //    xhr.send(formdata);
        //    xhr.onreadystatechange = function () {
        //        if (xhr.readyState == 4 && xhr.status == 200) {
        //            if (xhr.responseText == "failed") {
        //                alert("Xảy ra lỗi.! vui lòng thử lại");
        //            } else {
        //                alert("Upload hình thành công.");
        //                $('#imageManage').modal('hide');
        //            }
        //        }
        //    }
        //    return false;
        //}//End document.getElementById('imageUploadId').onsubmit

        //Sự kiện từ nút ImageUpload từ GalleryDetail.cshtml
        $('#btnSubmit').off('click').on('click', function (e) {
            e.preventDefault();
            var formdata = new FormData();
            var GalleryName = $('#GalleryName').val();
            var fileInput = document.getElementById('fileInputType');
            for (i = 0; i < fileInput.files.length; i++) {
                formdata.append(fileInput.files[i].name, fileInput.files[i]);
            }
            var id = $('#hidProductID').val();
            formdata.append("id", id);
            var xhr = new XMLHttpRequest();
            xhr.open('POST', '/Admin/Gallery/UploadImageMethod');
            xhr.send(formdata);
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    if (xhr.responseText == "failed") {
                        alert("Xảy ra lỗi.! vui lòng thử lại");
                    } else {
                        alert("Upload hình thành công.");
                        document.getElementById('fileInputType').value="";
                        $('#imageManage').modal('hide');
                        gallery.LoadImage();
                    }
                }
            }
        })

        //Sự kiện click trên tab hình ảnh ở trang GalleryDetail.cshtml
        $('#tabImage').off('click').on('click', function (e) {
            e.preventDefault();
            $('#GalleryID').val($(this).data('id'));
            gallery.LoadImage();
        })//End $('#tabImage')

        //script này áp dụng cho trang Index của Gallery tại client
        if ($('#Gallery_Client_Index').length) {
            var width = $('div.item').width();
            var height = (width / 5) * 2;
            $('.slider_images').width(width);
            $('.slider_images').height(height);
            squareThis('.square_images',0.67);
        }

    },//End registerEvents

    //Load hình mỗi album không gian quán
    LoadImage: function () {
        $.ajax({
            url: '/Admin/Gallery/LoadImage',
            type: 'POST',
            dataType: 'json',
            data: {
                id: $('#GalleryID').val()
            },
            success: function (response) {
                var data = response.data;
                var htmlTop = '<ul class="gallery clearfix">';
                var htmlBottom = '</ul>';
                var html = '';
                $.each(data, function (i, item) {
                    html += '<li>' +
                        ' <a href=' + item + ' rel="prettyPhoto[gallery1]" title='+ item +'>' +
                        ' <img src=' + item + ' width="150" height="150" alt=' + $('#GalleryName').val() + ' />' +
                        ' </a>' +
                        '<a href="#" class="btn-removeImage" data-id=' + item + ' title="Xóa hình này"><i class="fa fa-times"></i></a>' +
                        '</li>';
                });
                var htmlfull = htmlTop + html + htmlBottom;
                $('#main').html(htmlfull);
                //Gọi prettyPhoto
                $("a[rel^='prettyPhoto']").prettyPhoto({ social_tools: '' });

                //xóa hình gọi từ trang GalleryDetail.cshtml
                $('.btn-removeImage').off('click').on('click', function (e) {
                    e.preventDefault();
                    var imageName = $(this).data('id');
                    if (confirm("Bạn muốn xóa hình này khỏi Album?")) {
                        $.ajax({
                            url: '/Admin/Gallery/DeleteImage',
                            type: 'POST',
                            dataType: 'json',
                            data: {
                                id: $('#GalleryID').val(),
                                imageName: imageName
                            },
                            success: function (response) {
                                if (response.status == true) {
                                    //Gọi lại phương thức LoadImage để cập nhật lại view
                                    gallery.LoadImage();
                                } else {
                                    alert("Chưa xóa hình được");
                                }
                            }
                        });
                    } else {

                    }
                })
            }
        });
    }
}
gallery.init();