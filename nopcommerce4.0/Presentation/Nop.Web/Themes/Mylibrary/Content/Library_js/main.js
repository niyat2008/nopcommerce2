(function ($) {
 "use strict";
    
	/*---------------------
	 jQuery MeanMenu
	--------------------- */
	jQuery('nav#dropdown').meanmenu();

	/*---------------------
 	scrollUp
	--------------------- */	
	$.scrollUp({
      scrollText: '<i class="fa fa-angle-up"></i>',
      easingType: 'linear',
      scrollSpeed: 900,
      animation: 'fade'
    });
	
	/*---------------------
	 1. owl-carousel
	--------------------- */
	$(".product-carusul").owlCarousel({
		autoPlay: false, //Set AutoPlay to 3 seconds
		navigation : true,
		navigationText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
		pagination :false,
		items : 5,
		itemsDesktop : [1199,5],
		itemsDesktopSmall : [979,2],
		itemsMobile : [767,1]
	  });
	
	/*---------------------
	 2. owl-carousel
	--------------------- */
	$(".new-product-carosul").owlCarousel({
		autoPlay: false, //Set AutoPlay to 3 seconds
		navigation : true,
		navigationText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
		pagination :false,
		items : 4,
		itemsDesktop : [1199,4],
		itemsDesktopSmall : [979,2],
		itemsMobile : [767,1]
	  });
	
	/*---------------------
	 3. owl-carousel
	--------------------- */
	$(".add-banner-carsuol, .special-product-carosul, .recent-post").owlCarousel({
		autoPlay: false, //Set AutoPlay to 3 seconds
		navigation : false,
		navigationText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
		pagination :true,
		items : 1,
		itemsDesktop : [1199,1],
		itemsDesktopSmall : [979,1],
		itemsMobile : [767,1]
	  });
	
	/*---------------------
	 4. owl-carousel
	--------------------- */
	$(".logo-brand-carosul").owlCarousel({
		autoPlay: false, //Set AutoPlay to 3 seconds
		navigation : true,
		navigationText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
		pagination :false,
		items : 6,
		itemsDesktop : [1199,6],
		itemsDesktopSmall : [979,4],
		itemsMobile : [767,1]
	  });
	
	/*---------------------
	 5. owl-carousel
	--------------------- */
	$(".product-carusul-pagination").owlCarousel({
		autoPlay: false, //Set AutoPlay to 3 seconds
		navigation : false,
		navigationText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
		pagination :true,
		items : 5,
		itemsDesktop : [1199,5],
		itemsDesktopSmall : [979,2],
		itemsMobile : [767,1]
	  });
	
	/*---------------------
	 6. owl-carousel
	--------------------- */
	$(".blog-carosul").owlCarousel({
		autoPlay: false, //Set AutoPlay to 3 seconds
		navigation : false,
		navigationText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
		pagination :true,
		items : 3,
		itemsDesktop : [1199,3],
		itemsDesktopSmall : [979,2],
		itemsMobile : [767,1]
	  });


	  /*---------------------
	 7. owl-carousel
	--------------------- */
	  $(".post-slider").owlCarousel({
	  
	    autoPlay: false, //Set AutoPlay to 3 seconds
	    navigation : true,
	    navigationText : ["<i class='fa fa-caret-left'></i>","<i class='fa fa-caret-right'></i>"],
	    pagination :true,
	    items : 1,
	    itemsDesktop : [1199,1],
	    itemsDesktopSmall : [979,1],
	    itemsMobile : [767,1],
	 
	  });

	/*---------------------
	 Category menu
	--------------------- */
	$('#cate-toggle li.has-sub>a').on('click', function(){
		$(this).removeAttr('href');
		var element = $(this).parent('li');
		if (element.hasClass('open')) {
			element.removeClass('open');
			element.find('li').removeClass('open');
			element.find('ul').slideUp();
		}
		else {
			element.addClass('open');
			element.children('ul').slideDown();
			element.siblings('li').children('ul').slideUp();
			element.siblings('li').removeClass('open');
			element.siblings('li').find('li').removeClass('open');
			element.siblings('li').find('ul').slideUp();
		}
	});
	$('#cate-toggle>ul>li.has-sub>a').append('<span class="holder"></span>');
	/*---------------------
	 Price Filter
	--------------------- */
    var min_price = parseFloat('0');
    var max_price = parseFloat('90');
    var current_min_price = parseFloat($('#price-from').val());
    var current_max_price = parseFloat($('#price-to').val());
    $('#slider-price').slider({
        range   : true,
        min     : min_price,
        max     : max_price,
        values  : [ current_min_price, current_max_price ],
        slide   : function (event, ui) {
            $('#price-from').val(ui.values[0]);
            $('#price-to').val(ui.values[1]);
            current_min_price = ui.values[0];
            current_max_price = ui.values[1];
        },
    });

	/*---------------------
	fancybox
	--------------------- */	
	$('.fancybox').fancybox();

	/*---------------------
	 Input Number Incrementer
	--------------------- */
	  $(".numbers-row").append('<div class="inc button">+</div><div class="dec button">-</div>');
	  $(".button").on("click", function() {
		var $button = $(this);
		var oldValue = $button.parent().find("input").val();
		if ($button.text() == "+") {
		  var newVal = parseFloat(oldValue) + 1;
		} else {
		   // Don't allow decrementing below zero
		  if (oldValue > 0) {
			var newVal = parseFloat(oldValue) - 1;
			} else {
			newVal = 0;
		  }
		  }
		$button.parent().find("input").val(newVal);

	  });




    
    
    
})(jQuery);    