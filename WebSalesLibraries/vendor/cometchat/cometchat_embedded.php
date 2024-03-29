<?php
/*

CometChat
Copyright (c) 2016 Inscripts
License: https://www.cometchat.com/legal/license

*/

$ccbasedata=null;
if(!empty($_REQUEST['basedata'])) {
	$ccbasedata = $_REQUEST['basedata'];
	include_once(dirname(__FILE__).DIRECTORY_SEPARATOR.'cometchat_init.php');
	setcookie($cookiePrefix."data", $_REQUEST['basedata'], 0, "/");
	$_SESSION['basedata']=$_REQUEST['basedata'];
}else{
	include_once(dirname(__FILE__).DIRECTORY_SEPARATOR.'config.php');
}

$callbackfn='';
if(!empty($_REQUEST['callbackfn'])) {
	$callbackfn = "&callbackfn=".$_REQUEST['callbackfn'];
}
$id = 0;
if(!empty($_REQUEST['user'])){
	$id = $_REQUEST['user'];
}


if(!empty($_REQUEST['chatroomsonly'])) {
	$chatroomsonly = "&chatroomsonly=1";
} else {
	$chatroomsonly = "&chatroomsonly=0";
}

if(!empty($_REQUEST['crid'])) {
	$chatroomid = "&chatroomid=".$_REQUEST['crid'];
} else {
	$chatroomid = "&chatroomid=0";
}

?>

<!DOCTYPE html>
<html>
<head>
	<meta name="viewport" content="user-scalable=0,width=device-width, minimum-scale=1.0, maximum-scale=1.0, initial-scale=1.0" />
	<meta http-equiv="Content-Type" content="text/html" charset="UTF-8"/>
	<title>CometChat</title>
	<script type="text/javascript">
		var embeddedchatroomid = <?php echo (!empty($_REQUEST['crid']) ? $_REQUEST['crid'] : '0').';'; ?>
		var chatroomsonly = <?php echo (!empty($_REQUEST['chatroomsonly']) ? '1' : '0').';'; ?>
	</script>
	<link type="text/css" href="./cometchatcss.php?cc_theme=embedded<?php echo $chatroomid; ?><?php echo $chatroomsonly; ?><?php echo $callbackfn;?>" rel="stylesheet" charset="utf-8">
	<script type="text/javascript" src="./cometchatjs.php?cc_theme=embedded<?php echo $chatroomid; ?><?php echo $chatroomsonly; ?><?php echo $callbackfn;?>" charset="utf-8"></script>
	<script type="text/javascript" src="./js.php?type=plugin&name=transliterate"></script>
	 <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
	<script type="text/javascript">
		function initializeEmbeddedLayout(){
			var embeddedchatroomid = <?php echo (!empty($_REQUEST['crid']) ? $_REQUEST['crid'] : '0').';'; ?>
			var cookiePrefix = '<?php echo (!empty($cookiePrefix) ? $cookiePrefix : 'cc_') ?>';
			var chatroomsonly = <?php echo (!empty($_REQUEST['chatroomsonly']) ? '1' : '0').';'; ?>
			jqcc(document).ready(function(){
				if (embeddedchatroomid != 0 && embeddedchatroomid !='null' && typeof(embeddedchatroomid) != "undefined" && typeof(jqcc.cometchat.chatroomHeartbeat) == "function") {
					var id = '1^'+embeddedchatroomid;
					if(typeof(jqcc.cookie(cookiePrefix+'state')) == 'undefined'){
						jqcc.cookie(cookiePrefix+'state', '1:2:_'+embeddedchatroomid+'|1|', {path: '/'});
					}
					jqcc.cometchat.chatroomHeartbeat(id);
				}
				google.load("elements", "1", {
					packages: "transliteration"
				});
				<?php if(!empty($ccbasedata)){ ?>
								
				setTimeout(function(){
					jqcc.cookie('<?php echo $cookiePrefix; ?>data','<?php echo $ccbasedata; ?>');
					jqcc.cometchat.reinitialize();
				},2000);

				<?php } ?>
				
				document.addEventListener("dragover",function(e){
					e = e || event;
					e.preventDefault();
				},false);

				document.addEventListener("drop",function(e){
					e = e || event;
					e.preventDefault();
				},false);

				var uid = '<?php echo $id; ?>';
				if(uid > 0){
					var controlparameters = {"type":"core", "name":"cometchat", "method":"chatWith", "params":{"uid":uid, "embedded":"1"}};
					controlparameters = JSON.stringify(controlparameters);
					parent.postMessage('CC^CONTROL_'+controlparameters,'*');
				}
			});
		}
	</script>
</head>
<body style="overflow: hidden;">
</body>
</html>
