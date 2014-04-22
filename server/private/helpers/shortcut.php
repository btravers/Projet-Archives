<?php

/**
 * Shortcut class
 * Shortcut helper
 */
class Shortcut
{
	/**
	 * Parse request and exec right function
	 */
	public static function exec($request)
	{
		$function = new ReflectionMethod(get_called_class(), $request->function);
		return $function->invokeArgs(NULL, $request->arguments);
	}

	public static function add_shortcut($id_session, $id_type, $text, $id_icon) {
	    $idUser = Database::getUser($id_session);
		if($idUser == -1) {
			return array("helper" => "shortcut", "message" => "user_not_found");
		} else {			
			Database::exec("INSERT INTO Shortcut VALUES (0, ?, ?, ?, ?)", array($idUser, $id_type, $text, $id_icon));
			$resReq = Database::exec("SELECT * FROM Shortcut WHERE id_user = ? AND id_type = ? AND text = ? AND id_icon = ?", array($idUser, $id_type, $text, $id_icon));
			foreach ($resReq as $id)
			{
				$result = $id;
			}	
			return array("helper" => "shortcut", "message" => "registered", "result" => $result);
		}
	}		
	
	public static function delete_shortcut($id_shortcut) {
		if (!Database::existShortcut($id_shortcut)) {
			return array("helper" => "shortcut", "message" => "shortcut_not_found");
		} else {
			Database::exec("DELETE FROM Shortcut WHERE id_shortcut = ?", array($id_shortcut));
			return array("helper" => "shortcut", "message" => "deleted");
		}
	}	
	
	public static function get_all_shortcut($id_session)
	{
		$id_user = Database::getUser($id_session);
		if($id_user == -1) {
			return array("helper" => "shortcut", "message" => "user_not_found");
		} else {
			$result = Database::query("SELECT * FROM Shortcut WHERE id_user = ?", array($id_user));
			if(count($result) > 0) {
				return array("helper" => "shortcut", "message" => "result_found", "result" => $result);
			} else {
				return array("helper" => "shortcut", "message" => "result_not_found");
			}
		}
	}
}