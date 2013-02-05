<?php
	$version = '8.0';
	$cs = Yii::app()->clientScript;
	$cs->registerCoreScript('jquery');
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile-1.2.0.css?' . $version);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile-1.2.0.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/phone/login.css?' . $version);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/login.js', CClientScript::POS_HEAD);
	$this->pageTitle = Yii::app()->name . ' - Login';
?>
<div data-role="page" id="main" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b"></div>
	<div data-role="content">
		<?php
		$form = $this->beginWidget('CActiveForm', array(
			'action' => Yii::app()->createUrl('site/login'),
			'htmlOptions' => array('data-ajax' => 'false')
		));
		?>
		<table class="form-login">
			<tr>
				<td>
					<img id="image-logo" src="<?php echo Yii::app()->baseUrl . '/images/logo_phone.png'; ?>"/>
				</td>
			</tr>
			<tr>
				<td>
					<?php
					echo $form->textField($formData, 'login'
						, array(
							'id' => 'field-login',
							'class' => 'edit-field',
							'placeholder' => 'Username')
					);
					?>
				</td>
			</tr>
			<tr>
				<td>
					<?php
					echo $form->textField($formData, 'password'
						, array(
							'id' => 'field-password',
							'class' => 'edit-field',
							'input type' => 'Password',
							'placeholder' => 'Password')
					);
					?>
				</td>
			</tr>
			<?if (Yii::app()->params['login']['rememberMeField']): ?>
			<tr>
				<td>
					<?php
					echo $form->checkBox($formData, 'rememberMe'
						, array(
							'id' => 'field-remember',
							'data-theme' => 'c')
					);
					echo $form->labelEx($formData, 'rememberMe'
						, array(
							'id' => 'lable-remember',
							'for' => 'field-remember'
						)
					);
					?>
				</td>
			</tr>
			<? endif; ?>
			<tr>
				<td>
					<?php
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
		<br>
		<?php if (Yii::app()->params['login']['forgotPasswordField'] || (Yii::app()->browser->isMobile() && !($this->browser == Browser::BROWSER_IPHONE || $this->browser == Browser::BROWSER_ANDROID_MOBILE))): ?>
		<table width="100%">
			<tr>
				<?php if (Yii::app()->params['login']['forgotPasswordField']): ?>
				<td width="50%" style="padding: 2%;">
					<a id="forgot-password" href="#recover-password" data-role="button" data-theme="b" data-mini="true"
					   data-transition="slidefade">Forgot Passsord</a>
				</td>
				<?php endif; ?>
				<?php if (Yii::app()->browser->isMobile() && !($this->browser == Browser::BROWSER_IPHONE || $this->browser == Browser::BROWSER_ANDROID_MOBILE)): ?>
				<td width="50%" style="padding: 2%;">
					<a id="button-switch-version" data-role="button" data-theme="b" data-mini="true">Full version</a>
				</td>
				<?php endif; ?>
			</tr>
		</table>
		<?php endif; ?>
	</div
	<div data-role='footer' class="page-header" data-position="fixed" data-theme="b"></div>
</div>
<?php if (Yii::app()->params['login']['disclaimer']): ?>
<div data-role="page" id="disclaimer" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b"></div>
	<div data-role="content">
		<table class="form-login">
			<tr>
				<td class="text">
					<span>
						<? echo Yii::app()->params['login']['disclaimerText']; ?>
					</span>
				</td>
			</tr>
			<tr>
				<td>
					<br> <br>
					<button id="button-accept-dislaimer" data-role="button" data-theme="b">I Agree</button>
				</td>
			</tr>
		</table>
	</div
	<div data-role='footer' class="page-header" data-position="fixed" data-theme="b"></div>
</div>
<?php endif; ?>
<div data-role='page' id="recover-password" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<span class="ui-title library-title">Recover Password</span>
	</div>
	<div data-role='content' class="page-content">
		<table class="form-login">
			<tr>
				<td class="title" colspan="2">Login:</td>
			</tr>
			<tr>
				<td colspan="2">
					<input id="login" name="login" type="text" value=""/> <br>
				</td>
			</tr>
			<tr>
				<td class="title" colspan="2">Email:</td>
			</tr>
			<tr>
				<td colspan="2">
					<input id="email" name="email" type="text" value=""/> <br>
				</td>
			</tr>
			<tr>
				<td class="error-message" colspan="2">
					<div></div>
					<br>
				</td>
			</tr>
			<tr>
				<td style="padding: 2%;">
					<a href="#" id="button-recover-password" data-role="button" data-theme="b">Accept</a>
				</td>
				<td style="padding: 2%;">
					<a href="#main" data-role="button" data-theme="b" data-transition="slidefade"
					   data-direction="reverse">Cancel</a>
				</td>
			</tr>
		</table>
	</div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b"></div>
</div>
<div data-role="page" id="recover-password-success" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<span class="ui-title library-title">Recover Password</span>
	</div>
	<div data-role="content">
		<table class="form-login">
			<tr>
				<td class="text">
					<span>
						A temporary password has been sent
						<br>
						Check your inbox of junk mail filter
					</span>
				</td>
			</tr>
			<tr>
				<td>
					<br> <br> <a href="#main" data-role="button" data-theme="b" data-transition="slidefade"
								 data-direction="reverse">Back To Login</a>
				</td>
			</tr>
		</table>
	</div
	<div data-role='footer' class="page-header" data-position="fixed" data-theme="b"></div>
</div>