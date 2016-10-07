var menuType = {
    init: function () {
        menuType.registerEvents();
    },
    registerEvents: function () {
        $('.menuTypesStatus').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/MenuType/ChangeStatus',
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
        })
    }
}
menuType.init();