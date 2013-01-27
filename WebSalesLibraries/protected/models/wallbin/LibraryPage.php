<?php
class LibraryPage
{
    public $parent;
    /**
     * @var string name
     * @soap
     */
    public $id;
    /**
     * @var string name
     * @soap
     */
    public $libraryId;
    /**
     * @var string name
     * @soap
     */
    public $name;
    /**
     * @var string libraryName
     * @soap
     */
    public $libraryName;
    /**
     * @var int order
     * @soap
     */
    public $order;
    public $logoPath;
    public $logoLink;
    /**
     * @var LibraryFolder[]
     * @soap
     */
    public $folders;
    /**
     * @var boolean enableColumns
     * @soap
     */
    public $enableColumns;
    /**
     * @var Column[]
     * @soap
     */
    public $columns;
    /**
     * @var string
     * @soap
     */
    public $dateModify;
    /**
     * @var boolean selected
     * @soap
     */
    public $selected;
    /**
     * @var GroupRecord[]
     * @soap
     */
    public $groups;
    /**
     * @var UserRecord[]
     * @soap
     */
    public $users;
    public $cachedColumnsView;

    public function __construct($library)
    {
        $this->parent = $library;
    }

    public function load($pageRecord)
    {
        $this->id = $pageRecord->id;
        $this->libraryId = $pageRecord->id_library;
        $this->name = $pageRecord->name;
        $this->order = $pageRecord->order;
        $this->enableColumns = $pageRecord->has_columns;

        $logoPath = Yii::app()->params['librariesRoot'] . "/Graphics/" . $this->parent->name . "/page" . strval($this->order + 1) . ".png";
        if (file_exists($logoPath))
        {
            $this->logoPath = $logoPath;
            $this->logoLink = str_replace(' ', '%20', htmlspecialchars($logoPath));
        }
        else
        {
            $this->logoPath = $this->parent->logoPath;
            $this->logoLink = str_replace(' ', '%20', htmlspecialchars($this->parent->logoPath));
        }
    }

    public function loadData($browser)
    {
        if (!isset($browser))
        {
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
        }
        unset($this->folders);
        foreach (FolderStorage::model()->findAll('id_page=?', array($this->id)) as $folderRecord)
        {
            $folder = new LibraryFolder($this);
            $folder->browser = $browser;
            $folder->load($folderRecord);
            $this->folders[] = $folder;
        }
        if (isset($this->folders))
            usort($this->folders, "LibraryFolder::libraryFolderComparer");

        unset($this->columns);
        foreach (ColumnStorage::model()->findAll('id_page=?', array($this->id)) as $columnRecord)
        {
            $column = new Column($this);
            $column->load($columnRecord);
            $this->columns[] = $column;
        }

        if (isset($this->columns))
            usort($this->columns, "Column::columnComparer");
    }

    public function loadFolders($allLinks, $userId)
    {
        if (isset($this->folders))
            foreach ($this->folders as $folder)
                $folder->loadFiles($allLinks, $userId);
    }

    public function buildCache($controller)
    {
        $this->buildCacheForBrowser($controller, 'ie', null, false);
        $this->buildCacheForBrowser($controller, 'firefox', null, false);
        $this->buildCacheForBrowser($controller, 'webkit', null, false);
        $this->buildCacheForBrowser($controller, 'opera', null, false);
        $this->buildCacheForBrowser($controller, 'mobile', null, false);

        $adminIds = UserStorage::getAdminUserIds();
        if (isset($adminIds))
            foreach ($adminIds as $userId)
            {
                UserPageCacheStorage::updateData($userId, $this->id, $this->libraryId);
                $this->buildCacheForBrowser($controller, 'ie', $userId, true);
                $this->buildCacheForBrowser($controller, 'firefox', $userId, true);
                $this->buildCacheForBrowser($controller, 'webkit', $userId, true);
                $this->buildCacheForBrowser($controller, 'opera', $userId, true);
                $this->buildCacheForBrowser($controller, 'mobile', $userId, true);
            }

        $restrictedUserIds = UserLinkStorage::getRestrictedUsersIds($this->libraryId);
        if (isset($restrictedUserIds))
            foreach ($restrictedUserIds as $userId)
            {
                if (!isset($adminIds) || (isset($adminIds) && !in_array($userId, $adminIds)))
                {
                    UserPageCacheStorage::updateData($userId, $this->id, $this->libraryId);
                    $this->buildCacheForBrowser($controller, 'ie', $userId, false);
                    $this->buildCacheForBrowser($controller, 'firefox', $userId, false);
                    $this->buildCacheForBrowser($controller, 'webkit', $userId, false);
                    $this->buildCacheForBrowser($controller, 'opera', $userId, false);
                    $this->buildCacheForBrowser($controller, 'mobile', $userId, false);
                }
            }
    }

