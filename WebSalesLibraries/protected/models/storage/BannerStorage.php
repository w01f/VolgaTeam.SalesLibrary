<?php
class BannerStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{banner}}';
    }

    public static function updateData($banner)
    {
        $bannerRecord = new BannerStorage();
        $bannerRecord->id = $banner->id;
        $bannerRecord->id_library = $banner->libraryId;
        $bannerRecord->enabled = $banner->isEnabled;
        $bannerRecord->image = $banner->image;
        $bannerRecord->show_text = $banner->showText;
        $bannerRecord->image_alignment = $banner->imageAlignment;
        $bannerRecord->text = $banner->text;
        $bannerRecord->fore_color = $banner->foreColor;
        $bannerRecord->font_name = $banner->font->name;
        $bannerRecord->font_size = $banner->font->size;
        $bannerRecord->font_bold = $banner->font->isBold;
        $bannerRecord->font_italic = $banner->font->isItalic;
        $bannerRecord->save();
    }

    public static function clearData($libraryId)
    {
        BannerStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

}

?>
