<?php
class FileCard
{
    /**
     * @var string
     * @soap
     */
    public $id;
    /**
     * @var string
     * @soap
     */
    public $libraryId;
    /**
     * @var string
     * @soap
     */
    public $title;    
    /**
     * @var string
     * @soap
     */
    public $advertiser;
    /**
     * @var string
     * @soap
     */
    public $dateSold;
    /**
     * @var double
     * @soap
     */
    public $broadcastClosed;
    /**
     * @var double
     * @soap
     */
    public $digitalClosed;
    /**
     * @var double
     * @soap
     */
    public $publishingClosed;
    /**
     * @var string
     * @soap
     */
    public $salesName;
    /**
     * @var string
     * @soap
     */
    public $salesEmail;
    /**
     * @var string
     * @soap
     */
    public $salesPhone;
    /**
     * @var string
     * @soap
     */
    public $salesStation;
    /**
     * @var string[]
     * @soap
     */
    public $notes;
    public function load($fileCardRecord)
    {
        $this->id = $fileCardRecord->id;
        $this->libraryId = $fileCardRecord->id_library;
        $this->title = $fileCardRecord->title;
        if ($fileCardRecord->advertiser != null)
            $this->advertiser = $fileCardRecord->advertiser;
        if ($fileCardRecord->date_sold != null)
            $this->dateSold = $fileCardRecord->date_sold;
        if ($fileCardRecord->broadcast_closed != null)
            $this->broadcastClosed = $fileCardRecord->broadcast_closed;
        if ($fileCardRecord->digital_closed != null)
            $this->digitalClosed = $fileCardRecord->digital_closed;
        if ($fileCardRecord->publishing_closed != null)
            $this->publishingClosed = $fileCardRecord->publishing_closed;
        if ($fileCardRecord->sales_name != null)
            $this->salesName = $fileCardRecord->sales_name;
        if ($fileCardRecord->sales_email != null)
            $this->salesEmail = $fileCardRecord->sales_email;
        if ($fileCardRecord->sales_phone != null)
            $this->salesPhone = $fileCardRecord->sales_phone;
        if ($fileCardRecord->sales_station != null)
            $this->salesStation = $fileCardRecord->sales_station;
        if ($fileCardRecord->notes != null)
            $this->notes = CJSON::decode($fileCardRecord->notes);
    }

}

?>
