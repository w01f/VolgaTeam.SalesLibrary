<?php
$version = '5.0';
$cs = Yii::app()->clientScript;
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/login.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/tool-dialog.css?' . $version);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/overlay.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/login.js', CClientScript::POS_HEAD);
?>

<?php $form = $this->beginWidget('CActiveForm', array('action' => Yii::app()->createUrl('site/changePassword'))); ?>
<table id="form-login">
	<tr>
		<td colspan="2">
			<img id="image-logo" src="<?php echo Yii::app()->baseUrl . '/images/logo.png'; ?>"/>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<br> You are logged as: <?php echo $formData->login ?>
			<br><b>Create your NEW Password for the site:</b>
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
		<td colspan="2" class="text-field ">
			<br>New Password:
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<?php
			echo $form->textField($formData, 'newInitialPassword'
				, array(
					'input type' => 'Password',
					'id' => 'edit-field-password',
					'class' => 'edit-field')
			);
			?>
		</td>
	</tr>
	<tr>
		<td colspan="2" class="text-field ">
			<br>Type Password Again:
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<?php
			echo $form->textField($formData, 'newRepeatPassword'
				, array(
					'input type' => 'Password',
					'id' => 'edit-field-password-confirm',
					'class' => 'edit-field')
			);
			?>
			<br>
		</td>
	</tr>
	<tr>
		<td colspan="2">
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
		<? if (Yii::app()->params['login']['complex_password']): ?>
			<td>
				<a id="password-requirements" class="btn">Password Requirements</a>
			</td>
		<? endif;?>
		<td <? if (!Yii::app()->params['login']['complex_password']): ?>colspan="2"<?endif;?>>
			<?php
			echo CHtml::submitButton('Save', array('id' => 'button-change-password', 'class' => 'btn'));
			?>
		</td>
	</tr>
</table>
<?php $this->endWidget(); ?>

