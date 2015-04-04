<?php
/*
 * File: project-teams.php
 * Purpose:  show teams, their current members and their captain
 * Modified by: Hans Koomen
 * Modified on: 2015-02-16
 */
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
<html><head>
    <script src="swipe.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.11.2.min.js"></script>
    
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
	<meta name="Author" content="Dr. Hans Koomen">
	<title>URCS DB SYS - Project Teams</title>
	<link rel="stylesheet" type="text/css" href="hans_style.css">
    </head>

    <body>
    
	<h2>TU03 order_history</h2>

	<?php
		require_once 'dbsetup.php';
		//include 'response.php';
		
echo $q;
    ?>
    
<a href="#" onclick="return getOutput();"> test </a>
<div id="output">waiting for action</div>

<div id="swipeBox" ontouchstart="touchStart(event,'swipeBox');" ontouchend="touchEnd(event);" ontouchmove="touchMove(event);" ontouchcancel="touchCancel(event);">
<?php
	try {
	    echo '<p>', 'Database pull (TINDER) as of ', date("Y-m-d H:i:s"), '</p>';

		
		$rand_id = mt_rand(1, 32);
	    $sql = "select * from images where id = '$rand_id';";
		$rand_id_next = mt_rand(1,33);
		echo "query = " . $sql;
		echo "</br>";
	    foreach ($db->query($sql) as $row)
	    {		
			//echo $path.$id.'.jpg';
			echo "<img id = 'image_shown' data-num=$rand_id src='$path$rand_id.jpg' height='800' width='800'>";
			}
		}
		catch (PDOException $e) {
			print "Error!: " . $e->getMessage() . "<br/>";
			die();
		}
		?>
        </div>
    </body>
</html>