<?php
$version = '2.0';
$cs = Yii::app()->clientScript;
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/login.css?' . $version);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qpage/login.js?' . $version, CClientScript::POS_HEAD);
?>

<div id="content">
	<?php $form = $this->beginWidget('CActiveForm', array('action' => Yii::app()->createUrl('qpage/show'))); ?>
	<table id="form-login">
		<tr>
			<td>
				<img id="image-logo" src="<?php echo Yii::app()->baseUrl . '/images/logo.png'; ?>"/> <br>
				<h5>Enter your 4 Digit Security PIN to access this site:<h5>
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
			<td class="form-inline">
				<?php
				echo $form->textField($formData, 'pinCode', array('id' => 'pin-code','maxlength' => 4, 'class' => 'input-small', 'placeholder' => 'Pin-code'));
				echo CHtml::submitButton('Log In'
					, array('id' => 'button-login',
						'class' => 'btn',
						'style' => 'margin-left:20px;',
					));
				?>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<?php
				echo $form->error($formData, 'pinCode'
					, array('class' => 'error-message')
				);
				?>
				<br>
			</td>
		</tr>
	</table>
	<?php $this->endWidget(); ?>
</div>