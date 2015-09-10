<?

	/**
	 * Class LocalAppDataController
	 */
	abstract class LocalAppDataController extends SoapController
	{
		/**
		 * @param string $sessionKey
		 * @param string $dataTag
		 * @param string $propertyName
		 * @return string
		 * @soap
		 */
		public function getMetaData($sessionKey, $dataTag, $propertyName)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				return MetaDataRecord::getData($dataTag, $propertyName);
			}
			return null;
		}
	}