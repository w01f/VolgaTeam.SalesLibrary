<?php
class DataController extends CController
{
    public function actions()
    {
        return array(
            'quote' => array(
                'class' => 'CWebServiceAction',
                'classMap' => array(
                    'Font' => 'Font',
                    'Library' => 'Library',
                    'LibraryPage' => 'LibraryPage',
                    'AutoWidget' => 'AutoWidget',
                    'Banner' => 'Banner',
                    'Column' => 'Column',
                    'LibraryFolder' => 'LibraryFolder',
                    'LibraryLink' => 'LibraryLink',
                    'LineBreak' => 'LineBreak',
                    'UniversalPreviewContainer' => 'UniversalPreviewContainer',
                ),
            ),
        );
    }

    protected function authenticateBySession($sessionKey)
    {
        $data = Yii::app()->cacheDB->get($sessionKey);
        if ($data !== FALSE)
            return TRUE;
        else
            return FALSE;
    }

    /**
     * @param string $login
     * @param string $password
     * @return string session key
     * @soap
     */
    public function getSessionKey($login, $password)
    {
        $identity = new UserIdentity($login, $password);
        $identity->authenticate();
        if ($identity->errorCode === UserIdentity::ERROR_NONE)
        {
            $sessionKey = strval(md5(mt_rand()));
            Yii::app()->cacheDB->set($sessionKey, $login, (60 * 60 * 24 * 7));
            return $sessionKey;
        }
        else
            return '';
    }

    /**
     * @param string Session Key
     * @param Library Library
     * @soap
     */
    public function setLibrary($sessionKey, $library)
    {
        if ($this->authenticateBySession($sessionKey))
        {
            LibraryStorage::ClearData($library->id);
            LibraryStorage::UpdateData($library);
        }
    }

    /**
     * @param string Session Key
     * @param string link id
     * @param string content
     * @soap
     */
    public function setContent($sessionKey, $linkId, $content)
    {
        if ($this->authenticateBySession($sessionKey))
            LinkStorage::UpdateContent($linkId, $content);
    }

}

?>