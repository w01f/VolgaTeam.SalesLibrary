<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * @var $link LibraryLink
	 * @var $authorized boolean
	 * @var $disableWidget boolean
	 */

	if ($link->isFolder)
		$linkContainerClass = 'link-container folder-link';
	else if ($link->isDirectUrl)
	{
		$linkContainerClass = 'link-container url';
		if ($link->isExternalUrl)
			$linkContainerClass .= ' url-external';
		else
			$linkContainerClass .= ' url-internal';
	}
	else
	{
		$isLineBreak = $link->isLineBreak;
		if ($link->isLineBreak)
			$linkContainerClass = 'link-container line-break';
		else
			$linkContainerClass = 'link-container clickable';
	}
	if ($link->isAppLink)
		$linkContainerClass .= ' hidden-app-link';
	if ($link->extendedProperties->isRestricted)
		$linkContainerClass .= ' restricted';
	if ($authorized)
		$linkContainerClass .= ' log-activity';

?>
<li>
    <a href="<? echo $link->isDirectUrl ? $link->fileLink : '#'; ?>" target="_blank" id="link<? echo $link->id; ?>"
       class="<? echo $linkContainerClass; ?>"
       style="text-decoration: underline;">
        <div class="link-text-container">
			<? if (!$disableWidget): ?>
				<? $widget = $link->getWidget('#ffffff'); ?>
				<? if (isset($widget) && $widget != ''): ?>
                    <div class="link-widget link-widget-no-wrap">
                        <img src="data:image/png;base64,<? echo $widget; ?>">
                    </div>
				<? endif; ?>
			<? endif; ?>
            <div class="link-text link-text-no-wrap">
				<? echo nl2br($link->name); ?>
            </div>
        </div>
		<? if ($link->isFolder): ?>
            <div class="folder-link-content" id="folder-link-content<? echo $link->id; ?>"></div>
		<? endif; ?>
        <span class="service-data"><? echo $link->getLinkData(); ?></span>
    </a>
</li>
