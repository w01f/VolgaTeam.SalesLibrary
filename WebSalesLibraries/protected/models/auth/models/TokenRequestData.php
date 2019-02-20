<?

	namespace application\models\auth\models;

	class TokenRequestData
	{
		public $configured;

		public $login;
		public $password;
		public $firstName;
		public $lastName;
		public $email;
		public $phone;
		public $group;

		public $token;

		public function __construct()
		{
			$this->password = 'gtvninja!@#';
			$this->phone = '222-222-2222';
		}

		/**
		 * @param $request \CHttpRequest
		 * @return TokenRequestData
		 */
		public static function tryExtractTokenData($request)
		{
			$instance = new self();

			$instance->login = $request->getQuery('user');
			$instance->email = $instance->login;
			$instance->firstName = $request->getQuery('first');
			$instance->lastName = $request->getQuery('last');
			$instance->group = $request->getQuery('station');
			$instance->token = $request->getQuery('token');

			$instance->configured = !empty($instance->login)
				&& !empty($instance->password)
				&& !empty($instance->firstName)
				&& !empty($instance->email)
				&& !empty($instance->phone)
				&& !empty($instance->group)
				&& !empty($instance->token);

			return $instance;
		}
	}