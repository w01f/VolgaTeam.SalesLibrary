<?

	/**
	 * Class RestResponse
	 */
	class RestResponse
	{
		const statusOK = 1;
		const statusError = 2;

		public $resultCode;
		public $dataEncoded;

		/**
		 * @param $errorMessage string;
		 * @return RestResponse
		 */
		public static function error($errorMessage)
		{
			$response = new RestResponse();
			$error = new RestError();
			$error->message = $errorMessage;
			$response->resultCode = self::statusError;
			$response->dataEncoded = CJSON::encode($error);
			return $response;
		}

		/**
		 * @param $data mixed;
		 * @return RestResponse
		 */
		public static function success($data)
		{
			$response = new RestResponse();
			$response->resultCode = self::statusOK;
			$response->dataEncoded = CJSON::encode($data);
			return $response;
		}
	}
