<?php
$cs = Yii::app()->clientScript;
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/login.js', CClientScript::POS_HEAD);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/login.css');
$this->pageTitle = Yii::app()->name . ' - Login';
echo CHtml::openTag('div', array(
    'class' => 'form'
));
{
    $form = $this->beginWidget('CActiveForm', array('id' => 'formLogin'));
    {
        echo CHtml::image(Yii::app()->baseUrl . '/images/logo.png', ''
            , array(
            'class' => 'row',
            'id' => 'imageLogo')
        );

        echo CHtml::tag('br');
        echo CHtml::tag('br');

        echo $form->textField($loginData, 'login'
            , array(
            'class' => 'row',
            'id' => 'fieldLogin')
        );

        echo CHtml::tag('br');
        echo CHtml::tag('br');

        echo $form->textField($loginData, 'password'
            , array(
            'class' => 'row',
            'id' => 'fieldPassword')
        );

        echo CHtml::openTag('div', array(
            'class' => 'row'
        ));
        {
            echo $form->error($loginData, 'login'
                , array('class' => 'errorMessage')
            );
            echo $form->error($loginData, 'password'
                , array('class' => 'errorMessage')
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
                'id' => 'rememberMeContainer'
            ));
            {
                echo $form->checkBox($loginData, 'rememberMe'
                    , array(
                    'id' => 'fieldRemember')
                );
                echo $form->label($loginData, 'rememberMe'
                    , array('id' => 'lableRemember')
                );
            }
            echo CHtml::closeTag('td'); //rememberMeContainer
            echo CHtml::openTag('td', array(
                'id' => 'buttonLoginContainer')
            );
            echo CHtml::submitButton('Log In'
                , array('id' => 'buttonLogin')
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
            'id' => 'linkRecoverPasswordContainer')
        );
        {
            echo CHtml::ajaxLink('Forgot Password?'
                , array('site/passwordRecover')
                , array('method' => 'POST')
                , array(
                'id' => 'linkRecoverPassword')
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