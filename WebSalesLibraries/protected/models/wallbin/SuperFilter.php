<?php
class SuperFilter
{
    /**
     * @var string
     * @soap
     */
    public $value;
    public $selected;
    
    public function __construct()
    {
    }

    public function load($superFilterRecord)
    {
        $this->value = $superFilterRecord->value;
    }
}
