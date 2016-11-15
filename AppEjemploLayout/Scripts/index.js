
    //<script type="text/javascript">
	jQuery(document).ready(function($) {
	    $(".scroll").click(function(event){
	        event.preventDefault();
	        $('html,body').animate({scrollTop:$(this.hash).offset().top},600);
	    });
	});
    //</script>


(function ($) {
    //<link rel="stylesheet" href="~/Content/flexslider.css" type="text/css" media="screen" property="" />
            //<script defer src="~/Scripts/jquery.flexslider.js"></script>
            //<script type="text/javascript">
							$(window).load(function(){
							    $('.flexslider').flexslider({
							        animation: "slide",
							        start: function(slider){
							            $('body').removeClass('loading');
							        }
							    });
							});
    //</script>
}(jQuery));

(function ($) {
    $(document).ready(function () {
        /*
            var defaults = {
            containerID: 'toTop', // fading element id
            containerHoverID: 'toTopHover', // fading element hover id
            scrollSpeed: 1200,
            easingType: 'linear'
            };
        */

        $().UItoTop({ easingType: 'easeOutQuart' });

    });
}(jQuery));
    //here ends scrolling icon



    //flicker -->
(function ($) {
    $(document).ready(function () {

        $('.flicker-example').flicker();
    });
    //flicker -->
}(jQuery));