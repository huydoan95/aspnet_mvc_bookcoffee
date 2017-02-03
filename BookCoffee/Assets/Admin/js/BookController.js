var book = {
    init: function () {
        book.registerEvents();
    },
    registerEvents: function () {
        $('.BookChangeStatus').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/Books/ChangeStatus',
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
        });
        if ($('#book_client_index').length) {
            squareThis('.square_images');
            squareThis('.item',0.67);
        }
       
    }
}
book.init();