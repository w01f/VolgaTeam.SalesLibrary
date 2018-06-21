<?
	/** @var $form CActiveForm */
	/** @var $formData PinCodeForm */

	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/css/jquery.mobile.ios.theme.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/login.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/js/jquery.mobile.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$this->pageTitle = Yii::app()->name . ' - Login';
?>
<div data-role="page" id="main" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role="content" class="login-content">
		<?
			$form = $this->beginWidget('CActiveForm', array(
				'action' => Yii::app()->createUrl('qpage/show'),
				'htmlOptions' => array('data-ajax' => 'false')
			));
		?>
		<div class="edit-fields">
			<?
				$logoFilePath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'logo_mobile.svg';
			?>
			<? if (file_exists($logoFilePath)): ?>
                <div style="width: 100%; padding-top: 20px; padding-bottom: 20px;">
                    <img style="width: 100%;" src="<? echo \Yii::app()->getBaseUrl(true) . '/images/logo_mobile.svg'; ?>"/>
                </div>
			<? endif; ?>
			<h5>Enter your 4 Digit Security PIN to access this site:</h5>
			<?
				echo $form->textField($formData, 'pageId'
					, array('input type' => 'hidden',
						'class' => 'edit-field')
				);
				echo $form->textField($formData, 'pinCode'
					, array(
						'id' => 'field-pin-code',
						'class' => 'edit-field',
						'maxlength' => 4,
						'placeholder' => 'Pin-code')
				);
				echo $form->error($formData, 'pinCode'
					, array('class' => 'error-message')
				);
			?>
		</div>
		<div class="login-button-container">
			<?
				echo CHtml::submitButton('Log In'
					, array('id' => 'button-login',
						'class' => 'btn',
						'data-theme' => 'd',
						'data-role' => 'button',
					));
			?>
		</div>
		<? $this->endWidget(); ?>
	</div
</div>