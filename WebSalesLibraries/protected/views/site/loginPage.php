<?php
$cs = Yii::app()->clientScript;
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/login.js', CClientScript::POS_HEAD);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/login.css');
$this->pageTitle = Yii::app()->name . ' - Login';
echo CHtml::openTag('div', array(
    'class' => 'form'
));
{
    $form = $this->beginWidget('CActiveForm', array('id' => 'form-login'));
    {
        echo CHtml::image(Yii::app()->baseUrl . '/images/logo.png', ''
            , array(
            'class' => 'row',
            'id' => 'image-logo')
        );

        echo CHtml::tag('br');
        echo CHtml::tag('br');

        echo $form->textField($loginData, 'login'
            , array(
            'class' => 'row',
            'id' => 'field-login')
        );

        echo CHtml::tag('br');
        echo CHtml::tag('br');

        echo $form->textField($loginData, 'password'
            , array(
            'class' => 'row',
            'id' => 'field-password')
        );

        echo CHtml::openTag('div', array(
            'class' => 'row'
        ));
        {
            echo $form->error($loginData, 'login'
                , array('class' => 'error-message')
            );
            echo $form->error($loginData, 'password'
                , array('class' => 'error-message')
            );
        }
        echo CHtml::closeTag('div'); //row        

        echo CHtml::tag('br');
        echo CHtml::tag('br');

        echo CHtml::openTag('table', array(
            'class' => 'row'
        ));
        echo CHtml::openTag('tr');
        {
            echo CHtml::openTag('td', array(
                'id' => 'remember-me-container'
            ));
            {
                echo $form->checkBox($loginData, 'rememberMe'
                    , array(
                    'id' => 'field-remember')
                );
                echo $form->label($loginData, 'rememberMe'
                    , array('id' => 'lable-remember')
                );
            }
            echo CHtml::closeTag('td'); //rememberMeContainer
            echo CHtml::openTag('td', array(
                'id' => 'button-login-container')
            );
            echo CHtml::submitButton('Log In'
                , array('id' => 'button-login')
            );
            echo CHtml::closeTag('td');
        }
        echo CHtml::closeTag('tr'); //row
        echo CHtml::closeTag('table'); //row

        echo CHtml::tag('br');
        echo CHtml::tag('br');
        echo CHtml::tag('br');

        echo CHtml::openTag('table', array(
            'class' => 'row'
        ));
        echo CHtml::openTag('tr');
        echo CHtml::openTag('td', array(
            'id' => 'recover-password-container')
        );
        {
            echo CHtml::ajaxLink('Forgot Password?'
                , array('site/passwordRecover')
                , array('method' => 'POST')
                , array(
                'id' => 'recover-password-link')
            );
        }
        echo CHtml::closeTag('td');
        echo CHtml::closeTag('tr'); //row
        echo CHtml::closeTag('table'); //row
    }
    $this->endWidget();
}
echo CHtml::closeTag('div'); //form
?>