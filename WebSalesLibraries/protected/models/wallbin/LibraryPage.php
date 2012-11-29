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

    public function loadFolders()
    {
        foreach($this->folders as $folder)
            $folder->loadFiles();
    }

    public function buildCache($controller)
    {
        $this->buildCacheForBrowser($controller, 'ie');
        $this->buildCacheForBrowser($controller, 'firefox');
        $this->buildCacheForBrowser($controller, 'webkit');
        $this->buildCacheForBrowser($controller, 'opera');
        $this->buildCacheForBrowser($controller, 'mobile');
    }

    private function buildCacheForBrowser($controller, $browser)
    {
        $this->loadData($browser);
        $this->loadFolders();

        $path = Yii::getPathOfAlias('application.views.regular.wallbin') . '/columnsPage.php';
        $content = $controller->renderFile($path, array('libraryPage' => $this), true);

        if (isset($content))
        {
            if ($content != '')
            {
                $pageRecord = LibraryPageStorage::model()->findByPk($this->id);
                if ($pageRecord !== null)
                {
                    switch ($browser)
                    {
                        case 'mobile':
                            $pageRecord->cached_col_view_mobile = $content;
                            break;
                        case 'ie':
                            $pageRecord->cached_col_view_ie = $content;
                            break;
                        case 'webkit':
                            $pageRecord->cached_col_view_webkit = $content;
                            break;
                        case 'firefox':
                            $pageRecord->cached_col_view_firefox = $content;
                            break;
                        case 'opera':
                            $pageRecord->cached_col_view_opera = $content;
                            break;
                    }
                    $pageRecord->save();
                    return true;
                }
            }
        }
        return false;
    }

    public function getCache()
    {
        $pageRecord = LibraryPageStorage::model()->findByPk($this->id);
        if ($pageRecord !== null)
        {
            if (Yii::app()->browser->isMobile())
            {
                $cache = $pageRecord->cached_col_view_mobile;
            }
            else
            {
                $browser = Yii::app()->browser->getBrowser();
                switch ($browser)
                {
                    case 'Internet Explorer':
                        $cache = $pageRecord->cached_col_view_ie;
                        break;
                    case 'Chrome':
                    case 'Safari':
                        $cache = $pageRecord->cached_col_view_webkit;
                        break;
                    case 'Firefox':
                        $cache = $pageRecord->cached_col_view_firefox;
                        break;
                    case 'Opera':
                        $cache = $pageRecord->cached_col_view_opera;
                        break;
                    default:
                        $cache = $pageRecord->cached_col_view_webkit;
                        break;
                }
            }
        }
        if (isset($cache))
            return $cache;
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

?>