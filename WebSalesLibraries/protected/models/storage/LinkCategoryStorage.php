<?php
class LinkCategoryStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{link_category}}';
    }

    public static function updateData($category)
    {
        $categoryRecord = new LinkCategoryStorage();
        $categoryRecord->id_link = $category['linkId'];
        $categoryRecord->id_library = $category['libraryId'];
        $categoryRecord->category = $category['category'];
        $categoryRecord->tag = $category['tag'];
        $categoryRecord->save();
    }

    public static function clearData($libraryId)
    {
        self::model()->deleteAll('id_library=?', array($libraryId));
    }

}
