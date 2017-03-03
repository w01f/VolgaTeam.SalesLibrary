<?
	namespace application\models\services_data\common\dictionaries;

	use application\models\services_data\common\rest\RestResponse;

	/**
	 * Class DictionariesManager
	 */
	class DictionariesManager
	{
		/**
		 * @return RestResponse
		 */
		public static function getSecurity()
		{
			$groups = array();
			foreach (\GroupRecord::model()->findAll() as $groupRecord)
			{
				/** @var $groupRecord \GroupRecord */
				$group = new \GroupModel();
				$group->id = $groupRecord->id;
				$group->name = $groupRecord->name;

				$userList = array();
				$assignedUsers = \UserGroupRecord::getUserIdsByGroup($groupRecord->id);
				$totalUsers = \UserRecord::model()->count('role<>2');
				$group->allUsers = isset($assignedUsers) && $totalUsers == count($assignedUsers);
				foreach ($assignedUsers as $userId)
				{
					/** @var $userRecord \UserRecord */
					$userRecord = \UserRecord::model()->findByPk($userId);
					if (isset($userRecord))
					{
						$user = new \UserModel();
						$user->id = $userRecord->id;
						$user->login = $userRecord->login;
						$user->firstName = $userRecord->first_name;
						$user->lastName = $userRecord->last_name;
						$user->email = $userRecord->email;
						$userList[] = $user;
					}
				}
				$sortHelper = new \ObjectSortHelper('firstName', 'asc');
				usort($userList, array($sortHelper, 'sort'));
				$group->users = $userList;

				$group->libraryIds = array_values(\GroupLibraryRecord::getLibraryIdsByGroup($groupRecord->id));
				$groups[] = $group;
			}
			$sortHelper = new \ObjectSortHelper('name', 'asc');
			usort($groups, array($sortHelper, 'sort'));
			return RestResponse::success($groups);
		}

		/**
		 * @return RestResponse
		 */
		public static function getCategories()
		{
			$categoryManager = new \CategoryManager();
			$categoryManager->loadCategories();
			return RestResponse::success($categoryManager->tags);
		}

		/**
		 * @return RestResponse
		 */
		public static function getSuperFilters()
		{
			$categoryManager = new \CategoryManager();
			$categoryManager->loadCategories();
			return RestResponse::success($categoryManager->superFilters);
		}

		/**
		 * @return RestResponse
		 */
		public static function getLibraryLinks()
		{
			/** @var \CDbCommand $dbCommand */
			$dbCommand = \Yii::app()->db->createCommand();
			$dbCommand = $dbCommand->select(array(
				'library_id' => 'lb.id as library_id',
				'library_name' => 'lb.name as library_name',
				'page_id' => 'p.id as page_id',
				'page_name' => 'p.name as page_name',
				'page_order' => 'p.order as page_order',
				'folder_id' => 'f.id as folder_id',
				'folder_name' => 'f.name as folder_name',
				'folder_order' => '(f.row_order*100+f.column_order) as folder_order',
				'link_id' => 'l.id as link_id',
				'link_name' => 'l.name as link_name',
				'link_order' => 'l.order as link_order',
				'file_name' => 'l.file_name as file_name'
			));
			$dbCommand = $dbCommand->from('tbl_link l');
			$dbCommand = $dbCommand->join('tbl_folder f', 'f.id = l.id_folder');
			$dbCommand = $dbCommand->join('tbl_page p', 'p.id = f.id_page');
			$dbCommand = $dbCommand->join('tbl_library lb', 'lb.id = p.id_library');
			$dbCommand = $dbCommand->where('l.type not in (5,6)');
			$dbCommand = $dbCommand->order('library_id, page_id, folder_id, link_id');
			$linkRecords = $dbCommand->queryAll();

			$libraries = array();
			foreach ($linkRecords as $linkRecord)
			{
				/** Library $library */
				if (!isset($library) || $library->id != $linkRecord['library_id'])
				{
					$library = new Library();
					$library->id = $linkRecord['library_id'];
					$library->name = $linkRecord['library_name'];
					$library->pages = array();
					$libraries[] = $library;
				}
				/** LibraryPage $libraryPage */
				if (!isset($libraryPage) || $libraryPage->id != $linkRecord['page_id'])
				{
					$libraryPage = new LibraryPage();
					$libraryPage->id = $linkRecord['page_id'];
					$libraryPage->name = $linkRecord['page_name'];
					$libraryPage->order = intval($linkRecord['page_order']);
					$libraryPage->folders = array();
					$library->pages[] = $libraryPage;
				}
				/** LibraryFolder $libraryFolder */
				if (!isset($libraryFolder) || $libraryFolder->id != $linkRecord['folder_id'])
				{
					$libraryFolder = new LibraryFolder();
					$libraryFolder->id = $linkRecord['folder_id'];
					$libraryFolder->name = $linkRecord['folder_name'];
					$libraryFolder->order = intval($linkRecord['folder_order']);
					$libraryFolder->links = array();
					$libraryPage->folders[] = $libraryFolder;
				}
				/** LibraryLink $libraryLink */
				$libraryLink = new LibraryLink();
				$libraryLink->id = $linkRecord['link_id'];
				$libraryLink->name = $linkRecord['link_name'];
				$libraryLink->order = intval($linkRecord['link_order']);
				$libraryLink->fileName = $linkRecord['file_name'];
				$libraryFolder->links[] = $libraryLink;
			}
			return RestResponse::success($libraries);
		}

		/**
		 * @return RestResponse
		 */
		public static function getShortcutLinks()
		{
			/** @var \CDbCommand $dbCommand */
			$dbCommand = \Yii::app()->db->createCommand();
			$dbCommand = $dbCommand->select(array(
				'link_id' => 'l.id as link_id',
				'link_order' => 'l.order as link_order',
				'link_title' => "extractvalue(l.config, 'Config/Regular/Title') as link_title",
				'group_order' => 'g.order as group_order',
				'group_folder' => "substring_index(g.source_path,'/',-1) as group_folder",
				'link_folder' => "substring_index(l.source_path,'/',-1) as link_folder"
			));
			$dbCommand = $dbCommand->from('tbl_shortcut_link l');
			$dbCommand = $dbCommand->join('tbl_shortcut_group g', 'g.id = l.id_group');
			$shortcutRecords = $dbCommand->queryAll();

			$shortcutLinks = array();
			foreach ($shortcutRecords as $shortcutRecord)
			{
				/** ShortcutLink $libraryLink */
				$shortcutLink = new ShortcutLink();
				$shortcutLink->id = $shortcutRecord['link_id'];
				$shortcutLink->order = intval($shortcutRecord['link_order']);
				$shortcutLink->title = $shortcutRecord['link_title'];
				$shortcutLink->groupOrder = $shortcutRecord['group_order'];
				$shortcutLink->groupFolder = $shortcutRecord['group_folder'];
				$shortcutLink->linkFolder = $shortcutRecord['link_folder'];
				$shortcutLinks[] = $shortcutLink;
			}
			return RestResponse::success($shortcutLinks);
		}
	}