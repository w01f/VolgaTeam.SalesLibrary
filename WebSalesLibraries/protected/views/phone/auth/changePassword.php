<?	/** @var $form CActiveForm */	/** @var $formData ChangePasswordForm */	$cs = Yii::app()->clientScript;	$cs->registerCoreScript('jquery');	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/jquery.mobile-1.2.0.css?' . Yii::app()->params['version']);	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/jquery.mobile-1.2.0.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/login.css?' . Yii::app()->params['version']);	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/login.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);	$this->pageTitle = Yii::app()->name . ' - Login';?><div data-role="page" id="main" data-cache="never" data-dom-cache="false" data-ajax="false">	<div data-role='header' class="page-header" data-position="fixed" data-theme="b"></div>	<div data-role="content">		<?			$form = $this->beginWidget('CActiveForm', array(				'action' => Yii::app()->createUrl('auth/changePassword'),				'htmlOptions' => array('data-ajax' => 'false')			));		?>		<table class="form-login">			<tr>				<td>					<img id="image-logo" src="<? echo Yii::app()->baseUrl . '/images/logo_phone.png'; ?>"/>				</td>			</tr>			<tr>				<td>					<br> You are logged as: <? echo $formData->login ?>					<br> You need to change your temporary password					<?						echo $form->textField($formData, 'login'							, array(								'input type' => 'hidden',								'class' => 'edit-field')						);						echo $form->textField($formData, 'oldPassword'							, array(								'input type' => 'hidden',								'class' => 'edit-field')						);						echo $form->textField($formData, 'rememberMe'							, array(								'input type' => 'hidden',								'class' => 'edit-field')						);					?>				</td>			</tr>			<tr>				<br>				<td class="title">New Password:</td>			</tr>			<tr>				<td>					<?						echo $form->textField($formData, 'newInitialPassword'							, array(								'input type' => 'Password',								'class' => 'edit-field')						);					?>				</td>			</tr>			<tr>				<td class="title">Type Password Again:</td>			</tr>			<tr>				<td>					<?						echo $form->textField($formData, 'newRepeatPassword'							, array(								'input type' => 'Password',								'class' => 'edit-field')						);					?>				</td>			</tr>			<tr>				<td>					<?						echo $form->error($formData, 'newInitialPassword'							, array('class' => 'error-message')						);						echo $form->error($formData, 'newRepeatPassword'							, array('class' => 'error-message')						);					?>				</td>			</tr>			<tr>				<td>					<?						echo CHtml::submitButton('Save'							, array('class' => 'btn',								'data-theme' => 'b',								'data-role' => 'button',							));					?>				</td>			</tr>		</table>		<? $this->endWidget(); ?>	</div	<div data-role='footer' class="page-header" data-position="fixed" data-theme="b"></div></div>