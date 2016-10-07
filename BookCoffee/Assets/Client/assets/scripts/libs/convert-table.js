/*function tableMidle () {
	(function ( $ ) {
		$.fn.convert_tableMidle=function(){
			var element=$(this)

			$(element).innerHeight();
			var h_=0;
			var itemss=$(element);
			itemss.each(function(){
				if(h_<$(this).innerHeight()) h_=$(this).height();
			})
			itemss.each(function(){
				var $ac=itemss.length;
				// var $height=$(this).parent().innerHeight();
				if ($(this).parent().hasClass('table-div')) {
					$(this).addClass('table-cell').parent('.table-div').innerHeight(h_);
				}
				else{
					$(this).addClass('table-cell').parent().prepend("<div class='table-div'></div>");
					$(this).clone().appendTo($(this).parent().find('.table-div').innerHeight(h_));
					$(this).remove();
				};
			});

		};
			$.fn.convert_oneTable=function(){
			var elementOne=$(this)
			var itemOne=$(elementOne);
			itemOne.each(function(){
				var $height=$(this).parent().innerHeight();
				if ($(this).parent().hasClass('table-div')) {
					$(this).addClass('table-cell').parent('.table-div').innerHeight($height);
				}
				else{
					$(this).addClass('table-cell').parent().prepend("<div class='table-div'></div>");
					$(this).clone().appendTo($(this).parent().find('.table-div').innerHeight($height));
					$(this).remove();
				};
			});

		};
	}( $ ));
};
function findTable(){
   $(".row").each(function(){
      $(this).find(".assd").convert_tableMidle();
    });
};

$(document).ready(function(){
	tableMidle();
});
$(window).load(function(){
	findTable();
});
$(window).resize(function(){
	findTable();
});
*/