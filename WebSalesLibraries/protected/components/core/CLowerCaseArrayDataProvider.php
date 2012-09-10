<?php
class CLowerCaseArrayDataProvider extends CArrayDataProvider
{
    protected function getSortingFieldValue($data, $fields)
    {
        foreach ($fields as $field)
        {
            $data = is_object($data) ? strtolower($data->$field) : strtolower($data[$field]);
        }
        return $data;
    }
}

?>
