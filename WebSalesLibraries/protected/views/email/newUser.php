Hello <?php echo $fullName ?>:
<br><br>
<?php echo Yii::app()->params['email']['new_user']['body'] ?>
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