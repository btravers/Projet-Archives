<?php

/**
 * Authentificator class
 * Authentificator helper
 */
class Authentificator
{
	/**
	 * Parse request and exec right function
	 */
	public static function exec($request)
	{
		$function = new ReflectionMethod(get_called_class(), $request->function);
		return $function->invokeArgs(NULL, $request->arguments);
	}

	/**
	 * Login
	 */
	public static function login($email, $password)
	{
		$passwordCrypt = sha1($password);

		$result = Database::query("SELECT * FROM User WHERE email = '" . $email . "' AND password = '" . $passwordCrypt . "'");
		if(count($result) > 0) {
			$sessionId = sha1(microtime() . $email);
			Database::exec("UPDATE User SET session_id = '" . $sessionId . "' WHERE id_user = " . $result[0]["id_user"]);
			return array("message" => "connected", "session_id" => $sessionId);
		} else {
			return array("message" => "user_not_found");
		}
	}

	/**
	 * Transform url in php function
	 */
	public static function logout($sessionId)
	{
		$idUser = Database::getUser($sessionId);
		if($idUser != -1) {
			return array("message" => "disconnected");
		} else {
			return array("message" => "user_not_found");
		}
	}

	/**
	 * Exec the parsed request
	 */
	public static function register($email, $password)
	{

	}
}