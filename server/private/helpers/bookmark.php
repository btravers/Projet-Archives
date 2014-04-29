<?php

/**
 * Bookmark class
 * Bookmark helper
 */
class Bookmark
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
	 * Creates a new bookmark folder
	 */
	public static function create_bookmark_folder($id_session, $id_parent_folder, $name)
	{
		$id_user = Database::getUser($id_session);
		if($id_user == -1) {
			return array("helper" => "bookmark", "message" => "user_not_found");
		} else if(!Database::existBookmarkFolder($id_parent_folder)) {
			return array("helper" => "bookmark", "message" => "parent_folder_not_found");
		} else {
			Database::exec("INSERT INTO BookmarkFolder VALUES ('', ?, ?, ?)", array($id_user, $id_parent_folder, $name));

			$query = "SELECT id_bookmark_folder FROM BookmarkFolder WHERE id_user = ? AND id_bookmark_folder_parent = ? AND label = ?";
			$result = Database::query($query, array($id_user, $id_parent_folder, $name));
			if (count($result) == 0) {
				return array("helper" => "bookmark", "message" => "creation_error");
			} else {
				return array("helper" => "bookmark", "message" => "created", "result" => $result[0][0]);
			}
		}
	}

	/**
	 * Creates a new bookmark file
	 */
	public static function create_bookmark_file($id_session, $id_parent_folder, $id_sheet, $name)
	{
		$id_user = Database::getUser($id_session);
		if($id_user == -1) {
			return array("helper" => "bookmark", "message" => "user_not_found");
		} else if(!Database::existBookmarkFolder($id_parent_folder)) {
			return array("helper" => "bookmark", "message" => "parent_folder_not_found");
		} else if(!Database::existSheet($id_sheet)) {
			return array("helper" => "bookmark", "message" => "sheet_not_found");
		} else {
			Database::exec("INSERT INTO BookmarkFile VALUES ('', ?, ?, ?, ?)", array($id_user, $id_sheet, $id_parent_folder, $name));
			
			$query = "SELECT id_bookmark_file FROM BookmarkFile WHERE id_user = ? AND id_sheet = ? AND id_bookmark_folder = ? AND label = ?";
			$result = Database::query($query, array($id_user, $id_sheet, $id_parent_folder, $name));
			if (count($result) == 0) {
				return array("helper" => "bookmark", "message" => "creation_error");
			} else {
				return array("helper" => "bookmark", "message" => "created", "result" => $result[0][0]);
			}
		}
	}

	/**
	 * Returns the tree of bookmark folders and files
	 */
	public static function get_tree($id_session)
	{
		throw new Exception('Not implemented');
	}

	/**
	 * Removes a bookmark file
	 */
	public static function remove_bookmark_file($id_file)
	{
		if (!Database::existBookmarkFile($id_file)) {
			return array("helper" => "bookmark", "message" => /* 404 */ "file_not_found");
		} else {
			Database::exec("DELETE FROM BookmarkFile WHERE id_bookmark_file = ?", array($id_file));
			return array("helper" => "bookmark", "message" => "deleted");
		}
	}

	/**
	 * Removes a bookmark folder
	 */
	public static function remove_bookmark_folder($id_folder)
	{
		if (!Database::existBookmarkFolder($id_folder)) {
			return array("helper" => "bookmark", "message" => "folder_not_found");
		} else {
			Database::exec("DELETE FROM BookmarkFolder WHERE id_bookmark_folder = ?", array($id_folder));
			return array("helper" => "bookmark", "message" => "deleted");
		}
	}

	/**
	 * Updates the parent folder of a bookmark file
	 */
	public static function update_parent_bookmark_file($id_file, $new_id_folder)
	{
		if (!Database::existBookmarkFile($id_file)) {
			return array("helper" => "bookmark", "message" => "file_not_found");
		} else if (!Database::existBookmarkFolder($new_id_folder)) {
			return array("helper" => "bookmark", "message" => "folder_not_found");
		} else {
			Database::exec("UPDATE BookmarkFile SET id_bookmark_folder = ? WHERE id_bookmark_file = ?", array($new_id_folder, $id_file));
			return array("helper" => "bookmark", "message" => "updated");
		}
	}

	/**
	 * Updates the parent folder of a bookmark folder
	 */
	public static function update_parent_bookmark_folder($id_folder, $new_id_parent)
	{
		if (!Database::existBookmarkFolder($id_folder)) {
			return array("helper" => "bookmark", "message" => "folder_not_found");
		} else if (!Database::existBookmarkFolder($new_id_parent)) {
			return array("helper" => "bookmark", "message" => "folder_not_found");
		} else {
			Database::exec("UPDATE BookmarkFolder SET id_bookmark_folder_parent = ? WHERE id_bookmark_folder = ?", array($new_id_parent, $id_folder));
			return array("helper" => "bookmark", "message" => "updated");
		}
	}

	/**
	 * Renames a bookmark file
	 */
	public static function rename_bookmark_file($id_file, $name)
	{
		if (!Database::existBookmarkFile($id_file)) {
			return array("helper" => "bookmark", "message" => "file_not_found");
		} else {
			Database::exec("UPDATE BookmarkFile SET label = ? WHERE id_bookmark_file = ?", array($name, $id_file));
			return array("helper" => "bookmark", "message" => "updated");
		}
	}

	/**
	 * Renames a bookmark folder
	 */
	public static function rename_bookmark_folder($id_folder, $name)
	{
		if (!Database::existBookmarkFolder($id_folder)) {
			return array("helper" => "bookmark", "message" => "folder_not_found");
		} else {
			Database::exec("UPDATE BookmarkFolder SET label = ? WHERE id_bookmark_folder = ?", array($name, $id_folder));
			return array("helper" => "bookmark", "message" => "updated");
		}
	}

}