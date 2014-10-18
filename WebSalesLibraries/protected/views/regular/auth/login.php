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
	<? if (Yii::app()->browser->isMobile()): ?>
		<a id="button-switch-version" href="#">
			<img src="<? echo Yii::app()->baseUrl . '/images/auth/mobile-version.png'; ?>" alt="Switch to Mobile version">
		</a>
	<? endif; ?>
	<?
		$form = $this->beginWidget('CActiveForm', array(
			'action' => Yii::app()->createUrl('auth/login'),
			'htmlOptions' => array(
				'role' => 'form'
			)
		));
	?>
	<table id="form-login">
		<tr>
			<td colspan="2">
				<img id="image-logo" src="<? echo Yii::app()->baseUrl . '/images/logo.png'; ?>"/>
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
		<? if (Yii::app()->params['login']['forgotPasswordField']): ?>
			<tr>
				<td colspan="2">
					<a id="recover-password-link" href="#view-dialog-container">
						<img src="<? echo Yii::app()->baseUrl . '/images/auth/forgot-password.png'; ?>" alt="Forgot Password?">
					</a>
				</td>
			</tr>
		<? endif; ?>
		<? if (Yii::app()->params['login']['complex_password']): ?>
			<tr>
				<td colspan="2">
					<button type="button" id="password-requirements" class="btn btn-default">Password Requirements</button>
				</td>
			</tr>
		<? endif; ?>
		<? if (Yii::app()->params['login']['disclaimer']): ?>
			<tr>
				<td colspan="2">
					<div id="disclaimer-container" class="checkbox">
						<label>
							<input id="disclaimer" type="checkbox" value="">
							<? echo Yii::app()->params['login']['disclaimerText']; ?>
						</label>
					</div>
				</td>
			</tr>
		<? endif; ?>
		<tr>
			<? if (Yii::app()->params['login']['rememberMeField']): ?>
				<td style="text-align: left">
					<div class="checkbox">
						<?
							echo $form->checkBox($formData, 'rememberMe');
							echo $form->labelEx($formData, 'rememberMe'
								, array('id' => 'label-remember')
							);
						?>
					</div>
				</td>
			<? endif; ?>
			<td>
				<button type="submit" id="button-login" class="btn btn-default">Log In</button>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<a href="mailto:<? echo Yii::app()->params['email']['from']; ?>" style="text-decoration: underline;"><strong>Need an Account?</strong></a>
			</td>
		</tr>
	</table>
	<? $this->endWidget(); ?>
</div>
<div id="content-overlay">
</div>
<div id="view-dialog-wrapper">
	<div id="view-dialog-container">
	</div>
</div>


