<?php
require_once 'dbsetup.php';
session_start();
$var_val = 'Test';
$_SESSION['varname'] = $var_val;
$_SESSION['table'];
$_SESSION['email'];
$_SESSION['time_ordered'];
$q = $_GET['name'];
$path = "img/";

?>
<!DOCTYPE html>
<html lang="en">
	<head>
    	<script src="swipe.js"></script>
    
    	<meta charset="utf-8">
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
		<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

		<link rel="stylesheet" href="css/bootstrap.min.css">

		<title>CAT: Cute Animal Tinder</title>
    </head>

    <body>
		<nav class="navbar navbar-inverse navbar-static-top">
		  <div class="container-fluid">
		    <!-- Brand and toggle get grouped for better mobile display -->
		    <div class="navbar-header">
		      <a class="navbar-brand" href="#">CAT: League of Dick Pics</a>
		    </div>
		  </div><!-- /.container-fluid -->
		</nav>
		<div class="container">
			<div id="swipeBox" ontouchstart="touchStart(event,'swipeBox');" ontouchend="touchEnd(event);" ontouchmove="touchMove(event);" ontouchcancel="touchCancel(event);">
<?php
				try {
					echo '<p>', 'Database pull (TINDER) as of ', date("Y-m-d H:i:s"), '</p>';
					$rand_id = mt_rand(1, 100);
					$sql = "SELECT * FROM images WHERE id = '$rand_id';";
					$rand_id_next = mt_rand(1,100);
					echo "query = " . $sql;
					echo "</br>";
					foreach ($db->query($sql) as $row) {
						//echo $path.$id.'.jpg';
						echo "<img id = 'image_shown' data-num=$rand_id src='$path$rand_id.jpg' height='300px' width='300px'>";
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