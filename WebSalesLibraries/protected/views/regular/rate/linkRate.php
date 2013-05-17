<span class="rate-value <? if ($isRated) echo 'rated'; ?>" data-toggle="tooltip" title="<? if ($isRated): ?> Already Liked! Click  if You want to Unlike<? else: ?>Do you LIKE this file?<? endif; ?>">
	<img src="<?php echo Yii::app()->getBaseUrl(true) . ($isRated ? '/images/rate/unlike.png' : '/images/rate/like.png'); ?>"><? echo $rate > 0 ? $rate : ''; ?>
</span>

