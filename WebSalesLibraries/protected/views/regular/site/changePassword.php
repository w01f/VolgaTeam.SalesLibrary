<?php
$version = '3.0';
$cs = Yii::app()->clientScript;
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/login.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/tool-dialog.css?' . $version);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/overlay.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/login.js', CClientScript::POS_HEAD);
$this->pageTitle = Yii::app()->name . ' - Change Password';
?>

<?php $form = $this->beginWidget('CActiveForm', array('action' => Yii::app()->createUrl('site/changePassword'))); ?>
<table id ="form-login">
    <tr>
        <td colspan ="2">
            <img  id="image-logo" src="<?php echo Yii::app()->baseUrl . '/images/logo.png'; ?>" />
        </td>
    </tr>        
    <tr>
        <td colspan ="2">
            <br>
            You are logged as: <?php echo $formData->login ?>
            <br>
            You need to change your temporary password
            <?php
            echo $form->textField($formData, 'login'
                , array(
                'input type' => 'hidden',
                'class' => 'edit-field')
            );
            echo $form->textField($formData, 'oldPassword'
                , array(
                'input type' => 'hidden',
                'class' => 'edit-field')
            );
            echo $form->textField($formData, 'rememberMe'
                , array(
                'input type' => 'hidden',
                'class' => 'edit-field')
            );
            ?>            
        </td>
    </tr>            
    <tr>
        <td colspan ="2" class ="text-field ">
            <br>New Password:
        </td>
    </tr>            
    <tr>
        <td colspan ="2">
            <?php
            echo $form->textField($formData, 'newInitialPassword'
                , array(
                'input type' => 'Password',
                'class' => 'edit-field')
            );
            ?>
        </td>
    </tr>        
    <tr>        
        <td colspan ="2" class ="text-field ">
            <br>Type Password Again:
        </td>        
    </tr>            
    <tr>        
        <td colspan ="2">
            <?php
            echo $form->textField($formData, 'newRepeatPassword'
                , array(
                'input type' => 'Password',
                'class' => 'edit-field')
            );
            ?>
            <br>
        </td>        
    </tr>        
    <tr>        
        <td colspan ="2">
            <?php
            echo $form->error($formData, 'newInitialPassword'
                , array('class' => 'error-message')
            );
            echo $form->error($formData, 'newRepeatPassword'
                , array('class' => 'error-message')
            );
            ?>
            <br>
        </td>        
    </tr>        
    <tr>        
        <td colspan ="2">
            <?php
            echo CHtml::submitButton('Save'
                , array('class' => 'btn'));
            ?>                        
        </td>                            
    </tr>
</table>
<?php $this->endWidget(); ?>

