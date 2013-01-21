<?php
class CategoryStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{category}}';
    }

    public static function updateData($categories)
    {
        self::clearData();
        $i = 1;
        foreach ($categories as $category)
        {
            $categoryRecord = new CategoryStorage();
            $categoryRecord->id = $i;
            $categoryRecord->category = $category['category'];
            $categoryRecord->tag = $category['tag'];
            $categoryRecord->save();
            $i++;
        }
    }

    public static function clearData()
    {
        self::model()->deleteAll();
    }

    public static function getData()
    {
        return self::model()->findAll();
    }

}
