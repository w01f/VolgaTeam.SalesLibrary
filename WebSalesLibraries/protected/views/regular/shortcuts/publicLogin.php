<?
	/**
	 * @var $formData PublicPageContentShortcutPasswordForm
	 * @var $shortcut PageContentShortcut
	 */
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/css/bootstrap.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/login.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/js/bootstrap.min.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/login.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>
<table id="content">
	<tr>
		<td class="content-inner">
			<div class="content-scrollable-area">
				<?
					/** @var $form CActiveForm */
					$form = $this->beginWidget('CActiveForm', array(
						'action' => $shortcut->getSourceLink(),
						'htmlOptions' => array(
							'id' => 'form-login',
							'class' => 'form-horizontal',
							'role' => 'form'
						)
					));
					echo $form->textField($formData, 'shortcutId'
						, array(
							'input type' => 'hidden',
							'class' => 'edit-field')
					);
					echo $form->textField($formData, 'isPhone'
						, array(
							'input type' => 'hidden',
							'class' => 'edit-field')
					);
				?>
				<div class="form-group">
					<div class="col-xs-12 text-center">
						<img id="image-logo" src="<? echo Yii::app()->getBaseUrl(true) . '/images/logo.png'; ?>"/>
					</div>
				</div>
				<div class="form-group">
					<div class="col-xs-12">
						<p class="form-control-static text-center">
							<strong>Enter password to access this site:</strong>
						</p>
					</div>
				</div>
				<div class="form-group">
					<div class="col-xs-7">
						<? echo $form->textField($formData, 'password', array('id' => 'password', 'class' => 'form-control', 'placeholder' => 'Password')); ?>
					</div>
					<div class="col-xs-4 col-xs-offset-1 text-right">
						<? echo CHtml::submitButton('Log In'
							, array('id' => 'button-login',
								'class' => 'btn btn-default',
								'style' => 'margin-left:20px;',
							)); ?>
					</div>
				</div>
				<div class="form-group">
					<div class="col-xs-12 text-center">
						<p class="form-control-static">
							<? echo $form->error($formData, 'password'
								, array('class' => 'error-message')
							); ?>
						</p>
					</div>
				</div>
				<? $this->endWidget(); ?>
			</div>
		</td>
	</tr>
</table>