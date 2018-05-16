<?

	class OneDriveRequestHelper
	{
		/**
		 * @param $parameters array
		 * @return array
		 */
		public static function sendAuthRequest($parameters)
		{
			$curl = curl_init();

			curl_setopt_array(
				$curl,
				array(
					CURLOPT_RETURNTRANSFER => true,
					CURLOPT_URL => 'https://login.microsoftonline.com/common/oauth2/v2.0/token',
					CURLOPT_POST => true,
					CURLOPT_HTTPHEADER => array(
						'Accept: */*',
						'Content-Type: application/x-www-form-urlencoded'
					),
					CURLOPT_POSTFIELDS => http_build_query($parameters),
					CURLOPT_FAILONERROR => false
				)
			);

			$response = curl_exec($curl);

			if (curl_errno($curl))
				throw new \RuntimeException(curl_error($curl));

			curl_close($curl);

			return CJSON::decode($response, true);
		}
	}