    private function buildCacheForBrowser($controller, $browser, $userId, $isAdmin)
    {
        $this->loadData($browser);
        $this->loadFolders($isAdmin, $userId);

        $path = Yii::getPathOfAlias('application.views.regular.wallbin') . '/columnsPage.php';
        $content = $controller->renderFile($path, array('libraryPage' => $this), true);

        if (isset($content))
        {
            if ($content != '')
            {
                if (isset($userId))
                {
                    $cacheRecord = UserPageCacheStorage::getPageCache($userId, $this->id);
                }
                else
                {
                    $cacheRecord = LibraryPageStorage::model()->findByPk($this->id);
                }
                if (isset($cacheRecord))
                {
                    switch ($browser)
                    {
                        case 'mobile':
                            $cacheRecord->cached_col_view_mobile = $content;
                            break;
                        case 'ie':
                            $cacheRecord->cached_col_view_ie = $content;
                            break;
                        case 'webkit':
                            $cacheRecord->cached_col_view_webkit = $content;
                            break;
                        case 'firefox':
                            $cacheRecord->cached_col_view_firefox = $content;
                            break;
                        case 'opera':
                            $cacheRecord->cached_col_view_opera = $content;
                            break;
                    }
                    $cacheRecord->save();
                    return true;
                }
            }
        }
        return false;
    }

    public function getCache()
    {
        if (isset(Yii::app()->user))
        {
            $userId = Yii::app()->user->getId();
            if (isset($userId))
                $cacheRecord = UserPageCacheStorage::getPageCache($userId, $this->id);
        }
        if (!isset($cacheRecord))
            $cacheRecord = LibraryPageStorage::model()->findByPk($this->id);
        if (isset($cacheRecord))
        {
            if (Yii::app()->browser->isMobile())
            {
                $cache = $cacheRecord->cached_col_view_mobile;
            }
            else
            {
                $browser = Yii::app()->browser->getBrowser();
                switch ($browser)
                {
                    case 'Internet Explorer':
                        $cache = $cacheRecord->cached_col_view_ie;
                        break;
                    case 'Chrome':
                    case 'Safari':
                        $cache = $cacheRecord->cached_col_view_webkit;
                        break;
                    case 'Firefox':
                        $cache = $cacheRecord->cached_col_view_firefox;
                        break;
                    case 'Opera':
                        $cache = $cacheRecord->cached_col_view_opera;
                        break;
                    default:
                        $cache = $cacheRecord->cached_col_view_webkit;
                        break;
                }
            }
        }
        if (isset($cache))
            return $cache;
        return null;
    }

    public function getFoldersByColumn($columnOrder)
    {
        if (isset($this->folders))
            foreach ($this->folders as $folder)
            {
                if ($folder->columnOrder == $columnOrder)
                    $columnFolders[] = $folder;
            }
        if (isset($columnFolders))
            return $columnFolders;
        else
            return NULL;
    }

    public static function libraryPageComparer($x, $y)
    {
        if ($x->order == $y->order)
            return 0;
        else
            return ($x->order < $y->order) ? -1 : 1;
    }

}
