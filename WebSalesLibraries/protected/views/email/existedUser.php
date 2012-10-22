Hello <?php echo $fullName ?>:
<br><br>
Your password was reset.
<br><br>
Your login: <?php echo $login ?><br>
<br>
Your temporary password : <?php echo $password ?><br>
<br>
You may login and change your temporary password now by this link:
<?php
echo Yii::app()->getBaseUrl(true) .Yii::app()->createUrl('site/changePassword', array(
    'login' => $login,
    'password' => $password,
    'rememberMe' => false
))
?>