<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adv.cs" Inherits="v2_invoice" %>
<!DOCTYPE html
	PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<!-- HTTP 1.1 -->
	<meta http-equiv="pragma" content="no-cache">
	<!-- HTTP 1.0 -->
	<meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
	<!-- Prevent caching at the proxy server -->
	<meta http-equiv="expires" content="Mon, 04 Dec 1999 21:29:02 GMT">
	<meta http-equiv="Cache" content="no-cache">
	<link rel="stylesheet" href="../admin/assets/adv.css?q=2">
	<script src="../admin/assets/jquery.min.js"></script>
	<script src="../admin/assets/adv.js?q=2"></script>

</head>

<body>
	<div class="carousel" >


		<%=PrintImg()%>
	</div>
	<div class="logo">
		<img src="../admin/assets/adv_bot.jpg" alt="GPOS" onclick=refresh()>
	</div>
</body>

</html>
