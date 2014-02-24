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
		return array("session_id" => "lololol");
	}

	/**
	 * Transform url in php function
	 */
	public static function logout($sessionId)
	{

	}

	/**
	 * Exec the parsed request
	 */
	public static function register($email, $password)
	{

	}
}