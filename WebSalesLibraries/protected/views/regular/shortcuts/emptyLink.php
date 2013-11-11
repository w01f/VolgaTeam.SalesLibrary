<div class="shortcuts-link empty"><img src="<? echo $link->imagePath ?>"
		<? if (isset($link->tooltip) && !Yii::app()->browser->isMobile()): ?>
			rel=" tooltip" title="<? echo $link->tooltip; ?>"
		<? endif; ?>
		></div>