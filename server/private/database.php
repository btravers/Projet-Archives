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
				Database::$connection = new PDO('mysql:host=localhost;dbname=' . Password::$dbname, Password::$username, Password::$password, array(PDO::MYSQL_ATTR_INIT_COMMAND => 'SET NAMES utf8'));
			} catch(Exception $e) {
				die("database error");
			}
		}
	}

	/**
	 * Execute an update/insert query
	 */
	public static function exec($sql, $arguments) 
	{
		Database::connect();
		
		try
		{			
			$statement = Database::$connection->prepare($sql);
			$statement->execute($arguments);
		} catch(Exception $e) {
			echo $e->getTraceAsString();
		}
	}
	
	/**
	 * Execute a select query
	 */
	public static function query($sql, $arguments)
	{
		Database::connect();
		
		$result = array();
		
		try
		{			
			$statement = Database::$connection->prepare($sql);
			$statement->execute($arguments);
			
			$result = $statement->fetchAll();
		} catch(Exception $e) {
			echo $e->getTraceAsString();
		}
		
		return $result;
	}

	/**
	 * Check if the session id exists in database
	 */
	public static function getUser($sessionId)
	{
		$result = Database::query("SELECT * FROM User WHERE session_id = ?", array($sessionId));
		if(count($result) > 0) {
			return $result[0]["id_user"];
		} else {
			return -1;
		}
	}
	
	/**
	 * Get user name
	 */
	public static function getUserName($userId)
	{
		$result = Database::query("SELECT * FROM User WHERE id_user = ?", array($userId));
		if(count($result) > 0) {
			return $result[0]["email"];
		} else {
			return -1;
		}
	}

	/**
	 * Check if the table exists in database
	 */
	public static function existPageTable($idTable)
	{
		$result = Database::query("SELECT * FROM PageTable WHERE id_page_table = ?", array($idTable));
		return (count($result) > 0);
	}

	/**
	 * Check if the sheet exists in database
	 */
	public static function existSheet($idSheet)
	{
		$result = Database::query("SELECT * FROM Sheet WHERE id_sheet = ?", array($idSheet));
		return (count($result) > 0);
	}

	/**
	 * Check if the type exists in database
	 */
	public static function existType($idType)
	{
		$result = Database::query("SELECT * FROM Type WHERE id_type = ?",array($idType));
		return (count($result) > 0);
	}

	/**
	 * Check if the annotation on the table exists in database
	 */
	public static function existAnnotationTable($idAnnotationTable)
	{
		$result = Database::query("SELECT * FROM AnnotationPageTable WHERE id_annotation_page_table = ?", array($idAnnotationTable));
		return (count($result) > 0);
	}

	/**
	 * Check if the annotation on the sheet exists in database
	 */
	public static function existAnnotationTable2($idAnnotationSheet)
	{
		$result = Database::query("SELECT * FROM AnnotationSheet WHERE id_annotation_sheet = ?", array($idAnnotationSheet));
		return (count($result) > 0);
	}
	
	/**
	 * Check if the shortcut exists in database
	 */
	public static function existShortcut($idShortcut)
	{
		$result = Database::query("SELECT * FROM Shortcut WHERE id_shortcut = ?", array($idShortcut));
		return (count($result) > 0);
	}

	/**
	 * Check if the bookmark folder exists in database
	 */
	public static function existBookmarkFolder($idFolder)
	{
		$result = Database::query("SELECT * FROM BookmarkFolder WHERE id_bookmark_folder = ?", array($idFolder));
		return (count($result) > 0);
	}
	/**
	 * Check if the bookmark file exists in database
	 */
	public static function existBookmarkFile($idFile)
	{
		$result = Database::query("SELECT * FROM BookmarkFile WHERE id_bookmark_file = ?", array($idFile));
		return (count($result) > 0);
	}
}
