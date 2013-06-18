<?php
$version = '1.0';
$cs = Yii::app()->clientScript;
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/login.css?' . $version);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qpage/login.js', CClientScript::POS_HEAD);
?>

<div id="content">
	<?php $form = $this->beginWidget('CActiveForm', array('action' => Yii::app()->createUrl('qpage/show'))); ?>
	<table id="form-login">
		<tr>
			<td>
				<img id="image-logo" src="<?php echo Yii::app()->baseUrl . '/images/logo.png'; ?>"/>
				<?php
				echo $form->textField($formData, 'pageId'
					, array(
						'input type' => 'hidden',
						'class' => 'edit-field')
				);
				?>
			</td>
		</tr>
		<tr>
			<td class="text-field">
				<br>
				<?php if (Yii::app()->browser->getBrowser() == 'Internet Explorer'): ?>
					<div>Pin-code:</div>
				<?php endif; ?>
				<?php
				echo $form->textField($formData, 'pinCode',array('class' => 'edit-field','placeholder' => 'Pin-code'));
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
				<br>
			</td>
		</tr>
		<tr>
			<td id="button-login-container">
				<?php
				echo CHtml::submitButton('Log In'
					, array('id' => 'button-login',
						'class' => 'btn'
					));
				?>
			</td>
		</tr>
	</table>
	<?php $this->endWidget(); ?>
</div>