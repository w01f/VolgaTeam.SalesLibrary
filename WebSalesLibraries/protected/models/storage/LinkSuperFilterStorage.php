<?php
class LinkSuperFilterStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{link_super_filter}}';
    }

    public static function updateData($superFilter)
    {
        $superFilterRecord = new LinkSuperFilterStorage();
        $superFilterRecord->id_link = $superFilter['linkId'];
        $superFilterRecord->id_library = $superFilter['libraryId'];
        $superFilterRecord->value = $superFilter['value'];
        $superFilterRecord->save();
    }

    public static function clearData($libraryId)
    {
        self::model()->deleteAll('id_library=?', array($libraryId));
    }

}
