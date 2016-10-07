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
        })

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
        })

    }

}
product.init();