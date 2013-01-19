<?php
$version = '9.0';
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
$this->pageTitle = Yii::app()->name . ' - Login';
?>

<div id="content">
    <?php if (Yii::app()->params['login']['forgotPasswordField']): ?>
        <a id="recover-password-link" href="#view-dialog-container">
            <img src="<?php echo Yii::app()->baseUrl . '/images/login/forgot-password.png'; ?>" alt="Forgot Password?">
        </a>
    <?php endif; ?>
    <?php if (Yii::app()->browser->isMobile()): ?>
        <a id="button-switch-version" href="#">
            <img src="<?php echo Yii::app()->baseUrl . '/images/login/mobile-version.png'; ?>" alt="Switch to Mobile version">
        </a>    
    <?php endif; ?>    
    <?php $form = $this->beginWidget('CActiveForm', array('action' => Yii::app()->createUrl('site/login'))); ?>
    <table id ="form-login">
        <tr>
            <td colspan ="2">
                <img  id="image-logo" src="<?php echo Yii::app()->baseUrl . '/images/logo.png'; ?>" />
            </td>
        </tr>        
        <tr>
            <td colspan ="2" class="text-field">
                <br>
                <?php if (Yii::app()->browser->getBrowser() == 'Internet Explorer'): ?>
                    <div>Username:</div>
                <?php endif; ?>                
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
            <td colspan ="2" class="text-field">
                <?php if (Yii::app()->browser->getBrowser() == 'Internet Explorer'): ?>
                    <div>Password:</div>
                <?php endif; ?>
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
        <?php if (Yii::app()->params['login']['disclaimer']): ?>            
            <tr>
                <td colspan ="2">
                    <div id="disclaimer-container">
                        <label class="checkbox">
                            <input id="disclaimer" type="checkbox" value="">
                            <?php echo Yii::app()->params['login']['disclaimerText']; ?>            
                        </label>
                    </div>
                    <br>
                    <br>
                </td>                    
            </tr>        
        <?php endif; ?>                            
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
                    'class' => Yii::app()->params['login']['disclaimer'] ? 'btn disabled' : 'btn'
                ));
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


