(function(){
  'use strict';

  function convertHeight(){
    (function ( $ ) {
      $.fn.convert_height=function(){
        var element=$(this)
        $(element).innerHeight("auto");
        var h_=0;
        var itemss=$(element);
        itemss.each(function(){
          if(h_<$(this).innerHeight()) h_=$(this).innerHeight();
        })
        itemss.each(function(){
          $(this).innerHeight(h_);

        })
      };
    }( jQuery ));
  };

  function convertMinHeight(){
    (function ( $ ) {
      $.fn.convert_min=function(){
        var element=$(this)
        $(element).innerHeight("auto");
        var h_=10000;
        var itemss=$(element);
        itemss.each(function(){
          if(h_>$(this).innerHeight()) h_=$(this).innerHeight();
        })
        itemss.each(function(){
          $(this).innerHeight(h_);

        })
      };
    }( jQuery ));
  };

  function feedHieght(){
    $("").each(function(){
      $(this).find("").convert_height();
    });
  };


  $(document).ready(function() { 
     convertHeight();
     convertMinHeight();
  });

  $(window).load(function() {
    feedHieght();
  });
  
  $(window).resize(function(){

  });

  return {
   convertHeight: convertHeight,
   convertMinHeight: convertMinHeight,
   feedHieght: feedHieght
 };


})();