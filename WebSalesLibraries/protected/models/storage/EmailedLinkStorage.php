<?php
class EmailedLinkStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{emailed_link}}';
    }

    public static function saveEmailedLink($id, $linkId, $libraryId, $filePath, $fileLink, $expiresIn, $senderLogin, $senderEmail, $recipients)
    {
        $emailedLink = new EmailedLinkStorage();
        $emailedLink->id = $id;
        $emailedLink->id_link = $linkId;
        $emailedLink->id_library = $libraryId;
        $emailedLink->path = $filePath;
        $emailedLink->link = $fileLink;
        $emailedLink->initial_date = date(Yii::app()->params['mysqlDateFormat'], strtotime(date('y:m:d')));
        $emailedLink->expires_in = $expiresIn;
        $emailedLink->sender_login = $senderLogin;
        $emailedLink->sender_email = $senderEmail;
        $emailedLink->recipients = $recipients;
        $emailedLink->save();
    }
    
    public static function getEmailedLink($id)
    {
        return self::model()->findByPk($id);
    }
}
