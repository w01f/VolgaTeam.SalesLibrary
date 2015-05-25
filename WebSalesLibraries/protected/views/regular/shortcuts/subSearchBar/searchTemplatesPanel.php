<?
	/**
	 * @var $templates SubSearchTemplate[]
	 * @var $id string
	 */
?>
<div>
	<div class="logo-list">
		<ul class="nav nav-pills">
			<? foreach ($templates as $template): ?>
				<li class="<? if ($template->disabled): ?>disabled<? else: ?>enabled<? endif; ?>">
					<a href="#">
						<img src="<? echo $template->imagePath . '?' . $id; ?>"<? if (!$template->disabled && !Yii::app()->browser->isMobile()): ?> alt="<? echo $template->tooltip; ?>" data-toggle="tooltip" title="<? echo $template->tooltip; ?><? endif; ?>">
						<div class="search-conditions" style="display: none;">
							<div class="encoded-object"><? echo CJSON::encode($template->getSearchOptions()) ?></div>
						</div>
					</a>
				</li>
			<? endforeach; ?>
		</ul>
	</div>
</div>