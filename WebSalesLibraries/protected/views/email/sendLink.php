<?
	/**
	 * @var $body string
	 * @var $link string
	 * @var $expiresIn string
	 */
?>
<? echo $body; ?>
<br>
<? echo $link; ?>
<br>
<? if (isset($expiresIn) && $expiresIn != ''): ?>
	This link will expire in <? echo $expiresIn; ?> days.
<? endif; ?>
