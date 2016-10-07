var lawCorner = {
    init: function () {
        lawCorner.registerEvents();
    },
    registerEvents: function () {
        $('.ChangeStatus').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/LawCorner/ChangeStatus',
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

        $('.ChangeTopHot').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/LawCorner/ChangeTopHot',
                data: { id: id },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    console.log(response);
                    if (response.status == 1) {
                        btn.text("Yes");
                    } else if (response.status==2) {
                        btn.text('No');
                    } else if (response.status == 0) {
                        alert("Tạm thời không thể thiết lập Top Hot cho tin này, vì hiện tại đang có 1 tin thuộc TopHot");
                        //$.ajax({
                        //    type: 'POST',
                        //    url: '/Admin/LawCorner/showAlert',
                        //    dataType: 'json',
                        //    data: '{}',
                        //    contentType: "application/json;charset=utf-8",
                        //    success: function (x) {
                        //        alert(x);
                        //    }
                        //});
                    }
                }
            });
        })
    }
}
lawCorner.init();