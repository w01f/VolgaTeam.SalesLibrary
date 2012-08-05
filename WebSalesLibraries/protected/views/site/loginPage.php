<?php
$cs=Yii::app()->clientScript;  
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/login.js', CClientScript::POS_HEAD);  
$cs->registerCssFile(Yii::app()->baseUrl . '/css/login.css');  
$this->pageTitle = Yii::app()->name . ' - Login';
?>
<div class="form">
    <?php $form=$this->beginWidget('CActiveForm', 
                                    array('id'=>'formLogin')); 
    ?>
    <div class="row">        
        <?php echo CHtml::image(Yii::app()->baseUrl.'/images/logo.png', ''
                                ,array('id'=>'imageLogo')                
            ); 
        ?>        
    </div>
    <br> <br>
    <div class="row">
        <?php echo $form->textField($loginData,'login'
                                                ,array('id'=>'fieldLogin')
            ); 
        ?>

    </div>
    <br> <br>
    <div class="row">
        <?php echo $form->textField($loginData,'password'
                                                , array('id'=>'fieldPassword')
            ); 
        ?>
    </div>
    <div class="row">        
        <?php echo $form->error($loginData,'login'
                                , array('class'=>'errorMessage')                
            ); 
        ?>
        <?php echo $form->error($loginData,'password'
                                , array('class'=>'errorMessage')                
            ); 
        ?>
    </div>
    <br> <br>
    <div class="row">
        <?php echo $form->checkBox($loginData,'rememberMe'
                                    , array('id'=>'fieldRemember')                
            ); 
        ?>
        <?php echo $form->label($loginData,'rememberMe'
                                    , array('id'=>'lableRemember')                
            ); 
        ?>
        <?php echo $form->error($loginData,'rememberMe'); ?>
        <?php echo CHtml::submitButton('Log In'
                                        ,array('id'=>'buttonLogin')                
            ); 
        ?>
    </div>
    <br><br><br>
    <div class="row" id ="linkRecoverPasswordContainer">
        <?php echo CHtml::ajaxLink('Forgot Password?'
                                ,array('site/passwordRecover')
                                ,array('method'=>'POST')
                                ,array('id'=>'linkRecoverPassword')
            ); 
        ?>            
    </div>
<?php $this->endWidget(); ?>
</div><!-- form -->