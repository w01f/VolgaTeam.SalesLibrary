<?php
$version = '3.0';
$cs = Yii::app()->clientScript;
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/login.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/recover-password-dialog.css?' . $version);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/overlay.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/login.js', CClientScript::POS_HEAD);
$this->pageTitle = Yii::app()->name . ' - Login';
?>

<div id="content">
    <?php $form = $this->beginWidget('CActiveForm', array('action' => Yii::app()->createUrl('site/login'))); ?>
    <table id ="form-login">
        <tr>
            <td colspan ="2">
                <img  id="image-logo" src="<?php echo Yii::app()->baseUrl . '/images/logo.png'; ?>" />
            </td>
        </tr>        
        <tr>
            <td colspan ="2">
                <br>
                <?php
                echo $form->textField($formData, 'login'
                    , array(
                    'placeholder' => 'Username',
                    'class' => 'edit-field')
                );
                ?>
            </td>
        </tr>        
        <tr>        
            <td colspan ="2">
                <?php
                echo $form->textField($formData, 'password'
                    , array(
                    'placeholder' => 'Password',
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
                echo $form->error($formData, 'login'
                    , array('class' => 'error-message')
                );
                echo $form->error($formData, 'password'
                    , array('class' => 'error-message')
                );
                ?>
                <br>
            </td>        
        </tr>        
        <tr>        
            <?php if (Yii::app()->params['login']['rememberMeField']): ?>            
                <td id="remember-me-container">
                    <?php
                    echo $form->checkBox($formData, 'rememberMe'
                        , array(
                        'id' => 'field-remember')
                    );
                    echo $form->labelEx($formData, 'rememberMe'
                        , array('id' => 'lable-remember')
                    );
                    ?>            
                </td>                    
            <?php endif; ?>            
            <td id="button-login-container">
                <?php
                echo CHtml::submitButton('Log In'
                    , array('id' => 'button-login',
                    'class' => 'btn'
                ));
                ?>                        
            </td>                            
        </tr>
        <tr>
            <td colspan ="2" id ="recover-password-container">
                <br>
                <br>
                <?php
                if (Yii::app()->params['login']['forgotPasswordField'])
                    echo CHtml::link('Forgot Password?', '#view-dialog-container', array('id' => 'recover-password-link'));
                ?>
            </td>
        </tr>            
    </table>
    <?php $this->endWidget(); ?>
</div>        
<div id="content-overlay">
</div>        
<div id="view-dialog-wrapper">
    <div id="view-dialog-container">
    </div>
</div>


