<?php
	$version = '7.0';
	$cs = Yii::app()->clientScript;
	$cs->registerCoreScript('jquery');
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile-1.2.0.css?' . $version);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile-1.2.0.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/phone/login.css?' . $version);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/login.js', CClientScript::POS_HEAD);
	$this->pageTitle = Yii::app()->name . ' - Login';
?>
<div data-role="page" id="main" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class ="page-header" data-position="fixed" data-theme="b"></div>
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
				<td>
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
			<tr>
				<td>
					<button id="forgot-password" data-role="button" data-theme="b">Forgot Passsord</button>
				</td>
			</tr>
		</table>
		<?php $this->endWidget(); ?>
		<?php if (Yii::app()->browser->isMobile() && !($this->browser == Browser::BROWSER_IPHONE || $this->browser == Browser::BROWSER_ANDROID_MOBILE)): ?>
		<br>
		<table class="form-login">
			<tr>
				<td>
					<button id="button-switch-version" data-role="button" data-theme="b">Switch to Full version</button>
				</td>
			</tr>
		</table>
		<?php endif; ?>
	</div
	<div data-role='footer' class ="page-header" data-position="fixed" data-theme="b"></div>
</div>
<?php if (Yii::app()->params['login']['disclaimer']): ?>
<div data-role="page" id="disclaimer" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class ="page-header" data-position="fixed" data-theme="b"></div>
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
					<br>
					<br>
					<button id="button-accept-dislaimer" data-role="button" data-theme="b">I Agree</button>
				</td>
			</tr>
		</table>
	</div
	<div data-role='footer' class ="page-header" data-position="fixed" data-theme="b"></div>
</div>
<?php endif; ?>