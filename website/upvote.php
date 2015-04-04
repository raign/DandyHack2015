<?php 
	//echo json_encode('HI');
	require_once 'dbsetup.php';
  	$img = $_POST['img_num'];
	//$sql = "select * from images where id = '$rand_id'";
	$sql = "UPDATE images SET upvotes=upvotes+1 WHERE id=$img;";
	//UPDATE images SET counter=counter+1 WHERE image_id=15
	try{
		$db->query($sql);
	}
	catch (PDOException $e) {
		$str = "Error!: " . $e->getMessage() . "<br/>";
		echo json_encode(array('error_msg' => $str));
		//die();
	}
	echo json_encode(array('rval' => 'yolo signal inbound', 'success' => true, 'param' => $img, 'posted' => $_POST));
?>