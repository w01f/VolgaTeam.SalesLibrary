<!DOCTYPE html>
<html>
    <head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <?php
        $version = '5.0';
        $cs = Yii::app()->clientScript;
        $cs->registerCssFile(Yii::app()->baseUrl . '/vendor/supersized/css/supersized.css?' . $version);
        $cs->registerCssFile(Yii::app()->baseUrl . '/vendor/supersized/theme/supersized.shutter.css?' . $version);
        $cs->registerCoreScript('jquery');
        $cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/supersized/js/jquery.easing.min.js', CClientScript::POS_HEAD);
        $cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/supersized/js/supersized.3.2.7.min.js', CClientScript::POS_HEAD);
        $cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/supersized/theme/supersized.shutter.js', CClientScript::POS_HEAD);
        ?>
        <script type="text/javascript">
            jQuery(function($){
                $.supersized({
                    // Functionality
                    slideshow               :   1,			// Slideshow on/off
                    autoplay				:	0,			// Slideshow starts playing automatically
                    start_slide             :   1,			// Start slide (0 is random)
                    stop_loop				:	0,			// Pauses slideshow on last slide
                    random					: 	0,			// Randomize slide order (Ignores start slide)
                    slide_interval          :   3000,		// Length between transitions
                    transition              :   6, 			// 0-None, 1-Fade, 2-Slide Top, 3-Slide Right, 4-Slide Bottom, 5-Slide Left, 6-Carousel Right, 7-Carousel Left
                    transition_speed		:	1000,		// Speed of transition
                    new_window				:	1,			// Image links open in new window/tab
                    pause_hover             :   0,			// Pause slideshow on hover
                    keyboard_nav            :   1,			// Keyboard navigation on/off
                    performance				:	1,			// 0-Normal, 1-Hybrid speed/quality, 2-Optimizes image quality, 3-Optimizes transition speed // (Only works for Firefox/IE, not Webkit)
                    image_protect			:	1,			// Disables image dragging and right click with Javascript
                    // Size & Position						   
                    min_width		        :   0,			// Min width allowed (in pixels)
                    min_height		        :   0,			// Min height allowed (in pixels)
                    vertical_center         :   1,			// Vertically center background
                    horizontal_center       :   1,			// Horizontally center background
                    fit_always				:	1,			// Image will never exceed browser width or height (Ignores min. dimensions)
                    fit_portrait         	:   0,			// Portrait images will not exceed browser height
                    fit_landscape			:   0,			// Landscape images will not exceed browser width
                    // Components							
                    slide_links				:	'blank',	// Individual links for each slide (Options: false, 'num', 'name', 'blank')
                    thumb_links				:	1,			// Individual thumb links for each slide
                    thumbnail_navigation    :   0,			// Thumbnail navigation
                    slides 					:  	[			// Slideshow Images
                                                <?php foreach ($selectedLinks as $link): ?>
                                                    {image : '<?php echo $link['image']; ?>', title : '<?php echo $link['title']; ?>', thumb : '<?php echo $link['thumb']; ?>', url : '<?php echo $link['image']; ?>'},
                                                <?php endforeach; ?>
                                ]
                            });
                            $('#tray-button').trigger('toggle');
                        });
        </script>
    </head>
    <body>
        <!--Arrow Navigation-->
        <a id="prevslide" class="load-item"></a>
        <a id="nextslide" class="load-item"></a>

        <div id="thumb-tray" class="load-item">
            <div id="thumb-back"></div>
            <div id="thumb-forward"></div>
        </div>

        <!--Control Bar-->
        <div id="controls-wrapper" class="load-item">
            <div id="controls">

                <!--Slide captions displayed here-->
                <div id="slidecaption"></div>

                <!--Navigation-->
                <ul id="slide-list"></ul>

                <!--Thumb Tray button-->
                <a id="tray-button"><img id="tray-arrow" src="vendor/supersized/img/button-tray-up.png"/></a>
            </div>
        </div>
    </body>
</html>