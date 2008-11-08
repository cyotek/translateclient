<%@ Page
	Language           = "C#"
	AutoEventWireup    = "false"
	Inherits           = "WebUI.Default"
	ValidateRequest    = "false"
	EnableSessionState = "false"
%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Translate.Net result page</title>

		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
		<meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
		<meta http-equiv="PRAGMA" content="NO-CACHE" />

		<link href="WebUI.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript" src="webui.js"></script>

	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id=result_table cellSpacing=3 cellPadding=1 align=left border=0>
				<tbody id=result_table_body>
				</tbody>
			</table>
		</form>
	</body>
</html>
