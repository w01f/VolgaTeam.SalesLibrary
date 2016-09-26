<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * @var $link LibraryLink
	 * @var $authorized boolean
	 */

	if ($link->isFolder)
		$linkContainerClass = 'folder-link';
	else if ($link->isDirectUrl)
	{
		$linkContainerClass = 'url';
		if($link->isExternalUrl)
			$linkContainerClass .= ' url-external';
		else
			$linkContainerClass .= ' url-internal';
	}
	else
	{
		$isLineBreak = $link->isLineBreak;
		if ($link->isLineBreak)
			$linkContainerClass = 'line-break';
		else
			$linkContainerClass = 'clickable';
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
		<? echo nl2br($link->name); ?>
		<span class="service-data"><? echo $link->getLinkData(); ?></span>
	</a>
</li>
