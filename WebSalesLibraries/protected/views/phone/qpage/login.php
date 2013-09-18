<?php
$version = '1.0';
$cs = Yii::app()->clientScript;
$cs->registerCoreScript('jquery');
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile.css?' . $version);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile.js?' . $version, CClientScript::POS_HEAD);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/phone/login.css?' . $version);
$this->pageTitle = Yii::app()->name . ' - Login';
?>
<div data-role="page" id="main" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b"></div>
	<div data-role="content">
		<?php
		$form = $this->beginWidget('CActiveForm', array(
			'action' => Yii::app()->createUrl('qpage/show'),
			'htmlOptions' => array('data-ajax' => 'false')
		));
		?>
		<table class="form-login">
			<tr>
				<td>
					<img id="image-logo" src="<?php echo Yii::app()->baseUrl . '/images/logo_phone.png'; ?>"/>
					<h5>Enter your 4 Digit Security PIN to access this site:<h5>
					<?php
					echo $form->textField($formData, 'pageId'
						, array('input type' => 'hidden',
							'class' => 'edit-field')
					);
					?>
				</td>
			</tr>
			<tr>
				<td>
					<?php
					echo $form->textField($formData, 'pinCode'
						, array(
							'id' => 'field-pin-code',
							'class' => 'edit-field',
							'maxlength' => 4,
							'placeholder' => 'Pin-code')
					);
					?>
				</td>
			</tr>
			<tr>
				<td>
					<?php
					echo $form->error($formData, 'pinCode'
						, array('class' => 'error-message')
					);
					?>
				</td>
			</tr>
			<tr>
				<td style="padding: 2%;">
					<?php
					echo CHtml::submitButton('Log In'
						, array('id' => 'button-login',
							'class' => 'btn',
							'data-theme' => 'b',
							'data-role' => 'button',
						));
					?>
				</td>
			</tr>
		</table>
		<?php $this->endWidget(); ?>
	</div
	<div data-role='footer' class="page-header" data-position="fixed" data-theme="b"></div>
</div>