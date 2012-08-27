Hello <?php echo $fullName ?>:
<br><br>
<?php echo Yii::app()->params['email']['new_user']['body'] ?>
<br><br>
Your login: <?php echo $login ?><br>
<br>
Your password: <?php echo $password ?><br>