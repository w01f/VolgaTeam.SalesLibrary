<?php echo $body; ?>
<br>
<?php echo $link; ?>
<br>
<?php if (isset($expiresIn) && $expiresIn != ''): ?>
    This link will expire in <?php echo $expiresIn; ?> days.
<?php endif; ?>
