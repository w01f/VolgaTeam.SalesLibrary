<?

	/**
	 * Class QPageRecord
	 * @property mixed id
	 * @property mixed id_owner
	 * @property mixed restricted
	 * @property mixed list_order
	 * @property string title
	 * @property mixed create_date
	 * @property mixed is_email
	 * @property mixed subtitle
	 * @property mixed header
	 * @property mixed footer
	 * @property string expiration_date
	 * @property bool disable_banners
	 * @property bool disable_widgets
	 * @property bool show_links_as_url
	 * @property bool record_activity
	 * @property bool activity_email_copy
	 * @property string pin_code
	 * @property string logo
	 */
	class QPageRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return QPageRecord
		 */
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{qpage}}';
		}

		/**
		 * @param $ownerId
		 * @return QPageRecord[]
		 */
		public function getByOwner($ownerId)
		{
			return $this->findAll('id_owner=? order by list_order', array($ownerId));
		}

		/**
		 * @return string
		 */
		public function getUrl()
		{
			return Yii::app()->createAbsoluteUrl('qpage/show', array('id' => $this->id));
		}

		/**
		 * @return string
		 */
		public function getUrlInternal()
		{
			if ($this->restricted == true)
				return Yii::app()->createAbsoluteUrl('qpage/getProtected', array('id' => $this->id));
			else
				return Yii::app()->createAbsoluteUrl('qpage/getPublic', array('id' => $this->id));
		}

		/**
		 * @return mixed|null
		 */
		public function getLogo()
		{
			return $this->logo;
		}

		/**
		 * @return string
		 */
		public function getCreateDateFormatted()
		{
			if (isset($this->create_date))
				return date(Yii::app()->params['outputDateFormat'], strtotime($this->create_date)) . ' ' . date(Yii::app()->params['outputTimeFormat'], strtotime($this->create_date));
			else
				return '';
		}

		/**
		 * @return string
		 */
		public function getExpirationDateFormatted()
		{
			if (isset($this->expiration_date) && $this->expiration_date != 0)
				return date(Yii::app()->params['outputDateFormat'], strtotime($this->expiration_date));
			else
				return '';
		}

		/**
		 * @return bool
		 */
		public function isExpired()
		{
			return isset($this->expiration_date) && $this->expiration_date != 0 && strtotime(date("Y-m-d")) > strtotime($this->expiration_date);
		}

		/**
		 * @return array|null
		 */
		public function getPageLinks()
		{
			$linkRecords = Yii::app()->db->createCommand()
				->select("concat('id',qpl.id,'---link',l.id) as id, l.id_library, l.name, l.file_name, l.format, l.type")
				->from('tbl_link l')
				->join('tbl_qpage_link qpl', 'qpl.id_link = l.id')
				->where("qpl.id_page='" . $this->id . "'")
				->order('qpl.list_order, l.name')
				->queryAll();
			return LinkRecord::getLinksGrid($linkRecords);
		}

		/**
		 * @return LibraryLink[]
		 */
		public function getLibraryLinks()
		{
			$links = array();
			$pageLinkRecords = QPageLinkRecord::model()->findAll('id_page=? order by list_order', array($this->id));
			foreach ($pageLinkRecords as $pageLinkRecord)
			{
				/** @var $linkRecord LinkRecord */
				$linkRecord = LinkRecord::getLinkById($pageLinkRecord->id_link);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->browser = Utils::getBrowser();
					$link->load($linkRecord);
					$links[] = $link;
				}
			}
			return $links;
		}

		/**
		 * @param $linkInCartId
		 * @param $order
		 */
		public function addLink($linkInCartId, $order)
		{
			if ($order >= 0)
				$this->rebuildLinkList($order);
			else
				$order = QPageLinkRecord::getMaxLinkIndex($this->id) + 1;
			/** @var $linkInCartRecord UserLinkCartRecord */
			$linkInCartRecord = UserLinkCartRecord::model()->findByPk($linkInCartId);
			$linkInPageRecord = new QPageLinkRecord();
			$linkInPageRecord->id = uniqid();
			$linkInPageRecord->id_link = $linkInCartRecord->id_link;
			$linkInPageRecord->id_page = $this->id;
			$linkInPageRecord->list_order = $order;
			$linkInPageRecord->save();
			$linkInCartRecord->delete();
		}

		/**
		 * @param $linkInPageId
		 * @param $order
		 */
		public function setLinkOrder($linkInPageId, $order)
		{
			$this->rebuildLinkList($order);
			/** @var $linkInPageRecord QPageLinkRecord */
			$linkInPageRecord = QPageLinkRecord::model()->findByPk($linkInPageId);
			$linkInPageRecord->list_order = $order;
			$linkInPageRecord->save();
		}

		/**
		 * @param $shiftIndex
		 */
		public function rebuildLinkList($shiftIndex)
		{
			$i = 0;
			foreach (QPageLinkRecord::model()->findAll('id_page=? order by list_order', array($this->id)) as $pageLink)
			{
				if ($i == $shiftIndex)
					$i++;
				/** @var $pageLink QPageLinkRecord */
				$pageLink->list_order = $i;
				$pageLink->save();
				$i++;
			}
		}

		/**
		 * @param $ownerId int
		 * @param $pageTitle string
		 * @param $createDate string
		 * @return string
		 */
		public static function addPage($ownerId, $pageTitle, $createDate)
		{
			$pageRecord = new QPageRecord();
			$pageRecord->id = uniqid();
			$pageRecord->id_owner = $ownerId;
			$pageRecord->list_order = self::getMaxPageIndex() + 1;
			$pageRecord->title = $pageTitle;
			$pageRecord->create_date = date(Yii::app()->params['mysqlDateFormat'], strtotime($createDate));
			$pageRecord->is_email = false;
			$pageRecord->save();
			return $pageRecord->id;
		}

		/**
		 * @param $ownerId int
		 * @param $createDate string
		 * @param $subtitle string
		 * @param $logo string
		 * @param $expirationDate string
		 * @param $restricted boolean
		 * @param $disableBanners boolean
		 * @param $disableWidgets boolean
		 * @param $showLinksAsUrl boolean
		 * @param $recordActivity boolean
		 * @param $pinCode string
		 * @param $activityEmailCopy boolean
		 * @param $linkId string
		 * @return QPageRecord
		 */
		public static function addPageLite($ownerId, $createDate, $subtitle, $logo, $expirationDate, $restricted, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy, $linkId)
		{
			$pageRecord = new QPageRecord();
			$pageRecord->id = uniqid();
			$pageRecord->id_owner = $ownerId;
			$pageRecord->list_order = self::getMaxPageIndex() + 1;
			$pageRecord->title = 'Shared Link';
			$pageRecord->subtitle = '<h1>' . $subtitle . '<h1>';
			$pageRecord->logo = $logo;
			$pageRecord->create_date = date(Yii::app()->params['mysqlDateFormat'], strtotime($createDate));
			if (isset($expirationDate))
				$pageRecord->expiration_date = $expirationDate;
			$pageRecord->is_email = true;
			$pageRecord->restricted = $restricted;
			$pageRecord->disable_banners = $disableBanners;
			$pageRecord->disable_widgets = $disableWidgets;
			$pageRecord->show_links_as_url = $showLinksAsUrl;
			$pageRecord->record_activity = $recordActivity;
			$pageRecord->pin_code = $pinCode;
			$pageRecord->activity_email_copy = $activityEmailCopy;
			$pageRecord->save();

			$linkInPageRecord = new QPageLinkRecord();
			$linkInPageRecord->id = uniqid();
			$linkInPageRecord->id_link = $linkId;
			$linkInPageRecord->id_page = $pageRecord->id;
			$linkInPageRecord->save();

			return $pageRecord;
		}

		/**
		 * @param $ownerId
		 * @param $pageTitle
		 * @param $createDate
		 * @param $clonePageId
		 * @return null|string
		 */
		public static function clonePage($ownerId, $pageTitle, $createDate, $clonePageId)
		{
			/** @var $clonedPageRecord QPageRecord */
			$clonedPageRecord = self::model()->findByPk($clonePageId);
			if (isset($clonedPageRecord))
			{
				$pageRecord = new QPageRecord();
				$pageRecord->id = uniqid();
				$pageRecord->id_owner = $ownerId;
				$pageRecord->list_order = self::getMaxPageIndex() + 1;
				$pageRecord->title = $pageTitle;
				$pageRecord->subtitle = $clonedPageRecord->subtitle;
				$pageRecord->create_date = date(Yii::app()->params['mysqlDateFormat'], strtotime($createDate));
				$pageRecord->expiration_date = $clonedPageRecord->expiration_date;
				$pageRecord->logo = $clonedPageRecord->logo;
				$pageRecord->header = $clonedPageRecord->header;
				$pageRecord->footer = $clonedPageRecord->footer;
				$pageRecord->restricted = $clonedPageRecord->restricted;
				$pageRecord->disable_banners = $clonedPageRecord->disable_banners;
				$pageRecord->disable_widgets = $clonedPageRecord->disable_widgets;
				$pageRecord->show_links_as_url = $clonedPageRecord->show_links_as_url;
				$pageRecord->record_activity = $clonedPageRecord->record_activity;
				$pageRecord->pin_code = $clonedPageRecord->pin_code;
				$pageRecord->activity_email_copy = $clonedPageRecord->activity_email_copy;
				$pageRecord->save();

				$clonedPageLinks = QPageLinkRecord::model()->findAll('id_page=?', array($clonePageId));
				foreach ($clonedPageLinks as $clonedPageLink)
				{
					$linkInPageRecord = new QPageLinkRecord();
					$linkInPageRecord->id = uniqid();
					$linkInPageRecord->id_link = $clonedPageLink->id_link;
					$linkInPageRecord->id_page = $pageRecord->id;
					$linkInPageRecord->save();
				}
				return $pageRecord->id;
			}
			return null;
		}

		/**
		 * @param $pageId
		 * @param $title
		 * @param $description
		 * @param $expirationDate
		 * @param $logo
		 * @param $header
		 * @param $footer
		 * @param $requireLogin
		 * @param $disableBanners
		 * @param $disableWidgets
		 * @param $showLinksAsUrl
		 * @param $recordActivity
		 * @param $pinCode
		 * @param $activityEmailCopy
		 */
		public static function savePage($pageId, $title, $description, $expirationDate, $logo, $header, $footer, $requireLogin, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy)
		{
			/** @var $pageRecord QPageRecord */
			$pageRecord = self::model()->findByPk($pageId);
			if (isset($pageRecord))
			{
				$pageRecord->title = $title;
				$pageRecord->subtitle = $description;
				$pageRecord->expiration_date = $expirationDate;
				$pageRecord->logo = $logo;
				$pageRecord->header = $header;
				$pageRecord->footer = $footer;
				$pageRecord->restricted = $requireLogin;
				$pageRecord->disable_banners = $disableBanners;
				$pageRecord->disable_widgets = $disableWidgets;
				$pageRecord->show_links_as_url = $showLinksAsUrl;
				$pageRecord->record_activity = $recordActivity;
				$pageRecord->pin_code = $pinCode;
				$pageRecord->activity_email_copy = $activityEmailCopy;
				$pageRecord->save();
			}
		}

		/**
		 * @param $pageId
		 */
		public static function deletePage($pageId)
		{
			/** @var $pageRecord QPageRecord */
			$pageRecord = self::model()->findByPk($pageId);
			$ownerId = $pageRecord->id_owner;
			QPageLinkRecord::model()->deleteAll('id_page=?', array($pageId));
			$pageRecord->delete();
			self::rebuildPageList($ownerId, -1);
		}

		/**
		 * @param $ownerId
		 */
		public static function deletePagesByOwner($ownerId)
		{
			$pageRecords = self::model()->findAll('id_owner=?', array($ownerId));
			foreach ($pageRecords as $pageRecord)
				self::deletePage($pageRecord->id);
			self::rebuildPageList($ownerId, -1);
		}

		/**
		 * @param $userId
		 * @param $pageId
		 * @param $order
		 */
		public static function setPageOrder($userId, $pageId, $order)
		{
			self::rebuildPageList($userId, $order);
			/** @var $pageRecord QPageRecord */
			$pageRecord = QPageRecord::model()->findByPk($pageId);
			$pageRecord->list_order = $order;
			$pageRecord->save();
		}

		/**
		 * @param $userId
		 * @param $shiftIndex
		 */
		public static function rebuildPageList($userId, $shiftIndex)
		{
			$i = 0;
			foreach (self::model()->findAll('id_owner=? order by list_order', array($userId)) as $page)
			{
				if ($i == $shiftIndex)
					$i++;
				/** @var $page QPageRecord */
				$page->list_order = $i;
				$page->save();
				$i++;
			}
		}

		/**
		 * @return int|mixed
		 */
		public static function getMaxPageIndex()
		{
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				return Yii::app()->db->createCommand()
					->select('max(list_order)')
					->from('tbl_qpage')
					->where("id_owner='" . $userId . "'")
					->queryScalar();
			}
			else
				return -1;
		}

		/**
		 * @return array
		 */
		public static function getPageLogoList()
		{
			$logoFolderPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'qpages' . DIRECTORY_SEPARATOR . 'page-logos';
			$logoFolder = new DirectoryIterator($logoFolderPath);
			$logos = array();
			foreach ($logoFolder as $logoFile)
			{
				/** @var $logoFile DirectoryIterator */
				if ($logoFile->isFile())
				{
					$type = 'png';
					$path = $logoFile->getPathname();
					$name = $logoFile->getFilename();
					$data = file_get_contents($path);
					if ($name == 'default.png')
						$default = 'data:image/' . $type . ';base64,' . base64_encode($data);
					else
						$stationLogos[$name] = 'data:image/' . $type . ';base64,' . base64_encode($data);
				}
			}
			if (isset($stationLogos))
				ksort($stationLogos);

			if (isset($default) && isset($stationLogos))
				$logos = array_merge((array)$default, (array)$stationLogos);
			else if (isset($default))
				$logos[] = $default;
			else if (isset($stationLogos))
				$logos = $stationLogos;

			return $logos;
		}
	}