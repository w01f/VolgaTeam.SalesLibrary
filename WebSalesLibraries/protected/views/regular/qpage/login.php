<?

	/**
	 * @var $formData PinCodeForm
	 * @var $page QPageRecord
	 */
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/css/bootstrap.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/login.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/js/bootstrap.min.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qpage/login.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>
<script type="text/javascript">
	window.BaseUrl = '<?php echo Yii::app()->getBaseUrl(true); ?>' + '/qpage/';
</script>
<table id="content">
	<tr>
		<td class="content-inner">
			<div class="content-scrollable-area">
				<?
					/** @var $form CActiveForm */
					$form = $this->beginWidget('CActiveForm', array(
						'action' => Yii::app()->createUrl('qpage/show'),
						'htmlOptions' => array(
							'id' => 'form-login',
							'class' => 'form-horizontal',
							'role' => 'form'
						)
					));
					echo $form->textField($formData, 'pageId'
						, array(
							'input type' => 'hidden',
							'class' => 'edit-field')
					);
				?>
				<div class="form-group">
					<div class="col-xs-12 text-center">
						<?
							if (isset($page->logo))
								$logo = $page->logo;
							else
								$logo = Yii::app()->baseUrl . '/images/logo.png';
						?>
						<img id="image-logo" src="<? echo $logo; ?>"/>
					</div>
				</div>
				<div class="form-group">
					<div class="col-xs-12">
						<p class="form-control-static text-center">
							<strong>Enter your 4 Digit Security PIN to access this site:</strong>
						</p>
					</div>
				</div>
				<div class="form-group">
					<div class="col-xs-7">
						<? echo $form->textField($formData, 'pinCode', array('id' => 'pin-code', 'maxlength' => 4, 'class' => 'form-control', 'placeholder' => 'Pin-code')); ?>
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
							<? echo $form->error($formData, 'pinCode'
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