<?php

/**
 * Database class
 * SQL manager
 */
class Database
{
	/**
	 * PDO
	 */
	private static $connection = NULL;

	/**
	 * Private function for the database connection
	 */
	private static function connect()
	{
		if(Database::$connection == NULL)
		{
			try
			{
				require_once("password.php");
				Database::$connection = new PDO('mysql:host=localhost;dbname=' . Password::$dbname, Password::$username, Password::$password);
			} catch(Exception $e) {
				die("database error");
			}
		}
	}

	/**
	 * Execute an update/insert query
	 */
	public static function exec($sql) 
	{
		Database::connect();

		Database::$connection->exec($sql);
	}
	
	/**
	 * Execute a select query
	 */
	public static function query($sql)
	{
		Database::connect();

		return Database::$connection->query($sql)->fetchAll();
	}

	/**
	 * Check if the session id exists in database
	 */
	public static function getUser($sessionId)
	{
		$result = Database::query("SELECT * FROM User WHERE session_id = '" . $sessionId . "'");
		if(count($result) > 0) {
			return $result[0]["id_user"];
		} else {
			return -1;
		}
	}
}
