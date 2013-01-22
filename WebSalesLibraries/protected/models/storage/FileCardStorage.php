<?php
class FileCardStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{file_card}}';
    }

    public static function updateData($fileCard)
    {
        $existedItem = self::model()->findByPk($fileCard['id']);
        if(isset($existedItem))
            return;
        $fileCardRecord = new FileCardStorage();
        $fileCardRecord->id = $fileCard['id'];
        $fileCardRecord->id_library = $fileCard['libraryId'];
        $fileCardRecord->title = $fileCard['title'];
        $fileCardRecord->advertiser = $fileCard['advertiser'];
        $fileCardRecord->date_sold = $fileCard['dateSold'] != null ? date(Yii::app()->params['mysqlDateFormat'], strtotime($fileCard['dateSold'])) : null;
        $fileCardRecord->broadcast_closed = $fileCard['broadcastClosed'] > 0 ? $fileCard['broadcastClosed'] : null;
        $fileCardRecord->digital_closed = $fileCard['digitalClosed'] > 0 ? $fileCard['digitalClosed'] : null;
        $fileCardRecord->publishing_closed = $fileCard['publishingClosed'] > 0 ? $fileCard['publishingClosed'] : null;
        $fileCardRecord->sales_name = $fileCard['salesName'];
        $fileCardRecord->sales_email = $fileCard['salesEmail'];
        $fileCardRecord->sales_phone = $fileCard['salesPhone'];
        $fileCardRecord->sales_station = $fileCard['salesStation'];

        if (array_key_exists('notes', $fileCard))
            if (isset($fileCard['notes']))
                $fileCardRecord->notes = CJSON::encode($fileCard['notes']);

        $fileCardRecord->save();
    }

    public static function clearData($libraryId)
    {
        self::model()->deleteAll('id_library=?', array($libraryId));
    }

}
