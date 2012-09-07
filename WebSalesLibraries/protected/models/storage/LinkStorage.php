<?php
class LinkStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{link}}';
    }

    public static function updateData($link)
    {
        $linkRecord = new LinkStorage();
        $linkRecord->id = $link->id;
        $linkRecord->id_folder = $link->folderId;
        $linkRecord->id_library = $link->libraryId;
        $linkRecord->name = $link->name;
        $linkRecord->file_relative_path = $link->fileRelativePath;
        $linkRecord->file_name = $link->fileName;
        $linkRecord->file_extension = $link->fileExtension;
        $linkRecord->note = $link->note;
        $linkRecord->is_bold = $link->isBold;
        $linkRecord->order = $link->order;
        $linkRecord->type = $link->type;
        $linkRecord->enable_widget = $link->enableWidget;
        $linkRecord->widget = $link->widget;

        $linkRecord->id_banner = $link->banner->id;
        BannerStorage::updateData($link->banner);

        if (isset($link->lineBreakProperties))
        {
            $linkRecord->id_line_break = $link->lineBreakProperties->id;
            LineBreakStorage::updateData($link->lineBreakProperties);
        }

        if (isset($link->universalPreview))
            PreviewStorage::updateData($link->universalPreview);

        $linkRecord->save();
    }

    public static function updateContent($linkId, $content)
    {
        $linkRecord = LinkStorage::model()->findByPk($linkId);
        if ($linkRecord !== false)
        {
            $linkRecord->content = $content;
            $linkRecord->save();
        }
    }

    public static function clearData($libraryId)
    {
        LinkStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

    public static function searchByContent($condition)
    {
        return Yii::app()->db->createCommand()
                ->select('id,name,file_name')
                ->from('tbl_link')
                ->where('match(name,file_name,content) against(:condition in boolean mode)', array(':condition' => $condition))
                ->queryAll();
//        $libraryManager = new LibraryManager();
//        foreach (Yii::app()->db->createCommand()
//            ->select('id,name,file_name')
//            ->from('tbl_link')
//            ->where('match(name,file_name,content) against(:condition in boolean mode)', array(':condition' => $condition))
//            ->queryAll() as $linkRow)
//        {
//            $linkRecord = LinkStorage::model()->findByPk($linkRow['id']);
//            if ($linkRecord !== null)
//            {
//                $library = $libraryManager->getLibraryById($linkRecord->id_library);
//                if ($library !== null)
//                {
//                    $link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
//                    if (Yii::app()->browser->isMobile())
//                    {
//                        $link->browser = 'mobile';
//                    }
//                    else
//                    {
//                        $browser = Yii::app()->browser->getBrowser();
//                        switch ($browser)
//                        {
//                            case 'Internet Explorer':
//                                $link->browser = 'ie';
//                                break;
//                            case 'Chrome':
//                            case 'Safari':
//                                $link->browser = 'webkit';
//                                break;
//                            case 'Firefox':
//                                $link->browser = 'firefox';
//                                break;
//                            case 'Opera':
//                                $link->browser = 'opera';
//                                break;
//                            default:
//                                $link->browser = 'ie';
//                                break;
//                        }
//                    }
//                    $link->load($linkRecord);
//                    $links[] = $link;
//                }
//            }
//        }
//        if (isset($links))
//            return $links;
    }

}

?>
