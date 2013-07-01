Hello <?php echo $fullName ?>:
<br><br>
<?php echo Yii::app()->params['email']['new_user']['body'] ?>
<br><br>
Account Username: <?php echo $login ?><br><br>
Click the link below to create your NEW Password:<br><br>
<?php
echo Yii::app()->getBaseUrl(true) .Yii::app()->createUrl('site/changePassword', array(
    'login' => $login,
    'password' => $password,
    'rememberMe' => false
))
?>