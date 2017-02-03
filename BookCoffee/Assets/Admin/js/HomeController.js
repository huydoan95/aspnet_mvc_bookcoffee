var home = {
    init: function () {
        home.registerEvents();
    },
    registerEvents: function () {
        //Chỉ chạy đoạn script này khi trang được load là trang Client Home Index
        if ($('#main-content').length) {
            squareThis('.square_images');
            squareThis('.bg-pink.mod-book', 0.67);
            squareThis('#slider2', 0.67);
        }
       

        //Chỉ chạy đoạn script này khi trang được load là trang Admin Home Index
        if ($('#Admin_Home_index').length) {
            $('#viewList').hide();
            $('#click_to_view').off('click').on('click', function (e) {
                e.preventDefault();
                $.ajax({
                    url: '/Admin/Home/listTableBooking',
                    data: '{}',
                    dataType: 'html',
                    type: 'POST',
                    success: function (response) {
                        $('#viewList').html(response);
                        $('#viewList').show();
                        $('tr').off('click').on('click', function () {
                            var $id = $(this).data('id');

                        });
                        
                    }
                });
            });
        }
    }
}
home.init();