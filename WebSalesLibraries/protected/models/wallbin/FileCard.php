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
        $this->advertiser = $fileCardRecord->advertiser;
        $this->dateSold = $fileCardRecord->date_sold;
        $this->broadcastClosed = $fileCardRecord->broadcast_closed;
        $this->digitalClosed = $fileCardRecord->digital_closed;
        $this->publishingClosed = $fileCardRecord->publishing_closed;
        $this->salesName = $fileCardRecord->sales_name;
        $this->salesEmail = $fileCardRecord->sales_email;
        $this->salesPhone = $fileCardRecord->sales_phone;
        $this->salesStation = $fileCardRecord->sales_station;
        $this->notes = CJSON::decode($fileCardRecord->notes);
    }
}
?>
