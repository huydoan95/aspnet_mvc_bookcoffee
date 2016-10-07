var Events = {
    init: function () {
        Events.registerEvents();
    },
    registerEvents: function () {
        $('.EventChangeStatus').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/Events/ChangeStatus',
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

        $('.EventChangeTopHot').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/Events/ChageTopHot',
                data: { id: id },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text("Yes");
                    } else {
                        btn.text('No');
                    }
                }
            });
        })

    }

}
Events.init();