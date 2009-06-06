<%@ Page
	Language           = "C#"
	AutoEventWireup    = "false"
	Inherits           = "WebUI.ServiceStatus"
	ValidateRequest    = "false"
	EnableSessionState = "false"
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>ServiceStatus</title>

		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
		<meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
		<meta http-equiv="PRAGMA" content="NO-CACHE" />

		<link href="WebUI.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript" src="jquery.js"></script>
		<script type="text/javascript" src="webui.js"></script>
		<style>
		body
		{	
			margin: -5px; 
		}
		</style>
	</head>
	<body id="service_status_body">
		<form id="Form_ServiceStatus" method="post" runat="server">
			<table id=result_table cellSpacing=3 cellPadding=1 align=left border=0>
				<tbody id=result_table_body>
				</tbody>
			</table>
		</form>
	</body>
</html>
