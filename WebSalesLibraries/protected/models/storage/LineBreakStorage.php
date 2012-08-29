<?php
class LineBreakStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{line_break}}';
    }

    public static function UpdateData($lineBreak)
    {
        $lineBreakRecord = new LineBreakStorage();
        $lineBreakRecord->id = $lineBreak->id;
        $lineBreakRecord->id_library = $lineBreak->libraryId;
        $lineBreakRecord->note = $lineBreak->note;
        $lineBreakRecord->fore_color = $lineBreak->foreColor;
        $lineBreakRecord->font_name = $lineBreak->font->name;
        $lineBreakRecord->font_size = $lineBreak->font->size;
        $lineBreakRecord->font_bold = $lineBreak->font->isBold;
        $lineBreakRecord->font_italic = $lineBreak->font->isItalic;
        $lineBreakRecord->save();
    }

    public static function ClearData($libraryId)
    {
        LineBreakStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

}

?>
