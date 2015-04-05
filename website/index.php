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
    
    <style>
    .clicker { 
    -moz-transition: all .2s ease-in;
    -o-transition: all .2s ease-in;
    -webkit-transition: all .2s ease-in;
    transition: all .2s ease-in;
    background: #eee; 
    padding: 20px;
	}
	
	</style>
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
        <div class = "clicker">
			<div id="swipeBox" ontouchstart="touchStart(event,'swipeBox');" ontouchend="touchEnd(event);" ontouchmove="touchMove(event);" ontouchcancel="touchCancel(event);">
<?php
				try {
					$rand_id = mt_rand(1, 100);
					$sql = "SELECT * FROM images WHERE id = '$rand_id';";
					$rand_id_next = mt_rand(1,100);
					foreach ($db->query($sql) as $row) {
						//echo $path.$id.'.jpg';
						echo "<div class=\"row\"><div class=\"col-xs-10 col-xs-offset-1\"><img id = 'image_shown' data-num=$rand_id src='$path$rand_id.jpg' height=auto width='100%'></div></div>";
					}
				} catch(PDOException $e) {
					print "Error!: " . $e->getMessage() . "<br/>";
					die();
				}
?>		
			</div>
			</div>
			<div class="row">
				<div class="col-xs-3 col-xs-offset-1">
					<button class="btn btn-danger btn-lg" onclick="leftButton()"><span class="glyphicon glyphicon-fire" aria-hidden="true"></span></button>
				</div>
				<div class="col-xs-2 col-xs-offset-0">
                <div class="col-xs-1 col-xs-offset-4">
					<button class="btn btn-info btn-lg"><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span></button>
				</div>
                </div>
				<div class="col-xs-2 col-xs-offset-2">
                <div class="col-xs-1 col-xs-offset-0">
					<button class="btn btn-success btn-lg" onclick="rightButton()"><span class="glyphicon glyphicon-heart" aria-hidden="true"></span></button>
				</div>
                </div>
			</div>
		</div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
        <script src="js/bootstrap.min.js"></script>
    </body>
</html>