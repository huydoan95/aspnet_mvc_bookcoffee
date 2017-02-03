var publisher = {
    init: function () {
        publisher.registerEvents();
    },
    registerEvents: function () {
        $('.PublisherChangeStatus').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/Publisher/ChangeStatus',
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
    }
}
publisher.init();