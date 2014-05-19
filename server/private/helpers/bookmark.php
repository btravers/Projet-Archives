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

	const ROOT = -1;

	/**
	 * Creates a new bookmark folder
	 */
	public static function create_bookmark_folder($id_session, $id_parent_folder, $name)
	{
		$id_user = Database::getUser($id_session);
		if($id_user == -1) {
			return array("helper" => "bookmark", "message" => "user_not_found");
		} else {
			if($id_parent_folder == -1 || !Database::existBookmarkFolder($id_parent_folder))
				$id_parent_folder = -1;

			Database::exec("INSERT INTO BookmarkFolder VALUES ('', ?, ?, ?)", array($id_user, $id_parent_folder, $name));

			$query = "SELECT id_bookmark_folder FROM BookmarkFolder WHERE id_user = ? AND id_bookmark_folder_parent = ? AND label = ?";
			$result = Database::query($query, array($id_user, $id_parent_folder, $name));
			if (count($result) == 0) {
				return array("helper" => "bookmark", "message" => "creation_error");
			} else {
				return array("helper" => "bookmark", "message" => "created", "result_id" => $result[0][0]);
			}
		}
	}

	/**
	 * Creates the root folder for an user
	 */
	public static function create_root_folder($id_session)
	{
		$id_user = Database::getUser($id_session);
		if($id_user == -1) {
			return array("helper" => "bookmark", "message" => "user_not_found");
		} else {
			$result = Database::query("SELECT id_bookmark_folder FROM BookmarkFolder WHERE id_user = ? AND id_bookmark_folder_parent = ?", array($id_user, static::ROOT));
			if (count($result) > 0) {
				return array("helper" => "bookmark", "message" => "root_already_exists");
			} else {
				Database::exec("INSERT INTO BookmarkFolder VALUES ('', ?, ?, 'root')", array($id_user, static::ROOT));

				$result = Database::query("SELECT id_bookmark_folder FROM BookmarkFolder WHERE id_user = ? AND id_bookmark_folder_parent = ?", array($id_user, static::ROOT));
				if (count($result) == 0) {
					return array("helper" => "bookmark", "message" => "creation_error");
				} else {
					return array("helper" => "bookmark", "message" => "created", "result_id" => $result[0][0]);
				}
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
		} else if(!Database::existSheet($id_sheet)) {
			return array("helper" => "bookmark", "message" => "sheet_not_found");
		} else {
			if($id_parent_folder == -1 || !Database::existBookmarkFolder($id_parent_folder))
				$id_parent_folder = -1;

			Database::exec("INSERT INTO BookmarkFile VALUES ('', ?, ?, ?, ?)", array($id_user, $id_sheet, $id_parent_folder, $name));
			
			$query = "SELECT id_bookmark_file FROM BookmarkFile WHERE id_user = ? AND id_sheet = ? AND id_bookmark_folder = ? AND label = ?";
			$result = Database::query($query, array($id_user, $id_sheet, $id_parent_folder, $name));
			if (count($result) == 0) {
				return array("helper" => "bookmark", "message" => "creation_error");
			} else {
				return array("helper" => "bookmark", "message" => "created", "result_id" => $result[0][0]);
			}
		}
	}

	/**
	 * Returns the list of bookmark folders and files
	 */
	public static function get_list($id_session)
	{
		$id_user = Database::getUser($id_session);
		if($id_user == -1) {
			return array("helper" => "bookmark", "message" => "user_not_found");
		} else {
			$folders = Bookmark::get_folders($id_session);
			$files = Bookmark::get_files($id_session);
			return array("helper" => "bookmark", "message" => "ok", "folders" => $folders, "files" => $files);
		}
	}

	/**
	 * Returns the list of bookmark folders
	 */
	private static function get_folders($id_session)
	{
		$id_user = Database::getUser($id_session);
		if($id_user == -1) {
			return array("helper" => "bookmark", "message" => "user_not_found");
		} else {
			$folders = Database::query("SELECT * FROM BookmarkFolder WHERE id_user = ?", array($id_user));
			$res = array();

			foreach ($folders as $folder) {
				$arr = array("id_bookmark_folder" => $folder[0],
						 "id_user" => $folder[1],
						 "id_bookmark_folder_parent" => $folder[2],
						 "label" => $folder[3]);
				$key = "folder" . $folder[0];
				$res[$key] = $arr;
			}

			return $res;
		}
	}

	/**
	 * Returns the list of bookmark files
	 */
	private static function get_files($id_session)
	{
		$id_user = Database::getUser($id_session);
		if($id_user == -1) {
			return array("helper" => "bookmark", "message" => "user_not_found");
		} else {
			$files = Database::query("SELECT * FROM BookmarkFile WHERE id_user = ?", array($id_user));
			$res = array();

			foreach ($files as $file) {
				$arr = array("id_bookmark_file" => $file[0],
						 "id_user" => $file[1],
						 "id_sheet" => $file[2],
						 "id_bookmark_folder" => $file[3],
						 "label" => $file[4]);
				$key = "file" . $file[0];
				$res[$key] = $arr;
			}

			return $res;
		}
	}

	/**
	 * Returns the tree of bookmark folders and files
	 */
	public static function get_tree($id_session)
	{
		$id_user = Database::getUser($id_session);
		if($id_user == -1) {
			return array("helper" => "bookmark", "message" => "user_not_found");
		} else {
			$root = Database::query("SELECT id_bookmark_folder FROM BookmarkFolder WHERE id_user = ? AND id_bookmark_folder_parent = ?", array($id_user, static::ROOT));
			if (count($root) <= 0) {
				return array("helper" => "bookmark", "message" => "root_not_found");
			} else {
				$tree = Bookmark::get_subtree($root[0][0]);
				return array("helper" => "bookmark", "message" => "ok", "tree" => $tree);
			}
		}
	}

	/**
	 * Returns the subtree of bookmark folders and files
	 */
	private static function get_subtree($id_folder)
	{
		$folder = Database::query("SELECT * FROM BookmarkFolder WHERE id_bookmark_folder = ?", array($id_folder));
		$sub_folders = Database::query("SELECT * FROM BookmarkFolder WHERE id_bookmark_folder_parent = ?", array($id_folder));
		$sub_files = Database::query("SELECT * FROM BookmarkFile WHERE id_bookmark_folder = ?", array($id_folder));
		
		$res = array("id_bookmark_folder" => $folder[0][0],
					 "id_user" => $folder[0][1],
					 "id_bookmark_folder_parent" => $folder[0][2],
					 "label" => $folder[0][3]);

		$file_count = 0;
		$folder_count = 0;
		
		foreach ($sub_files as $file) {
			$arr = array("id_bookmark_file" => $file[0],
						 "id_user" => $file[1],
						 "id_sheet" => $file[2],
						 "id_bookmark_folder" => $file[3],
						 "label" => $file[4]);
			$key = "file" . $file_count++;
			$res = $res + array($key => $arr);
		}

		foreach ($sub_folders as $sub_folder) {
			$key = "folder" . $folder_count++;
			$res = array_merge($res, array($key => Bookmark::get_subtree($sub_folder[0])));
		}

		return $res;
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
			$result = Database::query("SELECT id_bookmark_folder_parent FROM BookmarkFolder WHERE id_bookmark_folder = ?", array($id_folder));

			// Sub-files suppression
			Database::exec("DELETE FROM BookmarkFile WHERE id_bookmark_folder = ?", array($id_folder));

			// Sub-folders suppression
			$subfolders = Database::query("SELECT id_bookmark_folder FROM BookmarkFolder WHERE id_bookmark_folder_parent = ?", array($id_folder));
			foreach ($subfolders as $folder) {
				Bookmark::remove_bookmark_folder($folder[0]);
			}

			// Folder suppression
			Database::exec("DELETE FROM BookmarkFolder WHERE id_bookmark_folder = ?", array($id_folder));
			return array("helper" => "bookmark", "message" => "deleted");
		}
	}

	/**
	 * Removes the root folder of an user, and all its sub-folders and files
	 */
	public static function remove_root_folder($id_session)
	{
		$id_user = Database::getUser($id_session);
		if($id_user == -1) {
			return array("helper" => "bookmark", "message" => "user_not_found");
		} else {
			Database::exec("DELETE FROM BookmarkFolder WHERE id_user = ?", array($id_user));
			Database::exec("DELETE FROM BookmarkFile WHERE id_user = ?", array($id_user));
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