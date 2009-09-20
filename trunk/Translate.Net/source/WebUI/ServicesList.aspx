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
		<title>Translate.Net services list</title>

		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
		<meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
		<meta http-equiv="PRAGMA" content="NO-CACHE" />
		<link rel="stylesheet" type="text/css" href="main.css">
		<script type="text/javascript" src="WebUI.js"></script>
		<script language="javascript">
		function treeView(section)
		{
			section_element = document.getElementById(section);
			if(section_element.style.display == 'none')
				section_element.style.display = 'inline';
			else
				section_element.style.display = 'none';
		}
		</script>
	</head>
	<body>
		<a href="http://translate-net.appspot.com/">
		<img style="border: 0px solid ;width: 32px; height: 32px;" alt="Homepage" src="translate.png"> 
		</a>
	
		<form id="Form1" method="post" runat="server">
			<table id=result_table cellSpacing=3 cellPadding=1 align=left border=0>
				<tbody id=result_table_body>
				</tbody>
			</table>
		</form>
	</body>
</html>
