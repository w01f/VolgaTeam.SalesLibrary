<?php
	/** @var $form CActiveForm */
	/** @var $formData ChangePasswordForm */

	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/css/bootstrap.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/vendor-customization.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/login.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/tool-dialog.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/js/bootstrap.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.pack.js', CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/progress-bar/progress-bar.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/overlay.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/login.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>

<?$form = $this->beginWidget('CActiveForm',
	array(
		'action' => Yii::app()->createUrl('auth/changePassword'),
		'htmlOptions' => array(
			'role' => 'form'
		)));
?>
<table id="form-login">
	<tr>
		<td colspan="2">
			<?
				$logoFolderPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images';
				$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'logo.png'));
			?>
			<img id="image-logo" src="<? echo $imageSource; ?>"/>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			You are logged as: <? echo $formData->login ?><br><b>Create your NEW Password for the site:</b>
			<?
				echo $form->textField($formData, 'login', array('input type' => 'hidden'));
				echo $form->textField($formData, 'oldPassword', array('input type' => 'hidden'));
				echo $form->textField($formData, 'rememberMe', array('input type' => 'hidden'));
			?>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<div class="form-group">
				<label for="edit-field-password">New Password:</label>
				<?
					echo $form->textField($formData, 'newInitialPassword'
						, array(
							'input type' => 'Password',
							'id' => 'edit-field-password',
							'class' => 'form-control')
					);
				?>
			</div>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<div class="form-group">
				<label for="edit-field-password-confirm">Type Password Again:</label>
				<?
					echo $form->textField($formData, 'newRepeatPassword'
						, array(
							'input type' => 'Password',
							'id' => 'edit-field-password-confirm',
							'class' => 'form-control')
					);
				?>
			</div>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<?php
				echo $form->error($formData, 'newInitialPassword', array('class' => 'error-message'));
				echo $form->error($formData, 'newRepeatPassword', array('class' => 'error-message'));
			?>
		</td>
	</tr>
	<tr>
		<? if (Yii::app()->params['login']['complex_password']): ?>
			<td>
				<button type="button" id="password-requirements" class="btn btn-default">Password Requirements</button>
			</td>
		<? endif; ?>
		<td <? if (!Yii::app()->params['login']['complex_password']): ?>colspan="2"<? endif; ?>>
			<button type="submit" id="button-change-password" class="btn btn-default">Save</button>
		</td>
	</tr>
</table>
<? $this->endWidget(); ?>

