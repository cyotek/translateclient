/*Service list gen*/
function CreateTable(parentName, name, DefaultTextFormat)
{
	var resultTable = document.createElement("table");
	resultTable.style.cssText = "text-align: left;width: 95%;" + DefaultTextFormat;
	resultTable.id = name;
	resultTable.setAttribute("border", "0");
	resultTable.setAttribute("cellpadding", "1");
	resultTable.setAttribute("cellspacing", "3");
	resultTable.setAttribute("align", "left");
	document.getElementById(parentName).appendChild(resultTable);
			
			
	var tableBody = document.createElement("TBODY");
	resultTable.appendChild(tableBody);
	tableBody.id = name +"_body";
}

function SetNodeInnerHtml(nodeName, innerHTML)
{
	document.getElementById(nodeName).innerHTML = innerHTML;
}

/*resultBrowser*/
function CreateDataRow(parentName, isClean, DefaultTextFormat)
{
	if(parentName == null)
		var tableBody = document.getElementById("result_table_body");
	else	
		var tableBody = document.getElementById(parentName);
	
	var tableRow = document.createElement("TR");
	tableBody.appendChild(tableRow);
			
	var rowCell = document.createElement("TD");
	tableRow.appendChild(rowCell);
	if(isClean)
		rowCell.style.cssText = "width: 100%;";
	else
		rowCell.style.cssText = "border-top: 1px solid gray;width: 100%;";
			
	var rowTable = document.createElement("table");
	rowCell.appendChild(rowTable);
	rowTable.style.cssText = "text-align: left;width: 95%;" + DefaultTextFormat;
	rowTable.setAttribute("border", "0");
	rowTable.setAttribute("cellpadding", "1");
	rowTable.setAttribute("cellspacing", "3");
			
			
	tableBody = document.createElement("TBODY");
	rowTable.appendChild(tableBody);
	var dataRow = document.createElement("TR");
	tableBody.appendChild(dataRow);
	return dataRow;
}


function CreateServiceIconCell(iconCellHtml)
{
	var tableCell = document.createElement("TD");

	tableCell.style.cssText = "width: 16px;";
	tableCell.innerHTML = iconCellHtml;
	tableCell.setAttribute("vAlign", "top");
	return tableCell;
}

function AddTranslationCell(parentName, isClean, DefaultTextFormat, iconCellHtml, dataCellHtml)
{
	var tableRow = CreateDataRow(parentName, isClean, DefaultTextFormat);
	if(iconCellHtml != null)
		tableRow.appendChild(CreateServiceIconCell(iconCellHtml));
	var tableCell = document.createElement("TD");
	tableRow.appendChild(tableCell);
	tableCell.style.cssText = "width: 100%;";
	try
	{
		tableCell.innerHTML = dataCellHtml;
	}
	catch(err)
	{
		txt="There was an error on setting translation result.\n\n";
		txt+="Error description: " + err.description + "\n\n";
		txt+="data: " + dataCellHtml + "\n\n";
		txt+="Click OK to continue.\n\n";
		alert(txt);
	}
}

function SetTableStyle(DefaultTextFormat)
{
	var tableBody = document.getElementById("result_table_body");
	tableBody.style.cssText = "text-align: left;width: 95%;" + DefaultTextFormat;
}

function ClearTranslations()
{
	return RemoveAllChilds("result_table_body");
}

//Remove all childs from the element
function RemoveAllChilds(parentName)
{
	var element = document.getElementById(parentName);
	if(element)
	{
		while (element.firstChild) 
		{
	  		element.removeChild(element.firstChild);
		}
		return true;
	}
	return false;
}

//remove element from document
function RemoveElement(name)
{
	var element = document.getElementById(name);
	if(element)
	{
		var parent = element.parentNode;
		if(parent)
		{
	  		parent.removeChild(element);
	  		return true;
		}
	}
	return false;
}

function GetCurrentSelection() 
{
	if (document.getSelection) 
	{
		var str = document.getSelection();
		if (window.RegExp) 
		{
			var regstr = unescape("%20%20%20%20%20");
			var regexp = new RegExp(regstr, "g");
			str = str.replace(regexp, "");
		}
	} 
	else if (document.selection && document.selection.createRange) 
	{
		var range = document.selection.createRange();
		var str = range.text;
	} 
	else
	{
		var str = "";//"Sorry, this is not possible with your browser.";
	}
	return str;
}

