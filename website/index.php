<?php
/*
 * File: project-teams.php
 * Purpose:  show teams, their current members and their captain
 * Modified by: Hans Koomen
 * Modified on: 2015-02-16
 */
require_once 'dbsetup.php';
session_start();
$var_val = 'Test';
$_SESSION['varname'] = $var_val;
$_SESSION['table'];
$_SESSION['email'];
$_SESSION['time_ordered'];
$q = $_GET['name'];
$path = "../img/";

?>
<!DOCTYPE html>
<html lang="en">
	<head>
    	<script src="swipe.js"></script>
    
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
		<meta name="viewport" content="width=device-width, initial-scale=1">

		<title>CAT: Cute Animal Tinder</title>

		<link rel="stylesheet" type="text/css" href="css/boostratp.min.css">
    </head>

    <body>
    	<div class="container">
			<h1>CAT: League of Dick Pics</h1>

<<<<<<< HEAD
<?php
			echo $q;
?>
			<a href="#" onclick="return getOutput();"> test </a>
			<div id="output">waiting for action</div>
=======
	<?php
		require_once 'dbsetup.php';
		
echo $q;
    ?>
    
<a href="#" onclick="return getOutput();"> test </a>
<div id="output">waiting for action</div>
>>>>>>> origin/stage

			<div id="swipeBox" ontouchstart="touchStart(event,'swipeBox');" ontouchend="touchEnd(event);" ontouchmove="touchMove(event);" ontouchcancel="touchCancel(event);">
<?php
	try {
<<<<<<< HEAD
			echo '<p>', 'Database pull (TINDER) as of ', date("Y-m-d H:i:s"), '</p>';
			$rand_id = mt_rand(1, 32);
			$sql = "select * from images where id = '$rand_id';";
			$rand_id_next = mt_rand(1,33);
			echo "query = " . $sql;
			echo "</br>";
			foreach ($db->query($sql) as $row) {
				//echo $path.$id.'.jpg';
				echo "<img id = 'image_shown' data-num=$rand_id src='$path$rand_id.jpg' height='800' width='800'>";
=======
	    echo '<p>', 'Database pull (TINDER) as of ', date("Y-m-d H:i:s"), '</p>';

		
		$rand_id = mt_rand(1, 100);
	    $sql = "select * from images where id = '$rand_id';";
		echo "query = " . $sql;
		echo "</br>";
		
	    foreach ($db->query($sql) as $row)
	    {		
			//echo $path.$id.'.jpg';
			echo "<img id = 'image_shown' data-num=$rand_id src='$path$rand_id.jpg' height='800' width='800'>";
>>>>>>> origin/stage
			}
		} catch(PDOException $e) {
			print "Error!: " . $e->getMessage() . "<br/>";
			die();
		}
?>
			</div>
		</div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
        <script src="js/bootstrap.min.js"></script>
    </body>
</html>