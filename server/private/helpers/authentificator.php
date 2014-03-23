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

		$result = Database::query("SELECT * FROM User WHERE email = ? AND password = ?", array($email, $passwordCrypt));
		if(count($result) > 0) {
			$sessionId = sha1(microtime() . $email . $passwordCrypt);
			Database::exec("UPDATE User SET session_id = ? WHERE id_user = ?", array($sessionId, $result[0]["id_user"]));
			return array("helper" => "authentificator", "message" => "connected", "session_id" => $sessionId);
		} else {
			return array("helper" => "authentificator", "message" => "user_not_found");
		}
	}

	/**
	 * Transform url in php function
	 */
	public static function logout($sessionId)
	{
		$idUser = Database::getUser($sessionId);
		if($idUser != -1) {
			Database::exec("UPDATE User SET session_id = '' WHERE id_user = ?", array($idUser));
			return array("helper" => "authentificator", "message" => "disconnected");
		} else {
			return array("helper" => "authentificator", "message" => "user_not_found");
		}
	}

	/**
	 * Exec the parsed request
	 */
	public static function register($email, $password)
	{
		$result = Database::query("SELECT * FROM User WHERE email = ?", array($email));
		if(count($result) > 0) {
			return array("helper" => "authentificator", "message" => "user_already_exists");
		} else {
			Database::exec("INSERT INTO User VALUES ('', '', ?, ?, 1)", array($email, sha1($password)));
			return array("helper" => "authentificator", "message" => "registered");
		}
	}
}