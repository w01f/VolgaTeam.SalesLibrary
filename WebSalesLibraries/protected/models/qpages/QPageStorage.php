<?
	class QPageStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{qpage}}';
		}

		public function getByOwner($ownerId)
		{
			return $this->findAll('id_owner=?', array($ownerId));
		}

		public function getUrl()
		{
			return Yii::app()->createAbsoluteUrl('qpage/show', array('id' => $this->id));
		}

		public function getUrlInternal()
		{
			if ($this->restricted == true)
				return Yii::app()->createAbsoluteUrl('qpage/getProtected', array('id' => $this->id));
			else
				return Yii::app()->createAbsoluteUrl('qpage/getPublic', array('id' => $this->id));
		}

		public function getLogo()
		{
			if (isset($this->logo))
				return $this->logo;
			else
			{
				$logos = self::getPageLogoList();
				if (isset($logos))
					return $logos[0];
				else
					return null;
			}
		}

		public function getCreateDateFormatted()
		{
			if (isset($this->create_date))
				return date(Yii::app()->params['outputDateFormat'], strtotime($this->create_date)) . ' ' . date(Yii::app()->params['outputTimeFormat'], strtotime($this->create_date));
			else
				return '';
		}

		public function getExpirationDateFormatted()
		{
			if (isset($this->expiration_date) && $this->expiration_date != 0)
				return date(Yii::app()->params['outputDateFormat'], strtotime($this->expiration_date));
			else
				return '';
		}

		public function isExpired()
		{
			return isset($this->expiration_date) && $this->expiration_date != 0 && strtotime(date("Y-m-d")) > strtotime($this->expiration_date);
		}

		public function getPageLinks()
		{
			$linkRecords = Yii::app()->db->createCommand()
				->select("concat('id',qpl.id,'---link',l.id) as id, l.id_library, l.name, l.file_name, l.format, l.type")
				->from('tbl_link l')
				->join('tbl_qpage_link qpl', 'qpl.id_link = l.id')
				->where("qpl.id_page='" . $this->id . "'")
				->queryAll();
			return LinkStorage::getLinksGrid($linkRecords);
		}

		public function getLibraryLinks()
		{
			$links = null;
			if (Yii::app()->browser->isMobile())
				$browser = 'mobile';
			else
			{
				$browser = Yii::app()->browser->getBrowser();
				switch ($browser)
				{
					case 'Internet Explorer':
						$browser = 'ie';
						break;
					case 'Chrome':
					case 'Safari':
						$browser = 'webkit';
						break;
					case 'Firefox':
						$browser = 'firefox';
						break;
					case 'Opera':
						$browser = 'opera';
						break;
					default:
						$browser = 'webkit';
						break;
				}
			}

			$pageLinkRecords = QPageLinkStorage::model()->findAll('id_page=?', array($this->id));
			foreach ($pageLinkRecords as $pageLinkRecord)
			{
				$linkRecord = LinkStorage::getLinkById($pageLinkRecord->id_link);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->browser = $browser;
					$link->load($linkRecord);
					$links[] = $link;
				}
			}
			return $links;
		}

		public function addLink($linkInCartId)
		{
			$linkInCartRecord = UserLinkCartStorage::model()->findByPk($linkInCartId);

			$linkInPageRecord = new QPageLinkStorage();
			$linkInPageRecord->id = uniqid();
			$linkInPageRecord->id_link = $linkInCartRecord->id_link;
			$linkInPageRecord->id_page = $this->id;
			$linkInPageRecord->save();

			$linkInCartRecord->delete();
		}

		public static function addPage($ownerId, $pageTitle, $createDate)
		{
			$pageRecord = new QPageStorage();
			$pageRecord->id = uniqid();
			$pageRecord->id_owner = $ownerId;
			$pageRecord->title = $pageTitle;
			$pageRecord->create_date = date(Yii::app()->params['mysqlDateFormat'], strtotime($createDate));
			$pageRecord->is_email = false;
			$pageRecord->save();
			return $pageRecord->id;
		}

		public static function addPageLite($ownerId, $createDate, $subtitle, $logo, $expirationDate, $restricted, $showLinkToMainSite, $linkId)
		{
			$pageRecord = new QPageStorage();
			$pageRecord->id = uniqid();
			$pageRecord->id_owner = $ownerId;
			$pageRecord->title = 'Shared Link';
			$pageRecord->subtitle = '<h1>' . $subtitle . '<h1>';
			$pageRecord->logo = $logo;
			$pageRecord->create_date = date(Yii::app()->params['mysqlDateFormat'], strtotime($createDate));
			if (isset($expirationDate))
				$pageRecord->expiration_date = $expirationDate;
			$pageRecord->is_email = true;
			$pageRecord->restricted = $restricted;
			$pageRecord->show_site_link = $showLinkToMainSite;
			$pageRecord->save();

			$linkInPageRecord = new QPageLinkStorage();
			$linkInPageRecord->id = uniqid();
			$linkInPageRecord->id_link = $linkId;
			$linkInPageRecord->id_page = $pageRecord->id;
			$linkInPageRecord->save();

			return $pageRecord;
		}

		public static function clonePage($ownerId, $pageTitle, $createDate, $clonePageId)
		{
			$clonedPageRecord = self::model()->findByPk($clonePageId);
			if (isset($clonedPageRecord))
			{
				$pageRecord = new QPageStorage();
				$pageRecord->id = uniqid();
				$pageRecord->id_owner = $ownerId;
				$pageRecord->title = $pageTitle;
				$pageRecord->subtitle = $clonedPageRecord->subtitle;
				$pageRecord->create_date = date(Yii::app()->params['mysqlDateFormat'], strtotime($createDate));
				$pageRecord->expiration_date = $clonedPageRecord->expiration_date;
				$pageRecord->logo = $clonedPageRecord->logo;
				$pageRecord->header = $clonedPageRecord->header;
				$pageRecord->footer = $clonedPageRecord->footer;
				$pageRecord->restricted = $clonedPageRecord->restricted;
				$pageRecord->show_ticker = $clonedPageRecord->show_ticker;
				$pageRecord->show_site_link = $clonedPageRecord->show_site_link;
				$pageRecord->save();

				$clonedPageLinks = QPageLinkStorage::model()->findAll('id_page=?', array($clonePageId));
				foreach ($clonedPageLinks as $clonedPageLink)
				{
					$linkInPageRecord = new QPageLinkStorage();
					$linkInPageRecord->id = uniqid();
					$linkInPageRecord->id_link = $clonedPageLink->id_link;
					$linkInPageRecord->id_page = $pageRecord->id;
					$linkInPageRecord->save();
				}
				return $pageRecord->id;
			}
			return null;
		}

		public static function savePage($pageId, $title, $description, $expirationDate, $logo, $header, $footer, $requireLogin, $showTicker, $showLinkToMainSite)
		{
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
				$pageRecord->show_ticker = $showTicker;
				$pageRecord->show_site_link = $showLinkToMainSite;
				$pageRecord->save();
			}
		}

		public static function deletePage($pageId)
		{
			QPageLinkStorage::model()->findAll('id_page=?', array($pageId));
			self::model()->deleteByPk($pageId);
		}

		public static function deletePagesByOwner($ownerId)
		{
			$pageRecords = self::model()->findAll('id_owner=?', array($ownerId));
			foreach ($pageRecords as $pageRecord)
				self::deletePage($pageRecord->id);
		}

		public static function getPageLogoList()
		{
			$logoFolderPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'qpages' . DIRECTORY_SEPARATOR . 'page-logos';
			$logoFolder = new DirectoryIterator($logoFolderPath);
			$logos = null;
			foreach ($logoFolder as $logoFile)
			{
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

			if (isset($logos))
				return $logos;
		}
	}