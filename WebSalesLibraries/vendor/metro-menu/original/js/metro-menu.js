$(document).ready(function() {

$('.metro-side-menu .level').each(function(){
    var w=$(this).find('.column').length*150;
    var m=$(this).find('.column').length*1;
   $(this).width(w-m); 
});

    /*
     * Replace all SVG images with inline SVG
     */
    jQuery('img.svg').each(function(){
        var $img = jQuery(this);
        var imgID = $img.attr('id');
        var imgClass = $img.attr('class');
        var imgURL = $img.attr('src');
 
        jQuery.get(imgURL, function(data) {
           
            // Get the SVG tag, ignore the rest
            var $svg = jQuery(data).find('svg');

            // Add replaced image's ID to the new SVG
            if(typeof imgID !== 'undefined') {
                $svg = $svg.attr('id', imgID);
            }
            // Add replaced image's classes to the new SVG
            if(typeof imgClass !== 'undefined') {
                $svg = $svg.attr('class', imgClass+' replaced-svg');
            }

            // Remove any invalid XML tags as per http://validator.w3.org
            $svg = $svg.removeAttr('xmlns:a');

            // Replace image with new SVG
            $img.replaceWith($svg);
            
        });

    });
        
        
        
    $('.menu').not('.level .menu').hover(function(){
            var el=$(this);
           
            if($('.level.open').length>0){
                 var closingLevel=$('.level.open');
                
                 closingLevel.removeClass('open');
                setTimeout(function(){
                    
                   closingLevel.css('display','none');
                    
                   
                    openMenu(el);
                },100);
                
            }else{
                 openMenu(el);
            }
        
    },function(){
       
    });
        $(document).bind('click',function(e) {
           
            if($('.level.open').length>0){
                 var closingLevel=$('.level.open');
               
        closingLevel.removeClass('open');
                setTimeout(function(){
                    closingLevel.css('display','none')
                    
                    
               
                },100);    
       }
        });
      function openMenu(el){
          var openingLevel= $("#"+el.attr('data-opening-id'));
            
            
             openingLevel.css('display','block')
                setTimeout(function(){
                    
                    openingLevel.addClass('open');
                    
                   
                
                },100);
    

            
      }
        
        
})

