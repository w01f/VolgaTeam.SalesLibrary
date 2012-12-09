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

    public static function updateData($link, $libraryRootPath)
    {
        $needToUpdate = false;
        $needToCreate = false;
        $linkRecord = LinkStorage::model()->findByPk($link['id']);
        $linkDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($link['dateModify']));
        if ($linkRecord !== null)
        {
            if ($linkRecord->date_modify != null)
                if ($linkRecord->date_modify != $linkDate)
                    $needToUpdate = true;
        }
        else
        {
            $linkRecord = new LinkStorage();
            $needToCreate = true;
        }
        if ($needToCreate || $needToUpdate)
        {
            $linkRecord->id = $link['id'];
            $linkRecord->id_folder = $link['folderId'];
            $linkRecord->id_library = $link['libraryId'];
            $linkRecord->name = $link['name'];
            $linkRecord->file_relative_path = $link['fileRelativePath'];
            $linkRecord->file_name = $link['fileName'];
            $linkRecord->file_extension = $link['fileExtension'];
            $linkRecord->file_date = date(Yii::app()->params['mysqlDateFormat'], strtotime($link['fileDate']));
            $linkRecord->file_size = $link['fileSize'];
            $linkRecord->note = $link['note'];
            $linkRecord->format = $link['originalFormat'];
            $linkRecord->is_bold = $link['isBold'];
            $linkRecord->order = $link['order'];
            $linkRecord->type = $link['type'];
            $linkRecord->enable_widget = $link['enableWidget'];
            $linkRecord->widget = $link['widget'];
            $linkRecord->tags = $link['tags'];
            $linkRecord->date_add = date(Yii::app()->params['mysqlDateFormat'], strtotime($link['dateAdd']));
            $linkRecord->date_modify = $linkDate;
            $linkRecord->id_preview = $link['previewId'];
            $linkRecord->enable_file_card = $link['enableFileCard'];
            $linkRecord->enable_attachments = $link['enableAttachments'];

            $contentPath = str_replace('\\', '/', $libraryRootPath . DIRECTORY_SEPARATOR . $link['contentPath']);
            if (file_exists($contentPath) && is_file($contentPath))
                $linkRecord->content = file_get_contents($contentPath);

            echo 'Link ' . ($needToCreate ? 'created' : 'updated') . ': ' . $link['name'] . ' (' . $link['fileName'] . ')' . "\n";
        }

        if (array_key_exists('banner', $link))
            if (isset($link['banner']))
            {
                $linkRecord->id_banner = $link['banner']['id'];
                BannerStorage::updateData($link['banner']);
            }

        if (array_key_exists('lineBreakProperties', $link))
            if (isset($link['lineBreakProperties']))
            {
                $linkRecord->id_line_break = $link['lineBreakProperties']['id'];
                LineBreakStorage::updateData($link['lineBreakProperties']);
            }

        if (array_key_exists('fileCard', $link) && $link['enableFileCard'])
            if (isset($link['fileCard']))
            {
                $linkRecord->id_file_card = $link['fileCard']['id'];
                FileCardStorage::updateData($link['fileCard']);
            }

        if (array_key_exists('attachments', $link))
            if (isset($link['filesAttachments']))
            {
                FileCardStorage::updateData($link['fileCard']);
            }

        if (array_key_exists('attachments', $link))
            if (isset($link['attachments']))
                foreach ($link['attachments'] as $attachment)
                    AttachmentStorage::updateData($attachment);

        if (array_key_exists('categories', $link))
            if (isset($link['categories']))
                foreach ($link['categories'] as $category)
                    LinkCategoryStorage::updateData($category);

        $linkRecord->save();
    }

    public static function clearByLibrary($libraryId)
    {
        LinkStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

    public static function clearByIds($folderId, $linkIds)
    {
        Yii::app()->db->createCommand()->delete('tbl_link', "id_folder = '" . $folderId . "' and id not in ('" . implode("','", $linkIds) . "')");
    }

    public static function searchByContent($contentCondition, $fileTypes, $startDate, $endDate, $dateFile, $checkedLibraryIds, $onlyFileCards, $categories, $categoriesExactMatch, $isSort)
    {
        $sessionId = 'searchedLinks';
        if (isset(Yii::app()->request->cookies['selectedRibbonTabId']->value))
            $sessionId .= Yii::app()->request->cookies['selectedRibbonTabId']->value;
        if ($isSort == 1)
        {
            $links = Yii::app()->session[$sessionId];
        }
        else
        {
            $libraryCondition = '1 != 1';
            if (isset($checkedLibraryIds))
            {
                $count = count($checkedLibraryIds);
                switch ($count)
                {
                    case 0:
                        $libraryCondition = '1 != 1';
                        break;
                    default:
                        $libraryCondition = "id_library in ('" . implode("','", $checkedLibraryIds) . "')";
                        break;
                }
            }

            $fileTypeCondition = '1 = 1';
            if (isset($fileTypes))
            {
                $count = count($fileTypes);
                switch ($count)
                {
                    case 0:
                        $fileTypeCondition = '1 = 1';
                        break;
                    default:
                        $fileTypeCondition = "format in ('" . implode("','", $fileTypes) . "')";
                        break;
                }
            }

            $dateCondition = '1 = 1';
            $additionalDateCondition = '';
            if (isset($startDate) && isset($endDate))
                if ($startDate != '' && $endDate != '')
                {
                    $dateColumn = 'date_modify';
                    if (isset($dateFile) && $dateFile == 'true')
                        $dateColumn = 'file_date';
                    $dateCondition = $dateColumn . " >= '" . date(Yii::app()->params['mysqlDateFormat'], strtotime($startDate)) . "' and " . $dateColumn . " <= '" . date(Yii::app()->params['mysqlDateFormat'], strtotime($endDate) + 86400) . "'";
                    if ($contentCondition == '""' || $contentCondition == '')
                        $additionalDateCondition = " or (" . $dateCondition . ")";
                }

            $fileCardsCondition = '1 = 1';
            $additionalFileCardsCondition = '';
            if (isset($onlyFileCards))
            {
                if ($onlyFileCards == 1)
                {
                    $fileCardsCondition = 'enable_file_card = true or enable_attachments = true ';
                    if ($contentCondition == '""' || $contentCondition == '')
                        $additionalFileCardsCondition = ' or (enable_file_card = true or enable_attachments = true)';
                }
            }

            $categoryCondition = '1 = 1';
            $additionalCategoryCondition = '';
            if (isset($categories))
            {
                foreach ($categories as $category)
                    $categoriesSelector[] = '(id in (select id_link from tbl_link_category where category = "' . $category['category'] . '" and tag = "' . $category['tag'] . '"))';
                if (isset($categoriesSelector))
                {
                    $categoryCondition = '(' . implode(($categoriesExactMatch == 'true' ? ' and ' : ' or '), $categoriesSelector) . ')';
                    if ($contentCondition == '""' || $contentCondition == '')
                        $additionalCategoryCondition = ' or ' . $categoryCondition;
                }
            }

            $folderCondition = '1 != 1';
            if (isset(Yii::app()->user))
            {
                $userId = Yii::app()->user->getId();
                if (isset($userId))
                    $assignedPageIds = UserLibraryStorage::getPageIdsByUser($userId);
            }
            if (isset($assignedPageIds))
                $folderCondition = "id_folder in (select id from tbl_folder where id_page in ('" . implode("', '", $assignedPageIds) . "'))";

            $linkRecords = Yii::app()->db->createCommand()
                ->select('*')
                ->from('tbl_link')
                ->where("(match(name,file_name,tags,content) against('" . $contentCondition . "' in boolean mode)" . $additionalFileCardsCondition . $additionalDateCondition . $additionalCategoryCondition . ") and (" . $libraryCondition . ") and (" . $fileTypeCondition . ") and (" . $fileCardsCondition . ") and (" . $dateCondition . ") and (" . $categoryCondition . ") and (" . $folderCondition . ")")
                ->queryAll();
            if (isset($linkRecords))
            {
                if (count($linkRecords) > 0)
                {
                    $libraryManager = new LibraryManager();
                    foreach ($linkRecords as $linkRecord)
                    {
                        $link['id'] = $linkRecord['id'];
                        $link['name'] = $linkRecord['name'];
                        $link['file_name'] = $linkRecord['file_name'];
                        $link['date_modify'] = isset($dateFile) && $dateFile == 'true' ? $linkRecord['file_date'] : $linkRecord['date_modify'];
                        $link['hasDetails'] = $linkRecord['enable_attachments'] | $linkRecord['enable_file_card'];

                        $library = $libraryManager->getLibraryById($linkRecord['id_library']);
                        if (isset($library))
                            $link['library'] = $library->name;
                        else
                            $link['library'] = '';

                        switch ($linkRecord['format'])
                        {
                            case 'ppt':
                                $link['file_type'] = 'images/search/search-powerpoint.png';
                                break;
                            case 'doc':
                                $link['file_type'] = 'images/search/search-word.png';
                                break;
                            case 'xls':
                                $link['file_type'] = 'images/search/search-excel.png';
                                break;
                            case 'pdf':
                                $link['file_type'] = 'images/search/search-pdf.png';
                                break;
                            case 'video':
                                $link['file_type'] = 'images/search/search-video.png';
                                break;
                            default:
                                $link['file_type'] = 'undefined';
                                break;
                        }
                        $links[] = $link;
                    }
                }
            }
            if (isset($links))
                Yii::app()->session[$sessionId] = $links;
            else
                Yii::app()->session[$sessionId] = null;
        }
        if (isset($links))
            if (count($links) > 0)
            {
                usort($links, 'LinkStorage::sortLinks');
                return $links;
            }
    }

    public static function getLinkById($linkId)
    {
        $linkRecord = self::model()->findByPk($linkId);
        if ($linkRecord !== false)
            return $linkRecord;
    }

    private static function sortLinks($a, $b)
    {
        if (isset(Yii::app()->request->cookies['sortColumn']->value))
        {
            switch (Yii::app()->request->cookies['sortColumn']->value)
            {
                case 'library':
                    $sortColumn = 'library';
                    break;
                case 'link-type':
                    $sortColumn = 'file_type';
                    break;
                case 'link-name':
                    $sortColumn = 'name';
                    break;
                case 'link-date':
                    $sortColumn = 'date_modify';
                    break;
            }
        }
        else
            $sortColumn = 'name';

        if (isset(Yii::app()->request->cookies['sortDirection']->value))
            $sortDirection = Yii::app()->request->cookies['sortDirection']->value;
        else
            $sortDirection = 'asc';

        if (isset($sortColumn) && isset($sortDirection))
        {
            if ($sortDirection == 'asc')
                return strnatcmp($a[$sortColumn], $b[$sortColumn]);
            else
                return strnatcmp($b[$sortColumn], $a[$sortColumn]);
        }
        else
            return 0;
    }

}

?>
