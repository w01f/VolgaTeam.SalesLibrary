<?
	/** @var $shortcut VideoShortcut */
?>
<video id="video-player" class="video-js vjs-default-skin" controls preload="auto" width="100%" data-setup="{'autoplay':false}">
	<source src="<? echo $shortcut->sourceLink; ?>" type="video/mp4"/>
</video>'