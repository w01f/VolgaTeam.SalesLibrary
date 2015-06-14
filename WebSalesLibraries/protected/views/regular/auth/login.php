<?
	/** @var $form CActiveForm */
	/** @var $formData LoginForm */

	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/login.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/tool-dialog.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/overlay.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/login.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
?>

<div id="content">
	<?
		$form = $this->beginWidget('CActiveForm', array(
			'id' => 'form-login-data',
			'action' => Yii::app()->createUrl('auth/login'),
			'htmlOptions' => array('role' => 'form')
		));
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
				<?
					echo $form->textField($formData, 'login'
						, array(
							'placeholder' => 'Username',
							'class' => 'form-control')
					);
				?>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<?
					echo $form->textField($formData, 'password'
						, array(
							'placeholder' => 'Password',
							'input type' => 'Password',
							'class' => 'form-control')
					);
				?>
			</td>
		</tr>
		<tr>
			<td colspan="2" style="height: auto;">
				<?
					echo $form->error($formData, 'login'
						, array('class' => 'error-message')
					);
					echo $form->error($formData, 'password'
						, array('class' => 'error-message')
					);
				?>
			</td>
		</tr>
		<tr>
			<? if (Yii::app()->params['login']['rememberMeField']): ?>
				<td style="text-align: left">
					<div class="checkbox" style="margin-left: 20px;">
						<?
							echo $form->checkBox($formData, 'rememberMe');
							echo $form->labelEx($formData, 'rememberMe'
								, array('id' => 'label-remember')
							);
						?>
					</div>
				</td>
			<? endif; ?>
			<td style="text-align: right">
				<button type="submit" id="button-login" class="btn btn-default">Log In</button>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<div class="action-link-group">
					<? if (Yii::app()->params['login']['forgotPasswordField']): ?>
						<a id="recover-password-link" class="action-link gray" href="#view-dialog-container">Forgot Password</a>
					<? endif; ?>
					<? if (Yii::app()->params['login']['complex_password']): ?>
						<a id="password-requirements" class="action-link gray" href="#">Password Requirements</a>
					<? endif; ?>
					<a class="action-link gray" href="mailto:<? echo Yii::app()->params['email']['from']; ?>">Site Help</a>
				</div>
			</td>
		</tr>
	</table>
	<? $this->endWidget(); ?>
</div>
<? if (Yii::app()->params['login']['disclaimer']==true): ?>
	<div class="disclaimer-text"><? echo Yii::app()->params['login']['disclaimerText']; ?></div>
<? endif; ?>
<div id="content-overlay">
</div>
<div id="view-dialog-wrapper">
	<div id="view-dialog-container">
	</div>
</div>


