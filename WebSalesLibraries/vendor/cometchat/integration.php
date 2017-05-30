<?php

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/* ADVANCED */
	$cms = "yii";
	define('SET_SESSION_NAME', '');    // Session name
	define('SWITCH_ENABLED', '0');
	define('INCLUDE_JQUERY', '1');
	define('FORCE_MAGIC_QUOTES', '0');


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/* DATABASE */

// DO NOT EDIT DATABASE VALUES BELOW
// DO NOT EDIT DATABASE VALUES BELOW
// DO NOT EDIT DATABASE VALUES BELOW

	define('DB_SERVER', 'localhost'); // Database host
	define('DB_PORT', "3306"); // Database port
	define('DB_USERNAME', 'root'); // Database username
	define('DB_PASSWORD', 'root'); // Database password
	define('DB_NAME', 'sales_library'); // Database name
	define('YII_FRAMEWORK', "yii");

	$table_prefix = 'tbl_';                                    // Table prefix(if any)
	$db_usertable = 'user';                            // Users or members information table name
	$db_usertable_userid = 'id';                        // UserID field in the users or members table
	$db_usertable_name = 'login';                    // Name containing field in the users or members table
	$db_avatartable = ' ';
	$db_avatarfield = "case when " . $table_prefix . $db_usertable . "." . $db_usertable_name . " in ('alex', 'billyb') then " . $table_prefix . $db_usertable . "." . $db_usertable_name . " else (select g.name from tbl_user_group ug join tbl_group g on g.id=ug.id_group where ug.id_user=" . $table_prefix . $db_usertable . "." . $db_usertable_userid . ") end";
	$db_linkfield = ' ' . $table_prefix . $db_usertable . '.' . $db_usertable_userid . ' ';
	// Name of Yii framework folder(eg. Yii)
	/*COMETCHAT'S INTEGRATION CLASS USED FOR SITE AUTHENTICATION */

	class Integration
	{

		function __construct()
		{
			if (!defined('TABLE_PREFIX'))
			{
				$this->defineFromGlobal('table_prefix');
				$this->defineFromGlobal('db_usertable');
				$this->defineFromGlobal('db_usertable_userid');
				$this->defineFromGlobal('db_usertable_name');
				$this->defineFromGlobal('db_avatartable');
				$this->defineFromGlobal('db_avatarfield');
				$this->defineFromGlobal('db_linkfield');
			}
		}

		function defineFromGlobal($key)
		{
			if (isset($GLOBALS[$key]))
			{
				define(strtoupper($key), $GLOBALS[$key]);
				unset($GLOBALS[$key]);
			}
		}

		function getUserID()
		{
			return $_SESSION['cometchat_userid'];
		}

		function chatLogin($userName, $userPass)
		{

			$userid = 0;
			global $guestsMode;

			if (filter_var($userName, FILTER_VALIDATE_EMAIL))
			{
				$sql = ("SELECT * FROM `" . TABLE_PREFIX . DB_USERTABLE . "` WHERE email = '" . mysqli_real_escape_string($GLOBALS['dbh'], $userName) . "'");
			}
			else
			{
				$sql = ("SELECT * FROM `" . TABLE_PREFIX . DB_USERTABLE . "` WHERE username = '" . mysqli_real_escape_string($GLOBALS['dbh'], $userName) . "'");
			}
			$result = mysqli_query($GLOBALS['dbh'], $sql);
			$row = mysqli_fetch_assoc($result);

			if ($row['password'] == $userPass)
			{
				$userid = $row[DB_USERTABLE_USERID];
			}

			if (!empty($userName) && !empty($_REQUEST['social_details']))
			{
				$social_details = json_decode($_REQUEST['social_details']);
				$userid = socialLogin($social_details);
			}
			if (!empty($_REQUEST['guest_login']) && $userPass == "CC^CONTROL_GUEST" && $guestsMode == 1)
			{
				$userid = getGuestID($userName);
			}
			if (!empty($userid) && isset($_REQUEST['callbackfn']) && $_REQUEST['callbackfn'] == 'mobileapp')
			{
				$sql = ("insert into cometchat_status (userid,isdevice) values ('" . mysqli_real_escape_string($GLOBALS['dbh'], $userid) . "','1') on duplicate key update isdevice = '1'");
				mysqli_query($GLOBALS['dbh'], $sql);
			}
			if ($userid && function_exists('mcrypt_encrypt') && defined('ENCRYPT_USERID') && ENCRYPT_USERID == '1')
			{
				$key = "";
				if (defined('KEY_A') && defined('KEY_B') && defined('KEY_C'))
				{
					$key = KEY_A . KEY_B . KEY_C;
				}
				$userid = rawurlencode(base64_encode(mcrypt_encrypt(MCRYPT_RIJNDAEL_256, md5($key), $userid, MCRYPT_MODE_CBC, md5(md5($key)))));
			}

			return $userid;
		}

		function getFriendsList($userid, $time)
		{
			global $hideOffline;
			$offlinecondition = '';
			$sql = ("select DISTINCT " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_USERID . " userid, " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_NAME . " username, " . DB_LINKFIELD . " link, " . DB_AVATARFIELD . " avatar, cometchat_status.lastactivity lastactivity, cometchat_status.lastseen lastseen, cometchat_status.lastseensetting lastseensetting, cometchat_status.status, cometchat_status.message, cometchat_status.isdevice, cometchat_status.readreceiptsetting readreceiptsetting from " . TABLE_PREFIX . "friends join " . TABLE_PREFIX . DB_USERTABLE . " on  " . TABLE_PREFIX . "friends.toid = " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_USERID . " left join cometchat_status on " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_USERID . " = cometchat_status.userid " . DB_AVATARTABLE . " where " . TABLE_PREFIX . "friends.fromid = '" . mysqli_real_escape_string($GLOBALS['dbh'], $userid) . "' order by username asc");
			if ((defined('MEMCACHE') && MEMCACHE <> 0) || DISPLAY_ALL_USERS == 1)
			{
				if ($hideOffline)
				{
					$offlinecondition = "where ((cometchat_status.lastactivity > (" . mysqli_real_escape_string($GLOBALS['dbh'], $time) . "-" . ((ONLINE_TIMEOUT) * 2) . ")) OR cometchat_status.isdevice = 1) and (cometchat_status.status IS NULL OR cometchat_status.status <> 'invisible' OR cometchat_status.status <> 'offline')";
				}
				$sql = ("select " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_USERID . " userid, " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_NAME . " username, " . DB_LINKFIELD . " link, " . DB_AVATARFIELD . " avatar, cometchat_status.lastactivity lastactivity, cometchat_status.lastseen lastseen, cometchat_status.lastseensetting lastseensetting, cometchat_status.status, cometchat_status.message, cometchat_status.isdevice, cometchat_status.readreceiptsetting readreceiptsetting from  " . TABLE_PREFIX . DB_USERTABLE . "   left join cometchat_status on " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_USERID . " = cometchat_status.userid " . DB_AVATARTABLE . " " . $offlinecondition . " order by username asc");
			}

			return $sql;
		}

		function getFriendsIds($userid)
		{

			$sql = ("SELECT toid as friendid FROM `friends` WHERE status =1 and fromid = '" . mysqli_real_escape_string($GLOBALS['dbh'], $userid) . "' union SELECT fromid as myfrndids FROM `friends` WHERE status = 1 and toid = '" . mysqli_real_escape_string($GLOBALS['dbh'], $userid) . "'");

			return $sql;
		}

		function getUserDetails($userid)
		{
			$sql = ("select " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_USERID . " userid, " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_NAME . " username, " . DB_LINKFIELD . " link, " . DB_AVATARFIELD . " avatar, cometchat_status.lastactivity lastactivity, cometchat_status.lastseen lastseen, cometchat_status.lastseensetting lastseensetting, cometchat_status.status, cometchat_status.message, cometchat_status.isdevice, cometchat_status.readreceiptsetting readreceiptsetting from " . TABLE_PREFIX . DB_USERTABLE . " left join cometchat_status on " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_USERID . " = cometchat_status.userid " . DB_AVATARTABLE . " where " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_USERID . " = '" . mysqli_real_escape_string($GLOBALS['dbh'], $userid) . "'");

			return $sql;
		}

		function getActivechatboxdetails($userids)
		{
			$sql = ("select DISTINCT " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_USERID . " userid, " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_NAME . " username, " . DB_LINKFIELD . " link, " . DB_AVATARFIELD . " avatar, cometchat_status.lastactivity lastactivity, cometchat_status.lastseen lastseen, cometchat_status.lastseensetting lastseensetting, cometchat_status.status, cometchat_status.message, cometchat_status.isdevice, cometchat_status.readreceiptsetting readreceiptsetting from " . TABLE_PREFIX . DB_USERTABLE . " left join cometchat_status on " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_USERID . " = cometchat_status.userid " . DB_AVATARTABLE . " where " . TABLE_PREFIX . DB_USERTABLE . "." . DB_USERTABLE_USERID . " IN (" . $userids . ")");

			return $sql;
		}

		function getUserStatus($userid)
		{
			$sql = ("select cometchat_status.message, cometchat_status.lastseen lastseen, cometchat_status.lastseensetting lastseensetting, cometchat_status.status from cometchat_status where userid = '" . mysqli_real_escape_string($GLOBALS['dbh'], $userid) . "'");

			return $sql;
		}

		function fetchLink($link)
		{
			return '';
		}

		function getAvatar($image)
		{
			$imagePath = dirname(dirname(__FILE__)) . DIRECTORY_SEPARATOR . '..' . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'avatars';
			$imageNamePng = strtolower($image . '.png');
			if (is_file($imagePath . DIRECTORY_SEPARATOR . 'users' . DIRECTORY_SEPARATOR . $imageNamePng))
				return $_SESSION['cometchat_baseurl'] . '/images/avatars/users/' . $imageNamePng;
			if (is_file($imagePath . DIRECTORY_SEPARATOR . 'groups' . DIRECTORY_SEPARATOR . $imageNamePng))
				return $_SESSION['cometchat_baseurl'] . '/images/avatars/groups/' . $imageNamePng;
			$imageNameSvg = strtolower($image . '.svg');
			if (is_file($imagePath . DIRECTORY_SEPARATOR . 'users' . DIRECTORY_SEPARATOR . $imageNameSvg))
				return $_SESSION['cometchat_baseurl'] . '/images/avatars/users/' . $imageNameSvg;
			if (is_file($imagePath . DIRECTORY_SEPARATOR . 'groups' . DIRECTORY_SEPARATOR . $imageNameSvg))
				return $_SESSION['cometchat_baseurl'] . '/images/avatars/groups/' . $imageNameSvg;
			return BASE_URL . 'images/noavatar.png';
		}

		function getTimeStamp()
		{
			return time();
		}

		function processTime($time)
		{
			return $time;
		}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		/* HOOKS */

		function hooks_message($userid, $to, $unsanitizedmessage)
		{

		}

		function hooks_forcefriends()
		{

		}

		function hooks_updateLastActivity($userid)
		{

		}

		function hooks_statusupdate($userid, $statusmessage)
		{

		}

		function hooks_activityupdate($userid, $status)
		{

		}

	}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	/* LICENSE */

	include_once(dirname(__FILE__) . DIRECTORY_SEPARATOR . 'license.php');
	$x = "\x62a\x73\x656\x34\x5fd\x65c\157\144\x65";
	eval($x('JHI9ZXhwbG9kZSgnLScsJGxpY2Vuc2VrZXkpOyRwXz0wO2lmKCFlbXB0eSgkclsyXSkpJHBfPWludHZhbChwcmVnX3JlcGxhY2UoIi9bXjAtOV0vIiwnJywkclsyXSkpOw'));

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
