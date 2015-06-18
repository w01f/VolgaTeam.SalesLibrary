<?	/** @var $form CActiveForm */	/** @var $formData LoginForm */	$cs = Yii::app()->clientScript;	$cs->registerCoreScript('jquery');	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/css/jquery.mobile.ios.theme.css?' . Yii::app()->params['version']);	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/login.css?' . Yii::app()->params['version']);	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/js/jquery.mobile.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/login.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);	$this->pageTitle = Yii::app()->name . ' - Login';?><div data-role="page" id="main" data-cache="never" data-dom-cache="false" data-ajax="false" data-theme="a">	<div data-role="content" class="login-content">		<p class="intro-message"><? echo Yii::app()->params['login']['jqmlogintext']; ?></p>		<?			$form = $this->beginWidget('CActiveForm', array(				'action' => Yii::app()->createUrl('auth/login'),				'htmlOptions' => array('data-ajax' => 'false')			));		?>		<div class="edit-fields">			<?				echo $form->textField($formData, 'login'					, array(						'id' => 'field-login',						'class' => 'edit-field',						'placeholder' => 'Username')				);				echo $form->textField($formData, 'password'					, array(						'id' => 'field-password',						'class' => 'edit-field',						'input type' => 'Password',						'placeholder' => 'Password')				);				echo $form->error($formData, 'login'					, array('class' => 'error-message')				);				echo $form->error($formData, 'password'					, array('class' => 'error-message')				);			?>		</div>		<div class="login-button-container">			<?				echo CHtml::submitButton('Log In'					, array('id' => 'button-login',						'class' => 'btn',						'data-theme' => 'd',						'data-role' => 'button',					));			?>		</div>		<? if (Yii::app()->params['login']['rememberMeField']): ?>			<div class="remember-button-container">				<?					echo $form->checkBox($formData, 'rememberMe'						, array(							'id' => 'field-remember',							'data-theme' => 'a')					);					echo $form->labelEx($formData, 'rememberMe'						, array(							'id' => 'label-remember',							'class' => 'ios-checkbox ui-icon-ion-checkmark',							'for' => 'field-remember'						)					);				?>			</div>		<? endif; ?>		<? $this->endWidget(); ?>		<? if (Yii::app()->params['login']['forgotPasswordField']): ?>			<div class="recover-password-button-container">				<a href="#site-help-menu" data-role="button" data-rel="popup" data-theme="a">Site Help</a>			</div>		<? endif; ?>		<? if (Yii::app()->params['login']['disclaimer']): ?>			<div id="disclaimer" class="login-content" data-role="popup" data-theme="a" data-overlay-theme="d" data-dismissible="false">				<div data-role="header" data-theme="d">					<h1>CONFIRM</h1>				</div>				<div role="main" style="padding:0 20px">					<p><? echo Yii::app()->params['login']['disclaimerText']; ?></p>					<div class="login-button-container">						<button id="button-accept-dislaimer" data-role="button" data-theme="d">I Agree</button>					</div>				</div>			</div>		<? endif; ?>	</div>	<div data-role="popup" id="site-help-menu" data-theme="a">		<ul data-role="listview" data-inset="true" style="min-width:250px;" data-corners="false">			<li data-role="list-divider" data-theme="d">Site Help</li>			<li>				<a href="#recover-password" data-transition="slidefade">Reset My Password</a>			</li>			<li>				<a id="site-help-request-link" href="mailto:<?php echo Yii::app()->params['email']['help_request_address']; ?>?subject=Site Help Request - <? echo Yii::app()->request->serverName; ?>" data-rel="external">Email Help Desk</a>			</li>		</ul>	</div></div><div data-role='page' id="recover-password" data-cache="never" data-dom-cache="false" data-ajax="false">	<div data-role='header' class="page-header" data-position="fixed" data-theme="a">		<span class="ui-title header-title">Reset Password</span>	</div>	<div data-role='content' class="login-content">		<div class="edit-fields">			<input id="login" name="login" type="text" value="" placeholder="Type your Username here"/>			<input id="email" name="email" type="text" value="" placeholder="Type your email address here"/>			<div class="error-message"></div>		</div>		<div class="ui-grid-a recover-password-accept-button-container">			<div class="ui-block-a">				<a href="#" id="button-recover-password" data-role="button" data-theme="d">Submit</a>			</div>			<div class="ui-block-b">				<a href="#main" data-role="button" data-theme="d" data-transition="slidefade" data-direction="reverse">Cancel</a>			</div>		</div>		<div class="support-button-container">			<a data-role="button" data-theme="a" data-mini="true" href="mailto:<? echo Yii::app()->params['email']['help_request_address']; ?>?subject=Site Help Request">Click here for Support</a>		</div>	</div></div><div data-role="page" id="recover-password-success" data-cache="never" data-dom-cache="false" data-ajax="false">	<div data-role='header' class="page-header" data-position="fixed" data-theme="a">		<span class="ui-title header-title">Recover Password</span>	</div>	<div data-role="content" class="login-content">		<div class="info-text">A temporary password has been sent <br> Check your inbox of junk mail filter</div>		<div class="back-login-button-container">			<a href="#main" data-role="button" data-theme="d" data-transition="slidefade"			   data-direction="reverse">Back To Login</a> /div>		</div>	</div></div>